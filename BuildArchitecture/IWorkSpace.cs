using Antlr4.Runtime.Tree;
using BuildArchitecture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture
{
    public interface IWorkSpace
    {
        ProjectContext CurrentProject { get; set; }

        string CurrentFile { get; set; }

        /// <summary>
        /// Update Syntax Tree of chaged file
        /// </summary>
        /// <param name="filePath">Path of file</param>
        /// <param name="content">Content of file</param>
        void UpdateTree(string content = null);

        /// <summary>
        /// Check code by running all rule on changed file
        /// </summary>
        /// <param name="filePathChanged">Path of file is changed</param>
        List<ErrorInformation> RunRules();

        void SetListener(IParseTreeListener listener);
    }
}
