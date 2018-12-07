using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    /// <summary>
    /// format order : field, property, function, struct , class
    /// </summary>
    class ClassFormat : CSharpParserBaseVisitor<string>
    {
        public override string VisitClass_definition([NotNull] Class_definitionContext context)
        {
            GetClassName(context);
            var classMembers = context.class_body().class_member_declarations();
            if (classMembers != null)
            {
                GetFields(classMembers);
                GetProperties(classMembers);
                GetFunction(classMembers);
            }
            return "";
        }

        private string GetClassName(Class_definitionContext context)
        {
            int end;
            int start = context.start.StartIndex;
            if(context.class_base() != null)
            {
                end = context.class_base().stop.StopIndex;
            }
            else
            {
                end = context.identifier().stop.StopIndex;
            }
            return context.Start.InputStream.GetText(new Interval(start, end));
        }

        private List<string> GetFields(Class_member_declarationsContext context)
        {
            List<string> fields = new List<string>();
            var classMembers = context.class_member_declaration();
            foreach(var item in classMembers)
            {
                var typed = item.common_member_declaration().typed_member_declaration();
                if(typed != null && typed.field_declaration() != null)
                {
                    int start = item.Start.StartIndex;
                    int stop = item.Stop.StopIndex;
                    fields.Add(item.Start.InputStream.GetText(new Interval(start, stop)));
                }
            }
            return fields;
        }

        private List<string> GetProperties(Class_member_declarationsContext context)
        {
            List<string> properties = new List<string>();
            var classMembers = context.class_member_declaration();
            foreach (var item in classMembers)
            {
                var typed = item.common_member_declaration().typed_member_declaration();
                if (typed != null && typed.property_declaration() != null)
                {
                    int start = item.Start.StartIndex;
                    int stop = item.Stop.StopIndex;
                    
                    properties.Add(item.Start.InputStream.GetText(new Interval(start, stop)));
                }
            }
            return properties;
        }

        private List<string> GetFunction(Class_member_declarationsContext context)
        {
            List<string> functions = new List<string>();
            var classMembers = context.class_member_declaration();
            foreach (var item in classMembers)
            {
                var typed = item.common_member_declaration().typed_member_declaration();
                if ((typed != null && typed.method_declaration() != null) 
                    || item.common_member_declaration().method_declaration() != null) 
                {
                    int start = item.Start.StartIndex;
                    int stop = item.Stop.StopIndex;

                    functions.Add(item.Start.InputStream.GetText(new Interval(start, stop)));
                }
            }
            return functions;
        }


    }
}
