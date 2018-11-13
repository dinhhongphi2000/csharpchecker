using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChecker.SolutionLoad
{
    class FileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }

    }
}
