using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    internal class RemoveBoolVarInConditionalStatment
    {
        [Export(typeof(Equality_expressionContext))]
        public void CheckIfInsideHasNumber(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var relationalExpressionContext = ((Equality_expressionContext)context).relational_expression();
            bool isBoolConditional = (context.GetText().Contains("true") || context.GetText().Contains("false"));

            if (isBoolConditional && relationalExpressionContext.Length == 2)
            {
                List<ReplaceCodeInfomation> replaceCodes = new List<ReplaceCodeInfomation>()
                {
                    new ReplaceCodeInfomation()
                    {
                        Start = context.Start.StartIndex,
                        Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                        ReplaceCode = ReplaceCode(context.GetText())
                    }
                };

                error = new ErrorInformation()
                {
                    ErrorCode = "IF0001",
                    ReplaceCode = replaceCodes,
                    DisplayText = "Simply statement",
                    ErrorMessage = "UIT: Bool return type in conditional statement could be simplied",
                    StartIndex = context.Start.StartIndex,
                    Length = context.Stop.StopIndex - context.Start.StartIndex + 1
                };
            }

        }
        private string ReplaceCode(string s)
        {
            if (s[0] == '(') s = s.Remove(s.Length - 1, 1).Remove(0, 1);
            string varName = "";
            string flag = "";
            string result = "";
            if (s.Contains("=="))
            {
                string[] str = s.Replace("==","=").Split('=');
                foreach(var substr in str)
                {
                    if (bool.TryParse(substr, out bool value))
                    {
                        if (value) flag = "";
                        else flag = "!";
                    }
                    else varName = substr;
                }
            }
            else if (s.Contains("!="))
            {
                string[] str = s.Replace("!=", "=").Split('=');
                foreach (var substr in str)
                {
                    if (bool.TryParse(substr, out bool value))
                    {
                        if (value) flag = "!";
                        else flag = "";
                    }
                    else varName = substr;
                }
            }
            result = string.Format("{0}{1}", flag, varName);
            if (result.Contains("!!"))
            {
                return result.Remove(0, 2);
            }
            else return result;
        }
    }
}
