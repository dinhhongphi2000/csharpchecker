using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.Text;

namespace CSharpChecker.ErrorHighLight
{
    class ErrorSpan
    {
        public readonly SnapshotSpan Span;

        // This is used by SpellingErrorsSnapshot.TranslateTo() to map this error to the corresponding error in the next snapshot.
        public int NextIndex = -1;

        public ErrorSpan(SnapshotSpan span)
        {
            this.Span = span;
        }

      
        public static ErrorSpan Clone(ErrorSpan error)
        {
            return new ErrorSpan(error.Span);
        }

        public static ErrorSpan CloneAndTranslateTo(ErrorSpan error, ITextSnapshot newSnapshot)
        {
            var newSpan = error.Span.TranslateTo(newSnapshot, SpanTrackingMode.EdgeExclusive);

            // We want to only translate the error if the length of the error span did not change (if it did change, it would imply that
            // there was some text edit inside the error and, therefore, that the error is no longer valid).
            return (newSpan.Length == error.Span.Length)
                   ? new ErrorSpan(newSpan)
                   : null;
        }
    }
}
