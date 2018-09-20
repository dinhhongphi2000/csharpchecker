using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace AnalysticCSharpFile
{
    class TestListener:CSharpParserBaseListener
    {
        private CSharpParser _parser;
        private List<Tuple<int, string>> _tuples = new List<Tuple<int, string>>();
        public TestListener(CSharpParser parser)
        {
            this._parser = parser;
        }
        public override void EnterCompilation_unit([NotNull] CSharpParser.Compilation_unitContext context)
        {

        }
        public override void  EnterMethod_member_name([NotNull] CSharpParser.Method_member_nameContext context)
        {
            _tuples.Add(new Tuple<int, string>(context.identifier()[0].start.StartIndex, UppercaseFirst(context.identifier()[0].GetText())));
            Console.WriteLine(context.identifier()[0].GetText());
        }

        public List<Tuple<int,string>> GetTuples()
        {
            return _tuples;
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
