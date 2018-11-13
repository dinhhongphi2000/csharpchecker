using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChecker.SolutionLoad
{
    class ProjectInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<FileInfo> FileInfos { get; set; }

        public ProjectInfo(string name, string path, List<FileInfo> fileInfo)
        {
            Name = name;
            Path = path;
            FileInfos = fileInfo;
        }

        public ProjectInfo(string name, string path)
        {
            Name = name;
            Path = path;
            FileInfos = new List<FileInfo>();
        }
    }
}
