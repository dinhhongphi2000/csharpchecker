﻿using BuildArchitecture;
using NUnit.Framework;
using System.IO;
using TestBuildArchitecture.Visitor;

namespace TestBuildArchitecture
{
    [TestFixture]
    public class BaseScopeTest
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

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\GetNamespaceName_IsTrue_1.cs", TestName = "GetNamespaceName_IsTrue_1")]
        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\GetNamespaceName_IsTrue_2.cs", TestName = "GetNamespaceName_IsTrue_2")]
        public void GetNamespaceName_IsTrue(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetClass_definitionVisitor visitor = new GetClass_definitionVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);

            var classContext = visitor.ClassIdentity;
            var scope = classContext.Scope;
            var namespaceName = scope.GetNamespaceName();
            if (TestContext.CurrentContext.Test.Name == "GetNamespaceName_IsTrue_1")
                Assert.AreEqual("TestA.NameSpace", namespaceName);
            else
                Assert.AreEqual("TestA", namespaceName);
        }

        [Test]
        public void ResolveSymbolDeclareOnOtherFile()
        {
            string file1 = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\ResolveSymbolDeclareOnOtherFile_file1.cs";
            string file2 = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\ResolveSymbolDeclareOnOtherFile_file2.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file1, GetFileContent(file1));
            workSpace.RunSemeticAnalysis(file1);
            workSpace.InitOrUpdateParserTreeOfFile(file2, GetFileContent(file2));
            workSpace.RunSemeticAnalysis(file2);
            ResolveSymbolDeclareOnOtherFile visitor = new ResolveSymbolDeclareOnOtherFile();
            visitor.Visit(workSpace._parserRuleContextOfFile[file2]);
            Assert.AreNotEqual(null, visitor.Symbol);
            Assert.AreEqual("global.TestBuildArchitecture.Math", visitor.Symbol.GetFullyQualifiedName("."));
        }

        [Test]
        public void ResolveSymbolDeclareOnOtherFileAndInAnotherScope_NotFound()
        {
            string file1 = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\ResolveSymbolDeclareOnOtherFileAndInAnotherScope_NotFound_file1.cs";
            string file2 = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\ResolveSymbolDeclareOnOtherFileAndInAnotherScope_NotFound_file2.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file1, GetFileContent(file1));
            workSpace.RunSemeticAnalysis(file1);
            workSpace.InitOrUpdateParserTreeOfFile(file2, GetFileContent(file2));
            workSpace.RunSemeticAnalysis(file2);
            ResolveSymbolDeclareOnOtherFile visitor = new ResolveSymbolDeclareOnOtherFile();
            visitor.Visit(workSpace._parserRuleContextOfFile[file2]);
            Assert.AreEqual(null, visitor.Symbol);
        }
    }
}
