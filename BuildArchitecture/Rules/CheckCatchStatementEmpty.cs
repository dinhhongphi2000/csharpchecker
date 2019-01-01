using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckCatchStatementEmpty
    {
        [Export(typeof(Catch_clausesContext))]
        public void VisitIdentifierContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var blockContext = context.GetDeepChildContext<BlockContext>()[0];
            if (blockContext.GetText() == "{}")
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
    }
}
