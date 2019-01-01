using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BuildArchitecture
{
    public class Utils
    {
        /** Return first ancestor node up the chain towards the root that has ruleName.
         *  Search includes the current node.
         */
        public static ParserRuleContext GetAncestor(Parser parser, ParserRuleContext ctx, string ruleName)
        {
            int ruleIndex = parser.GetRuleIndex(ruleName);
            return GetAncestor(ctx, ruleIndex);
        }

        /** Return first ancestor node up the chain towards the root that has the rule index.
         *  Search includes the current node.
         */
        public static ParserRuleContext GetAncestor(ParserRuleContext t, int ruleIndex)
        {
            while (t != null)
            {
                if (t.RuleIndex == ruleIndex)
                {
                    return t;
                }
                t = (ParserRuleContext)t.Parent;
            }
            return null;
        }

        /** Return first ancestor node up the chain towards the root that is clazz.
         *  Search includes the current node.
         */
        public static ParserRuleContext GetFirstAncestorOfType(ParserRuleContext t, Type clazz)
        {
            while (t != null)
            {
                if (t.GetType() == clazz)
                {
                    return t;
                }
                t = (ParserRuleContext)t.Parent;
            }
            return null;
        }

        public static FieldInfo[] GetAllFields(Type clazz)
        {

            List<FieldInfo> fields = new List<FieldInfo>();
            while (clazz != null && clazz != typeof(object))
            {
                foreach (FieldInfo f in clazz.GetFields())
                {
                    fields.Add(f);
                }
                clazz = clazz.BaseType;
            }
            return fields.ToArray();
        }

        public static FieldInfo[] GetAllAnnotatedFields(Type clazz)
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            while (clazz != null && clazz != typeof(object))
            {
                foreach (FieldInfo f in clazz.GetFields())
                {
                    if (f.CustomAttributes.Count() > 0)
                    {
                        fields.Add(f);
                    }
                }
                clazz = clazz.BaseType;
            }
            return fields.ToArray();
        }

        /** Order of scopes not guaranteed but is currently breadth-first according
         *  to nesting depth. Gets ScopedSymbols only.
         */
        public static void GetAllNestedScopedSymbols(IScope scope, List<IScope> scopes)
        {
            scopes.AddRange(scope.GetNestedScopedSymbols());
            foreach (IScope s in scope.GetNestedScopedSymbols())
            {
                GetAllNestedScopedSymbols(s, scopes);
            }
        }

        /** Order of scopes not guaranteed but is currently breadth-first according
         *  to nesting depth. Gets ScopedSymbols and non-ScopedSymbols.
         */
        public static void GetAllNestedScopes(IScope scope, List<IScope> scopes)
        {
            scopes.AddRange(scope.GetNestedScopes());
            foreach (IScope s in scope.GetNestedScopes())
            {
                GetAllNestedScopes(s, scopes);
            }
        }

        /** Return a string of scope names with the "stack" growing to the left
         *  E.g., myblock:mymethod:myclass.
         *  string includes arg scope in string.
         */
        public static string ToScopeStackString(IScope scope, string separator)
        {
            List<IScope> scopes = scope.GetEnclosingPathToRoot();
            return JoinScopeNames(scopes, separator);
        }

        /** Return a string of scope names with the "stack" growing to the right.
         *  E.g., myclass:mymethod:myblock.
         *  string includes arg scope in string.
         */
        public static string ToQualifierString(IScope scope, string separator)
        {
            List<IScope> scopes = scope.GetEnclosingPathToRoot();
            scopes.Reverse();
            return JoinScopeNames(scopes, separator);
        }

        public static string ToString(IScope s, int level)
        {
            if (s == null) return "";
            StringBuilder buf = new StringBuilder();
            buf.Append(Tab(level));
            buf.Append(s.GetName());
            buf.Append("\n");
            level++;
            foreach (ISymbol sym in s.GetSymbols())
            { // print out all symbols but not scopes
                if (!(sym is IScope))
                {
                    buf.Append(Tab(level));
                    buf.Append(sym);
                    buf.Append("\n");
                }
            }
            foreach (IScope nested in s.GetNestedScopes())
            { // includes named scopes and local scopes
                buf.Append(ToString(nested, level));
            }
            return buf.ToString();
        }

        public static string ToString(IScope s)
        {
            return ToString(s, 0);
        }

        //  Generic filtering, mapping, joining that should be in the standard library but aren't

        public static T FindFirst<T>(List<T> data, Predicate<T> pred)
        {
            if (data != null)
                foreach (T x in data)
                {
                    if (pred(x))
                    {
                        return x;
                    }
                }
            return default(T);
        }

        public static List<T> Filter<T>(List<T> data, Predicate<T> pred)
        {
            List<T> output = new List<T>();
            if (data != null)
                foreach (T x in data)
                {
                    if (pred(x))
                    {
                        output.Add(x);
                    }
                }
            return output;
        }

        public static HashSet<T> Filter<T>(IEnumerable<T> data, Predicate<T> pred)
        {
            HashSet<T> output = new HashSet<T>();
            foreach (T x in data)
            {
                if (pred(x))
                {
                    output.Add(x);
                }
            }
            return output;
        }

        public static List<R> Map<T, R>(IEnumerable<T> data, Func<T, R> getter)
        {
            List<R> output = new List<R>();
            if (data != null)
                foreach (T x in data)
                {
                    output.Add(getter.Invoke(x));
                }
            return output;
        }

        public static List<R> Map<T, R>(T[] data, Func<T, R> getter)
        {
            List<R> output = new List<R>();
            if (data != null)
                foreach (T x in data)
                {
                    output.Add(getter.Invoke(x));
                }
            return output;
        }

        public static string Join<T>(IEnumerable<T> data, string separator)
        {
            return Join(data.GetEnumerator(), separator, "", "");
        }

        public static string Join<T>(IEnumerable<T> data, string separator, string left, string right)
        {
            return Join(data.GetEnumerator(), separator, left, right);
        }

        public static string Join<T>(IEnumerator<T> iter, string separator, string left, string right)
        {
            StringBuilder buf = new StringBuilder();

            while (iter.MoveNext())
            {
                buf.Append(iter.Current);
                if (iter.MoveNext())
                {
                    buf.Append(separator);
                }
            }

            return left + buf.ToString() + right;
        }

        public static string Join<T>(T[] array, string separator)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < array.Length; ++i)
            {
                builder.Append(array[i]);
                if (i < array.Length - 1)
                {
                    builder.Append(separator);
                }
            }

            return builder.ToString();
        }

        public static string Tab(int n)
        {
            StringBuilder buf = new StringBuilder();
            for (int i = 1; i <= n; i++) buf.Append("    ");
            return buf.ToString();
        }

        public static string JoinScopeNames(List<IScope> scopes, string separator)
        {
            if (scopes == null || scopes.Count == 0)
            {
                return "";
            }
            StringBuilder buf = new StringBuilder();
            buf.Append(scopes[0].GetName());
            for (int i = 1; i < scopes.Count; i++)
            {
                IScope s = scopes[i];
                buf.Append(separator);
                buf.Append(s.GetName());
            }
            return buf.ToString();
        }
    }
}
