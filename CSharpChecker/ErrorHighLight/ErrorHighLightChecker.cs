using BuildArchitecture;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CSharpChecker.ErrorHighLight
{
    ///<summary>
    /// Finds the spelling errors in comments for a specific buffer.
    ///</summary>
    /// <remarks><para>The lifespan of this object is tied to the lifespan of the taggers on the view. On creation of the first tagger, the SpellChecker starts doing
    /// work to find errors in the file. On the disposal of the last tagger, it shuts down.</para>
    /// </remarks>
    public class ErrorHighLightChecker
    {
        private readonly ErrorHighLightProvider _provider;
        private readonly ITextBuffer _buffer;
        private readonly Dispatcher _uiThreadDispatcher;
        private readonly ITextView _textview;      // Used to do spell checking.

        private IClassifier _classifier;

        private ITextSnapshot _currentSnapshot;

        private bool _isUpdating = false;
        private bool _isDisposed = false;

        private readonly List<ErrorHighLightTagger> _activeTaggers = new List<ErrorHighLightTagger>();
        private List<ErrorInformation> _spanErrors = new List<ErrorInformation>();
        internal readonly string FilePath;
        internal readonly ErrorFactory Factory;

        internal ErrorHighLightChecker(ErrorHighLightProvider provider, ITextView textView, ITextBuffer buffer)
        {
            _provider = provider;
            _buffer = buffer;
            _currentSnapshot = buffer.CurrentSnapshot;
            _textview = textView;
            // Get the name of the underlying document buffer
            ITextDocument document;
            if (provider.TextDocumentFactoryService.TryGetTextDocument(textView.TextDataModel.DocumentBuffer, out document))
            {
                this.FilePath = document.FilePath;
                // TODO we should listen for the file changing its name (ITextDocument.FileActionOccurred)
            }


            // We're assuming we're created on the UI thread so capture the dispatcher so we can do all of our updates on the UI thread.
            _uiThreadDispatcher = Dispatcher.CurrentDispatcher;

            this.Factory = new ErrorFactory(this, new ErrorSnapShot(this.FilePath, 0));
        }

        internal void AddTagger(ErrorHighLightTagger tagger)
        {
            _activeTaggers.Add(tagger);

            if (_activeTaggers.Count == 1)
            {
                // First tagger created ... start doing stuff.
                _classifier = _provider.ClassifierAggregatorService.GetClassifier(_buffer);

                _buffer.ChangedLowPriority += this.OnBufferChange;

                _provider.AddSpellChecker(this);

                this.KickUpdate();
            }
        }

        internal void RemoveTagger(ErrorHighLightTagger tagger)
        {
            _activeTaggers.Remove(tagger);

            if (_activeTaggers.Count == 0)
            {
                // Last tagger was disposed of. This is means there are no longer any open views on the buffer so we can safely shut down
                // spell checking for that buffer.
                _buffer.ChangedLowPriority -= this.OnBufferChange;

                _provider.RemoveSpellChecker(this);

                _isDisposed = true;

                _buffer.Properties.RemoveProperty(typeof(ErrorHighLightChecker));

                if (_classifier is IDisposable classifierDispose)
                    classifierDispose.Dispose();

                _classifier = null;
            }
        }

        private void OnBufferChange(object sender, TextContentChangedEventArgs e)
        {
            _currentSnapshot = e.After;

            // Translate all the spelling errors to the new snapshot (and remove anything that is a dirty region since we will need to check that again).
            var oldSpellingErrors = this.Factory.CurrentSnapshot;
            var newSpellingErrors = new ErrorSnapShot(this.FilePath, oldSpellingErrors.VersionNumber + 1);

            // Copy all of the old errors to the new errors unless the error was affected by the text change
            foreach (var error in oldSpellingErrors.Errors)
            {
                Debug.Assert(error.NextIndex == -1);

                ErrorSpan newError = ErrorSpan.CloneAndTranslateTo(error, e.After);

                if (newError != null)
                {
                    Debug.Assert(newError.Span.Length == error.Span.Length);

                    error.NextIndex = newSpellingErrors.Errors.Count;
                    newSpellingErrors.Errors.Add(newError);
                }
            }

            this.UpdateSpellingErrors(newSpellingErrors);

            this.KickUpdate();
        }

        private void KickUpdate()
        {
            // We're assuming we will only be called from the UI thread so there should be no issues with race conditions.
            if (!_isUpdating)
            {
                _isUpdating = true;
                _uiThreadDispatcher.BeginInvoke(new Action(() => this.DoUpdate()), DispatcherPriority.Background);
            }
        }

        private void DoUpdate()
        {
            // It would be good to do all of this work on a background thread but we can't:
            //      _classifier.GetClassificationSpans() should only be called on the UI thread because some classifiers assume they are called from the UI thread.
            //      Raising the TagsChanged event from the taggers needs to happen on the UI thread (because some consumers might assume it is being raised on the UI thread).
            // 
            // Updating the snapshot for the factory and calling the sink can happen on any thread but those operations are so fast that there is no point.
            if (!_isDisposed)
            {
                if (_buffer.Equals(_textview.TextBuffer))
                {
                    ErrorSnapShot oldErrors = this.Factory.CurrentSnapshot;
                    ErrorSnapShot newErrors = new ErrorSnapShot(this.FilePath, oldErrors.VersionNumber + 1);
                    List<ErrorInformation> newSpanErrors = new List<ErrorInformation>();
                    // Go through the existing errors. If they are on the line we are currently parsing then
                    // copy them to oldLineErrors, otherwise they go to the new errors.

                    newSpanErrors = this.TestLightBulb(_buffer.CurrentSnapshot.GetText());
                    if (!newSpanErrors.Equals(_spanErrors))
                    {
                        _spanErrors = newSpanErrors;
                        foreach (ErrorInformation spanError in _spanErrors)
                        {
                            SnapshotSpan newSpan = new SnapshotSpan(_buffer.CurrentSnapshot, spanError.StartIndex, spanError.Length);
                            ErrorSpan oldError = oldErrors.Errors.Find((e) => e.Span == newSpan);

                            if (oldError != null)
                            {
                                // There was a spelling error at the same span as the old one so we should be able to just reuse it.
                                oldError.NextIndex = newErrors.Errors.Count;
                                newErrors.Errors.Add(ErrorSpan.Clone(oldError));    // Don't clone the old error yet
                            }
                            else
                            {
                                newErrors.Errors.Add(new ErrorSpan(newSpan));
                            }
                        }
                        this.UpdateSpellingErrors(newErrors);
                    }
                    else
                    {
                        foreach (var error in oldErrors.Errors)
                        {
                            error.NextIndex = -1;
                        }
                    }
                }
            }
            _isUpdating = false;
        }
        private void UpdateSpellingErrors(ErrorSnapShot spellingErrors)
        {
            // Tell our factory to snap to a new snapshot.
            this.Factory.UpdateErrors(spellingErrors);

            // Tell the provider to mark all the sinks dirty (so, as a side-effect, they will start an update pass that will get the new snapshot
            // from the factory).
            _provider.UpdateAllSinks();

            foreach (var tagger in _activeTaggers)
            {
                tagger.UpdateErrors(_currentSnapshot, spellingErrors);
            }

            this.LastError = spellingErrors;
        }

        internal ErrorSnapShot LastError { get; private set; }
        public string GetFileContent(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
        public List<ErrorInformation> TestLightBulb(string buffer)
        {
            WorkSpace workSpace = new WorkSpace();
            workSpace.InitOrUpdateParserTreeOfFile(this.FilePath, buffer);
            workSpace.RunRules(this.FilePath);
            return workSpace.GetErrors();
        }
        public List<ErrorInformation> GetSpanErrors()
        {
            return _isDisposed ? null : _spanErrors;
        }
    }
}
