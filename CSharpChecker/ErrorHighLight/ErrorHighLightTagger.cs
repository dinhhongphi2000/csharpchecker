using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;

namespace CSharpChecker.ErrorHighLight
{
    class ErrorHighLightTagger : ITagger<IErrorTag>, IDisposable
    {
        private readonly ErrorHighLightChecker _errorChecker;
        private ErrorSnapShot _errors;

        internal ErrorHighLightTagger(ErrorHighLightChecker errorChecker)
        {
            _errorChecker = errorChecker;
            _errors = errorChecker.LastError;

            errorChecker.AddTagger(this);
        }

        internal void UpdateErrors(ITextSnapshot currentSnapshot, ErrorSnapShot errors)
        {
            ErrorSnapShot oldErrors = _errors;
            _errors = errors;

            EventHandler<SnapshotSpanEventArgs> h = this.TagsChanged;
            if (h != null)
            {
                // Raise a single tags changed event over the span that could have been affected by the change in the errors.
                int start = int.MaxValue;
                int end = int.MinValue;

                if ((oldErrors != null) && (oldErrors.Errors.Count > 0))
                {
                    start = oldErrors.Errors[0].Span.Start.TranslateTo(currentSnapshot, PointTrackingMode.Negative);
                    end = oldErrors.Errors[oldErrors.Errors.Count - 1].Span.End.TranslateTo(currentSnapshot, PointTrackingMode.Positive);
                }

                if (errors.Count > 0)
                {
                    start = Math.Min(start, errors.Errors[0].Span.Start.Position);
                    end = Math.Max(end, errors.Errors[errors.Errors.Count - 1].Span.End.Position);
                }

                if (start < end)
                {
                    h(this, new SnapshotSpanEventArgs(new SnapshotSpan(currentSnapshot, Span.FromBounds(start, end))));
                }
            }
        }

        public void Dispose()
        {
            // Called when the tagger is no longer needed (generally when the ITextView is closed).
            _errorChecker.RemoveTagger(this);
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IErrorTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (_errors != null)
            {
                foreach (var error in _errors.Errors)
                {
                    if (spans.IntersectsWith(error.Span))
                    {
                        yield return new TagSpan<IErrorTag>(error.Span, new ErrorTag(PredefinedErrorTypeNames.Warning,error.ErrorMessage));
                    }
                }
            }
        }
    }
}
