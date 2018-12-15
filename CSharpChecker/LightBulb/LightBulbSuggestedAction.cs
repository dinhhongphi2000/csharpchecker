using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Language.Intellisense;
using BuildArchitecture;
using Span = Microsoft.VisualStudio.Text.Span;

namespace CSharpChecker.LightBulb
{
    internal class LightBulbSuggestedAction : ISuggestedAction
    {
        private readonly ITrackingSpan _span;
        private readonly ITextSnapshot _snapshot;
        private readonly List<ReplaceCodeInfomation> _replaceText;
        private readonly string _display;

        public LightBulbSuggestedAction(ITrackingSpan span, List<ReplaceCodeInfomation> replaceText)
        {
            _span = span;
            _snapshot = span.TextBuffer.CurrentSnapshot;
            _replaceText = replaceText;
            _display = string.Format("Replace '{0}' to '{1}'", span.GetText(_snapshot),_replaceText[0].ReplaceCode);
        }
        public string DisplayText
        {
            get
            {
                return _display;
            }
        }

        public string IconAutomationText
        {
            get
            {
                return null;
            }
        }

        ImageMoniker ISuggestedAction.IconMoniker
        {
            get
            {
                return default(ImageMoniker);
            }
        }

        public string InputGestureText
        {
            get
            {
                return null;
            }
        }

        public bool HasActionSets
        {
            get
            {
                return false;
            }
        }

        public Task<IEnumerable<SuggestedActionSet>> GetActionSetsAsync(CancellationToken cancellationToken)
        {
            return null;
        }

        public bool HasPreview
        {
            get
            {
                return false;
            }
        }

        public Task<object> GetPreviewAsync(CancellationToken cancellationToken)
        {
            //var textBlock = new TextBlock();
            //textBlock.Padding = new Thickness(5);
            //textBlock.Inlines.Add(new Run() { Text = _replaceText });
            //return Task.FromResult<object>(textBlock);
            return null;
        }

        public void Dispose()
        {
        }

        public void Invoke(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            foreach(var err in _replaceText)
            {
                Span span = new Span(err.Start, err.Length);
                ITrackingSpan trackingSpan = _span.TextBuffer.CurrentSnapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeInclusive);
                _span.TextBuffer.Replace(trackingSpan.GetSpan(_snapshot), err.ReplaceCode);

            }
        }

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }
    }
}
