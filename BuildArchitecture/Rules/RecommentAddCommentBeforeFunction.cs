using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildArchitecture.CSharpParser;
namespace BuildArchitecture.Rules
{
    /// <summary>
    /// 
    /// </summary>
    class RecommentAddCommentBeforeFunction
    {
        CommonTokenStream tokenStream;

        [Export(typeof(Class_member_declarationContext))]
        public void CheckCommentFunctionInClass(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var classMemberContext = (Class_member_declarationContext)context;

            if (IsMethod(classMemberContext.common_member_declaration()) 
                && tokenStream.GetHiddenTokensToLeft(context.Start.TokenIndex, CSharpLexer.COMMENTS_CHANNEL) == null)
            {
                var functionContext = GetFunctionIdentifierContext(classMemberContext.common_member_declaration());
                error = CreateError(functionContext);
            }
        }

        [Export(typeof(Struct_member_declarationContext))]
        public void CheckCommentFunctionInStruct(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var structMemberContext = (Struct_member_declarationContext)context;

            if (IsMethod(structMemberContext.common_member_declaration())
                && tokenStream.GetHiddenTokensToLeft(context.Start.TokenIndex, CSharpLexer.COMMENTS_CHANNEL) == null)
            {
                var functionContext = GetFunctionIdentifierContext(structMemberContext.common_member_declaration());
                error = CreateError(functionContext);
            }
        }

        private ErrorInformation CreateError(IdentifierContext function)
        {
            return new ErrorInformation()
            {
                ErrorCode = "IF0002",
                ErrorMessage = "You should add comment for method " + function.GetText(),
                StartIndex = function.Start.StartIndex,
                Length = function.SourceInterval.Length
            };
        }

        private bool IsMethod(Common_member_declarationContext context)
        {
            if ((context.method_declaration() != null)
                || (context.typed_member_declaration().method_declaration() != null))
                return true;
            return false;
        }

        private IdentifierContext GetFunctionIdentifierContext(Common_member_declarationContext context)
        {
            if (context.typed_member_declaration() != null)
            {
                var identifiers = context.typed_member_declaration().method_declaration().method_member_name().identifier();
                return identifiers[identifiers.Length - 1];
            }
            else
            {
                var identifiers = context.method_declaration().method_member_name().identifier();
                return identifiers[identifiers.Length - 1];
            }
        }

        [Export("TokenStreamChanged")]
        public void UpdateTokenStream(CommonTokenStream stream)
        {
            tokenStream = stream;
        }
    }
}
