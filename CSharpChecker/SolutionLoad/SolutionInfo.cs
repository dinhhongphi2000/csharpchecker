using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChecker.SolutionLoad
{
    class SolutionInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<ProjectInfo> ProjectInfos { get; set; }

        public SolutionInfo(string name, string path, List<ProjectInfo> projectInfos)
        {
            Name = name;
            Path = path;
            ProjectInfos = projectInfos;
        }

        public SolutionInfo(string name, string path)
        {
            Name = name;
            Path = path;
            ProjectInfos = new List<ProjectInfo>();
        }
    }
}
