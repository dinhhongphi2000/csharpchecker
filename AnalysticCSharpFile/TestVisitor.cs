using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace AnalysticCSharpFile
{
    class TestVisitor : CSharpParserBaseVisitor<object>
    {
        private List<String> NamespaceName = new List<string>();
        private List<Tuple<String, String>> MethodNames = new List<Tuple<string, string>>();

        public override object VisitNamespace([NotNull] CSharpParser.NamespaceContext context)
        {
            NamespaceName.Add(context.qualified_identifier().GetText());
            return base.VisitNamespace(context);
        }

        public void ListNameSpace(System.IO.TextWriter output)
        {
            NamespaceName.ToList().GroupBy(d => d).ToList().ForEach(d =>
            {
                output.WriteLine(d.Key);
                ListMethodOfNamespace(d.Key, output);
            });

        }

        private void ListMethodOfNamespace(string namespaceName, System.IO.TextWriter output)
        {
            List<string> values = new List<string>();
            MethodNames.ForEach(d =>
            {
                if (d.Item1 == namespaceName)
                    output.WriteLine("\t{0}", d.Item2);
            });
        }

        public override object VisitClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            MethodNames.Add(new Tuple<string, string>(NamespaceName.Last(), context.identifier().GetText()));
            return null;
        }
    }
}
