using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModifyTokenTree
{
    class MyToken : CommonToken
    {
        public MyToken(int type, string text) : base(type, text)
        {
        }

        public MyToken([NotNull] Tuple<ITokenSource, ICharStream> source, int type, int channel, int start, int stop) : base(source, type, channel, start, stop)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
