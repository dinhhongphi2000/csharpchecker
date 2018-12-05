using BuildArchitecture;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture
{
    [TestFixture]
    public class WorkspaceTest
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

        [Test]
        public void RunRules_GetError_from_Rule_Success_Test()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\RunRules_GetError_from_Rule_Success_Test.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunRules(file);

            var error = workSpace.GetErrors();
            Assert.AreEqual(1, error[file].Count);
        }
    }
}
