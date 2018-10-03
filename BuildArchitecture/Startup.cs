using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture
{
    public class Startup
    {
        public void CheckFile(string path)
        {
            AntlrFileStream stream = new AntlrFileStream(path);
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);
            CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
            IParseTree tree = parser.compilation_unit();
            Include_Buildin_Rule(ListenerProvider.Instance);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(ListenerProvider.Instance, startContext);
        }

        private void Include_Buildin_Rule(ListenerProvider provider)
        {
            var ruleClass = Assembly.GetExecutingAssembly().GetTypes();
            var filter = from r in ruleClass
                        where r.IsSubclassOf(typeof(BaseRule))
                        select r;
            filter.ToList().ForEach(rule =>
            {
                BaseRule instance = (BaseRule)Activator.CreateInstance(rule);
            });
        }
    }
}
