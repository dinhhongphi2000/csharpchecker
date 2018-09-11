using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace Caculator
{
    class EvalVisitor : CaculatorBaseVisitor<int>
    {
        Dictionary<String, int> memory = new Dictionary<string, int>();

        public EvalVisitor()
        {
        }

        public override int VisitBlank([NotNull] CaculatorParser.BlankContext context)
        {
            return Visit(context);
        }

        public override int VisitPrintExpr([NotNull] CaculatorParser.PrintExprContext context)
        {
            int value = Visit(context.expr());
            Console.WriteLine(value.ToString());
            return 0;
        }

        public override int VisitAssign([NotNull] CaculatorParser.AssignContext context)
        {
            String id = context.ID().GetText();
            int value = Visit(context.expr());
            memory.Add(id, value);
            return value;
        }

        public override int VisitParens([NotNull] CaculatorParser.ParensContext context)
        {
            return Visit(context.expr());
        }

        public override int VisitMulDiv([NotNull] CaculatorParser.MulDivContext context)
        {
            int left = Visit(context.expr(0)); // get value of left subexpression
            int right = Visit(context.expr(1)); // get value of right subexpression
            if (context.op.Type == CaculatorParser.MUL)
                return left * right;
            return left / right; // must be DIV
        }

        public override int VisitAddSub([NotNull] CaculatorParser.AddSubContext context)
        {
            int left = Visit(context.expr(0)); // get value of left subexpression
            int right = Visit(context.expr(1)); // get value of right subexpression
            if (context.op.Type == CaculatorParser.ADD)
                return left + right;
            return left - right; // must be SUB
        }

        public override int VisitId([NotNull] CaculatorParser.IdContext context)
        {
            String id = context.ID().GetText();
            if (memory.ContainsKey(id))
                return memory[id];
            return 0;
        }

        public override int VisitInt([NotNull] CaculatorParser.IntContext context)
        {
            return int.Parse(context.INT().GetText());
        }

        public override int VisitProg([NotNull] CaculatorParser.ProgContext context)
        {
            return base.VisitProg(context);
        }
    }
}
