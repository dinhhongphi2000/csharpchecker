using Microsoft.VisualStudio.Shell.TableControl;
using Microsoft.VisualStudio.Shell.TableManager;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CSharpChecker.ErrorHighLight
{
    /// <summary>
    /// Factory for the <see cref="ITagger{T}"/>. There will be one instance of this class/VS session.
    /// 
    /// It is also the <see cref="ITableDataSource"/> that reports errors in code.
    /// </summary>
    [Export(typeof(IViewTaggerProvider))]
    [TagType(typeof(IErrorTag))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    [TextViewRole(PredefinedTextViewRoles.Analyzable)]
    internal sealed class ErrorHighLightProvider : IViewTaggerProvider, ITableDataSource
    {
        public readonly ITableManager ErrorTableManager;
        public readonly ITextDocumentFactoryService TextDocumentFactoryService;

        private const string ErrorCheckerDataSource = "CSharpChecker";

        private readonly List<SinkManager> _managers = new List<SinkManager>();      // Also used for locks
        private readonly List<ErrorHighLightChecker> _errorCheckers = new List<ErrorHighLightChecker>();

        [ImportingConstructor]
        internal ErrorHighLightProvider([Import]ITableManagerProvider provider, [Import] ITextDocumentFactoryService textDocumentFactoryService)
        {
            this.ErrorTableManager = provider.GetTableManager(StandardTables.ErrorsTable);
            this.TextDocumentFactoryService = textDocumentFactoryService;

            this.ErrorTableManager.AddSource(this, StandardTableColumnDefinitions.ErrorSeverity, StandardTableColumnDefinitions.ErrorCode,
                                                   StandardTableColumnDefinitions.ErrorSource, StandardTableColumnDefinitions.BuildTool,
                                                   StandardTableColumnDefinitions.ErrorSource, StandardTableColumnDefinitions.ErrorCategory,
                                                   StandardTableColumnDefinitions.Text, StandardTableColumnDefinitions.DocumentName, StandardTableColumnDefinitions.Line, StandardTableColumnDefinitions.Column);
        }

        /// <summary>
        /// Create a tagger that does error checking on the view/buffer combination.
        /// </summary>
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            ITagger<T> tagger = null;
            // Only attempt to spell check on the view's edit buffer (and multiple views could have that buffer open simultaneously so
            // only create one instance of the spell checker.
            if ((buffer == textView.TextBuffer) && (typeof(T) == typeof(IErrorTag)))
            {
                ErrorHighLightChecker errorChecker = buffer.Properties.GetOrCreateSingletonProperty(typeof(ErrorHighLightChecker), () => new ErrorHighLightChecker(this, textView, buffer));
                tagger = new ErrorHighLightTagger(errorChecker) as ITagger<T>;
            }
            return tagger;
        }

        #region ITableDataSource members
        public string DisplayName
        {
            get
            {
                return "CSharp Checker";
            }
        }

        public string Identifier
        {
            get
            {
                return ErrorCheckerDataSource;
            }
        }

        public string SourceTypeIdentifier
        {
            get
            {
                return StandardTableDataSources.ErrorTableDataSource;
            }
        }

        public IDisposable Subscribe(ITableDataSink sink)
        {
            // This method is called to each consumer interested in errors. In general, there will be only a single consumer (the error list tool window)
            // but it is always possible for 3rd parties to write code that will want to subscribe.
            return new SinkManager(this, sink);
        }
        #endregion

        public void AddSinkManager(SinkManager manager)
        {
            // This call can, in theory, happen from any thread so be appropriately thread safe.
            // it will probably be called only once from the UI thread (by the error list tool window).
            lock (_managers)
            {
                _managers.Add(manager);

                // Add the pre-existing spell checkers to the manager.
                foreach (var errorChecker in _errorCheckers)
                {
                    manager.AddErrorChecker(errorChecker);
                }
            }
        }

        public void RemoveSinkManager(SinkManager manager)
        {
            // This call can, in theory, happen from any thread so be appropriately thread safe.
            // it will probably be called only once from the UI thread (by the error list tool window).
            lock (_managers)
            {
                _managers.Remove(manager);
            }
        }

        public void AddSpellChecker(ErrorHighLightChecker errorChecker)
        {
            // This call will always happen on the UI thread (it is a side-effect of adding or removing the 1st/last tagger).
            lock (_managers)
            {
                _errorCheckers.Add(errorChecker);

                // Tell the preexisting managers about the new spell checker
                foreach (var manager in _managers)
                {
                    manager.AddErrorChecker(errorChecker);
                }
            }
        }

        public void RemoveSpellChecker(ErrorHighLightChecker errorChecker)
        {
            // This call will always happen on the UI thread (it is a side-effect of adding or removing the 1st/last tagger).
            lock (_managers)
            {
                _errorCheckers.Remove(errorChecker);

                foreach (var manager in _managers)
                {
                    manager.RemoveErrorChecker(errorChecker);
                }
            }
        }

        public void UpdateAllSinks()
        {
            lock (_managers)
            {
                foreach (var manager in _managers)
                {
                    manager.UpdateSink();
                }
            }
        }
    }
}
