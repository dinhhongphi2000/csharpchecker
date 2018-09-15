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
        public override object VisitNamespace_declaration([NotNull] CSharpParser.Namespace_declarationContext context)
        {
            Console.WriteLine("Namespace {0}", context.qi.GetText());
            return base.VisitNamespace_declaration(context);
        }

        public override object VisitClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            Console.WriteLine("\tClass {0}", context.identifier().GetText());
            return base.VisitClass_definition(context);
        }

        //    public override object VisitUsing_directive([NotNull] CSharpParser.Using_directiveContext context)
        //    {
        //        Console.WriteLine(context.ToStringTree());
        //        return base.VisitUsing_directive(context);
        //    }
        }
    }
