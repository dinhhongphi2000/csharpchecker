using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        private ErrorInformation CreateError(IdentifierContext function)
        {
            ParserRuleContext method = (ParserRuleContext)function.Parent.Parent.Parent.Parent;
            if (method is Common_member_declarationContext)
                method = (ParserRuleContext)method.Parent;
            int spaceBefore = method.Start.Column;
            List<ReplaceCodeInfomation> replaceCode = new List<ReplaceCodeInfomation>()
            {
                new ReplaceCodeInfomation()
                {
                    Start = method.Start.StartIndex - 1,
                    Length = 1,
                    ReplaceCode = " /// <summary>\r\n"
                                  + InsertSpace(spaceBefore) +"/// " + function.GetText() + "\r\n"
                                  + InsertSpace(spaceBefore) + "/// </summary>\r\n"
                                  + InsertParameterComment(function.Symbol as MethodSymbol, spaceBefore)
                                  + InsertSpace(spaceBefore) + "/// <returns></returns>\r\n"
                                  + InsertSpace(spaceBefore)
                }
            };
            return new ErrorInformation()
            {
                ErrorCode = "IF0002",
                ErrorMessage = "You should add comment for method " + function.GetText(),
                DisplayText = "Add comment before function",
                StartIndex = function.Start.StartIndex,
                Length = 1,
                ReplaceCode = replaceCode
            };
        }

        private string InsertSpace(int length)
        {
            string s = "";
            for(int i = 0; i < length; i++)
            {
                s += " ";
            }
            return s;
        }

        private string InsertParameterComment(MethodSymbol method, int spacebefore)
        {
            string buildString = "";
            var parameters = method.GetParameterSymbol();
            foreach(var item in parameters)
            {
                buildString += String.Format("{0}/// <param name=\"{1}\"></param>\r\n", 
                    InsertSpace(spacebefore), 
                    item.GetName());
            }
            return buildString;
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
