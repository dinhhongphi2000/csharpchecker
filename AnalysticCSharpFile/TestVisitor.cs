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
        private List<String> MethodName = new List<string>();

        public override object VisitNamespace([NotNull] CSharpParser.NamespaceContext context)
        {
            NamespaceName.Add(context.qualified_identifier().GetText());
            return base.VisitNamespace(context);
        }

        public void ListNameSpace(System.IO.TextWriter output)
        {
            NamespaceName.GroupBy(d => d).ToList().ForEach(d =>
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
            return base.VisitClass_definition(context);
        }

        public override object VisitMethod_member_name([NotNull] CSharpParser.Method_member_nameContext context)
        {
            for(int id = 0; id < context.identifier().Count();id++)
            {
                MethodName.Add(context.identifier()[id].GetText() +" "+ id.ToString());
            }
            
            return base.VisitMethod_member_name(context);
        }
        public void ListMethodName(System.IO.TextWriter outer)
        {
            foreach(String method in MethodName)
            {
                outer.WriteLine("{0}", method);
            }
        }
    }
}
