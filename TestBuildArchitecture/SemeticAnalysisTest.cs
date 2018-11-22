using Antlr4.Runtime.Tree;
using BuildArchitecture;
using NUnit.Framework;
using System.IO;

namespace TestBuildArchitecture
{
    [TestFixture]
    class SemeticAnalysisTest
    {
        WorkSpace workSpace;

        [SetUp]
        public void InitTestForFile()
        {
            workSpace = new WorkSpace();
        }

        public string GetFileContent(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }

        public void CreateNamespaceScopeSuccess(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

        }
    }
}
