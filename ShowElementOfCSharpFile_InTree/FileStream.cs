using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowElementOfCSharpFile_InTree
{
    public class FileStream : AntlrFileStream
    {
        public FileStream([NotNull] string fileName) : base(fileName)
        {
        }

        public virtual void UpdateFile(TokenStreamRewriter rewriter)
        {
            using (StreamWriter writer = new StreamWriter(this.fileName,false))
            {
                writer.Write(rewriter.GetText());
            }
        }
    }
}
