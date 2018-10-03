﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class ForStatement_LoopDontRun : BaseRule
    {
        private bool isInForLoop = false;
        private int initValue;
        public override void EnterNode([NotNull] ParserRuleContext context)
        {
            base.EnterNode(context);
        }
    }
}
