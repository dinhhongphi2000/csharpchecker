using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace ShowElementOfCSharpFile_InTree
{
    class UpperCaseClassName : CSharpParserBaseListener
    {
        private ITokenStream _tokenStream;
        public TokenStreamRewriter ValidCode;
        public IToken classNameToken;
        public UpperCaseClassName(ITokenStream tokenStream)
        {
            _tokenStream = tokenStream;
            ValidCode = new TokenStreamRewriter(tokenStream);
        }

        public override void EnterClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            classNameToken = context.identifier().Start;
        }

        public override void EnterMethod_member_name([NotNull] CSharpParser.Method_member_nameContext context)
        {
            IToken token = context.identifier()[0].Start;
            ValidCode.Replace(classNameToken, token, classNameToken.Text);
        }
}
}
