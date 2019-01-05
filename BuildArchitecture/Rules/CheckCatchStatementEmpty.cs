using Antlr4.Runtime;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckCatchStatementEmpty
    {
        CommonTokenStream tokenStream;

        [Export(typeof(Catch_clausesContext))]
        public void VisitCatchContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var blockContext = context.GetDeepChildContext<BlockContext>()[0];
            int a = blockContext.Start.StartIndex;
            int b = blockContext.Stop.StopIndex;
            if (blockContext.Start
                .InputStream.GetText(new Antlr4.Runtime.Misc.Interval(a, b))
                .Replace("\r","")
                .Replace("\n","")
                .Replace(" ","")
                == "{}")
            {
                var identifier = blockContext.GetText();
                error = new ErrorInformation
                {
                    StartIndex = blockContext.Start.StartIndex,
                    ErrorCode = "WA0003",
                    Length = blockContext.Stop.StopIndex - blockContext.Start.StartIndex + 1,
                    ErrorMessage = "UIT: Catch Statement should not be empty"
                };
            }
        }

        [Export("TokenStreamChanged")]
        public void UpdateTokenStream(CommonTokenStream stream)
        {
            tokenStream = stream;
        }
    }
}
