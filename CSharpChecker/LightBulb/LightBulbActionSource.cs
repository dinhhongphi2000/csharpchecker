using BuildArchitecture;
using BuildArchitecture.Context;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;
using SolutionContext = BuildArchitecture.Context.SolutionContext;

namespace CSharpChecker.LightBulb
{
    class LightBulbActionsSource : ISuggestedActionsSource
    {
        private readonly TestSuggestedActionsSourceProvider _factory;
        private readonly ITextBuffer _textBuffer;
        private readonly ITextView _textView;
        List<ErrorInformation> errors;
        public LightBulbActionsSource(TestSuggestedActionsSourceProvider testSuggestedActionsSourceProvider, ITextView textView, ITextBuffer textBuffer)
        {
            //Dte = System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE") as DTE;
            _factory = testSuggestedActionsSourceProvider;
            _textBuffer = textBuffer;
            _textView = textView;
        }

#pragma warning disable 0067
        public event EventHandler<EventArgs> SuggestedActionsChanged;
#pragma warning restore 0067

        public void Dispose()
        {
        }

        public IEnumerable<SuggestedActionSet> GetSuggestedActions(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {


            errors = TestLightBulb();
            List<SuggestedActionSet> suggestedActionSets = new List<SuggestedActionSet>();
            foreach (ErrorInformation err in errors)
            {
                Span span = new Span(err.StartIndex, err.Length);
                if (span.Contains(_textView.Caret.Position.BufferPosition.Position))
                {

                    ITrackingSpan trackingSpan = range.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeInclusive);
                    var action = new LightBulbSuggestedAction(trackingSpan, "Test LightBulb");
                    suggestedActionSets.Add(new SuggestedActionSet(new ISuggestedAction[] { action }));
                }
            }
            //List<Tuple<Span, String>> span = new List<Tuple<Span, String>>();
            //span.Add(new Tuple<Span, String>(new Span(0, 1), "áda"));
            //span.Add(new Tuple<Span, String>(new Span(14, 5), "qưéaáda"));
            //List<SuggestedActionSet> suggestedActionSets = new List<SuggestedActionSet>();
            //foreach (Tuple<Span, String> sp in span)
            //{
            //    if (sp.Item1.Contains(_textView.Caret.Position.BufferPosition.Position))
            //    {

            //        ITrackingSpan trackingSpan = range.Snapshot.CreateTrackingSpan(sp.Item1, SpanTrackingMode.EdgeInclusive);
            //        LightBulbSuggestedAction upperAction = new LightBulbSuggestedAction(trackingSpan, sp.Item2);
            //        suggestedActionSets.Add(new SuggestedActionSet(new ISuggestedAction[] { upperAction }));
            //    }
            //}
            return suggestedActionSets;
        }

        public Task<bool> HasSuggestedActionsAsync(ISuggestedActionCategorySet requestedActionCategories, SnapshotSpan range, CancellationToken cancellationToken)
        {
            errors = TestLightBulb();
            List<Span> span = new List<Span>();
            foreach (ErrorInformation err in errors)
            {
                span.Add(new Span(err.StartIndex, err.Length));
            }
            IEnumerable<SuggestedActionSet> suggestedActionSets = new List<SuggestedActionSet>();
            return Task.Factory.StartNew(() =>
            {
                foreach (Span sp in span)
                {
                    if (sp.Contains(_textView.Caret.Position.BufferPosition.Position))
                    {
                        // don't display the tag if the extent has whitespace
                        return true;
                    }
                }

                return false;
            });
            //List<Span> span = new List<Span>();
            //span.Add(new Span(0, 1));
            //span.Add(new Span(14, 5));
            //IEnumerable<SuggestedActionSet> suggestedActionSets = new List<SuggestedActionSet>();
            //return Task.Factory.StartNew(() =>
            //{
            //    foreach (Span sp in span)
            //    {
            //        if (sp.Contains(_textView.Caret.Position.BufferPosition.Position))
            //        {
            //            // don't display the tag if the extent has whitespace
            //            return true;
            //        }
            //    }

            //    return false;
            //});
        }
    

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }
        public string GetFileContent(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
        public List<ErrorInformation> TestLightBulb()
        {
            string currentFile = @"D:\UIT\KLTN\CSharpParser\TestBuildArchitecture\TestClass.cs";

            var solution = InitSolutionContext();
            IWorkSpace workSpace = new WorkSpace(solution);
            workSpace.CurrentProject = solution.GetProject("TestBuildArchitecture");
            workSpace.CurrentFile = @"D:\UIT\KLTN\CSharpParser\TestBuildArchitecture\TestClass.cs";
            workSpace.UpdateTree(GetFileContent(currentFile));
            return workSpace.RunRules();
        }

        public SolutionContext InitSolutionContext()
        {
            var solution = new SolutionContext(@"D:\UIT\KLTN\CSharpParser\Caculator.sln", "Caculator");
            var project = new ProjectContext(@"D:\UIT\KLTN\CSharpParser\TestBuildArchitecture\", "TestBuildArchitecture");
            solution.AddProjectNode(project.Name, project);
            return solution;
        }

        private bool TryGetWordUnderCaret(out TextExtent wordExtent)
        {
            ITextCaret caret = _textView.Caret;
            SnapshotPoint point;

            if (caret.Position.BufferPosition > 0)
            {
                point = caret.Position.BufferPosition - 1;
            }
            else
            {
                wordExtent = default(TextExtent);
                return false;
            }

            ITextStructureNavigator navigator = _factory.NavigatorService.GetTextStructureNavigator(_textBuffer);

            wordExtent = navigator.GetExtentOfWord(point);
            return true;
        }
    }
}
