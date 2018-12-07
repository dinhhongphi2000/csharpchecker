﻿using BuildArchitecture;
using CSharpChecker.ErrorHighLight;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace CSharpChecker.LightBulb
{
    class LightBulbActionsSource : ISuggestedActionsSource
    {
        private ErrorHighLightChecker _errorChecker;
        private readonly TestSuggestedActionsSourceProvider _factory;
        private readonly ITextBuffer _textBuffer;
        private readonly ITextView _textView;
        List<ErrorInformation> _errors;
        public LightBulbActionsSource(TestSuggestedActionsSourceProvider testSuggestedActionsSourceProvider, ITextView textView, ITextBuffer textBuffer)
        {
            //Dte = System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE") as DTE;
            _factory = testSuggestedActionsSourceProvider;
            _textBuffer = textBuffer;
            _textView = textView;
            
        }

        public event EventHandler<EventArgs> SuggestedActionsChanged;


        public void Dispose()
        {
        }

        public IEnumerable<SuggestedActionSet> GetSuggestedActions(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            if (TryGetHighLightChecker(out _errors))
            {
                List<SuggestedActionSet> suggestedActionSets = new List<SuggestedActionSet>();
                foreach (ErrorInformation err in _errors)
                {
                    Span span = new Span(err.StartIndex, err.Length);
                    if (span.Contains(_textView.Caret.Position.BufferPosition.Position))
                    {

                        ITrackingSpan trackingSpan = range.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeInclusive);
                        var action = new LightBulbSuggestedAction(trackingSpan, err.ReplaceCode);
                        suggestedActionSets.Add(new SuggestedActionSet(new ISuggestedAction[] { action }));
                    }
                }

                return suggestedActionSets;
            }
            else return null;
        }

        public Task<bool> HasSuggestedActionsAsync(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                if (TryGetHighLightChecker(out _errors))
                {

                    List<Span> span = new List<Span>();
                    foreach (ErrorInformation err in _errors)
                    {
                        if (err.Length >= 0)
                        {
                            span.Add(new Span(err.StartIndex, err.Length));
                        }
                    }
                    IEnumerable<SuggestedActionSet> suggestedActionSets = new List<SuggestedActionSet>();

                    foreach (Span sp in span)
                    {
                        if (sp.Contains(_textView.Caret.Position.BufferPosition.Position))
                        {
                            // don't display the tag if the extent has whitespace
                            return true;
                        }
                    }
                }
                return false;
            });

        }
    
        public bool TryGetHighLightChecker(out List<ErrorInformation> errors)
        {
            if (_textBuffer.Properties.ContainsProperty(typeof(ErrorHighLightChecker)))
            {
                _errorChecker = _textBuffer.Properties.GetProperty(typeof(ErrorHighLightChecker)) as ErrorHighLightChecker;
                errors = _errorChecker.GetSpanErrors();
                if (errors == null) return false;
                else return true;
            }
            else
            {
                errors = null;
                return false;
            }

        }

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }
    }
}