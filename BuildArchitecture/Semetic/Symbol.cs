using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic
{
    delegate string testc();

    public class Symbol
    {
        public string Name { get; set; }
        public HashSet<string> Modifier { get; set; }
        public string Alias { get; set; }

        /// <summary>
        /// Where's Symbol?
        /// </summary>
        public string FullName { get; protected set; }

        public Symbol(string name, HashSet<string> modifier = null, string alias = null)
        {
            this.Name = name;
            this.Modifier = modifier;
            this.Alias = alias;
        }

        public Symbol(string name, string[] modifier = null, string alias = null)
        {
            this.Name = name;
            this.Modifier = new HashSet<string>(modifier);
            this.Alias = alias;
        }

        /// <summary>
        /// Support get parameter list in GenericType of class, function
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Parameter list was declared</returns>
        protected static List<string> GetGenericParameters(Type_parameter_listContext context)
        {
            if (context == null)
                return null;

            Type_parameterContext[] parameterList = context.type_parameter();
            List<string> parameters = new List<string>();
            foreach(var param in parameterList)
            {
                parameters.Add(param.identifier().GetText());
            }
            return parameters;
        }

        /// <summary>
        /// Support get parameter list in GenericType of delegate, interface
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Parameter list was declared</returns>
        protected static List<string> GetGenericParameters(Variant_type_parameter_listContext context)
        {
            if (context == null)
                return null;

            Variant_type_parameterContext[] parameterList = context.variant_type_parameter();
            List<string> parameters = new List<string>();
            foreach (var param in parameterList)
            {
                parameters.Add(param.identifier().GetText());
            }
            return parameters;
        }

        /// <summary>
        /// Get allowed type of parameter in generic type
        /// class Test<T> where T : class
        /// </summary>
        /// <param name="context"></param>
        /// <returns>[{T,class}]</returns>
        protected static Dictionary<string, string> GetGenericParameterConstraint(Type_parameter_constraints_clausesContext context)
        {
            if (context == null)
                return null;

            Type_parameter_constraints_clauseContext[] constraintClauses = context.type_parameter_constraints_clause();
            Dictionary<string, string> constraints = new Dictionary<string, string>();
            foreach (var constraint in constraintClauses)
            {
                constraints.Add(constraint.identifier().GetText(), constraint.type_parameter_constraints().GetText());
            }
            return constraints;
        }
    }
}
