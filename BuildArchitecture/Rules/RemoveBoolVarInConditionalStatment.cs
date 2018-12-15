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
            bool isBoolConditional = (context.GetText().Contains("true") || context.GetText().Contains("false"));
            bool isCheckContext = !context.InRule(RuleContextType.VARIABLE_INITIALIZERCONTEXT) && !context.InRule(RuleContextType.EQUALITY_EXPRESSIONCONTEXT)
                && !context.InRule(RuleContextType.LOCAL_VARIABLE_INITIALIZERCONTEXT) && !context.InRule(RuleContextType.RETURNSTATEMENTCONTEXT);
            if (isBoolConditional && isCheckContext)
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
                    ErrorCode = "WA0003",
                    ReplaceCode = replaceCodes,
                    DisplayText = "Simply statement",
                    ErrorMessage = "Bool return type in conditional statement could be simplied",
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
