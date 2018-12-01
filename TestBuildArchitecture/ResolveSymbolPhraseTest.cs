using BuildArchitecture;
using BuildArchitecture.Semetic.V2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBuildArchitecture.Visitor;
using static BuildArchitecture.CSharpParser;

namespace TestBuildArchitecture
{
    [TestFixture]
    public class ResolveSymbolPhraseTest
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
        public void SetTypeOfField_Success()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\SetTypeOfField_Success.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunDefinedPhraseAllfile();
            workSpace.RunResolvePhraseAllFile();

            GetFieldInClassVisotor visitor = new GetFieldInClassVisotor();
            visitor.Visit(workSpace._parserRuleContextOfFile[file]);
            var symbol = visitor.Identifier.Symbol as FieldSymbol;
            Assert.IsInstanceOf<ClassSymbol>(symbol.GetSymbolType());
            var type = symbol.GetSymbolType() as ClassSymbol;
            Assert.AreEqual("global.TestBuildArchitecture.B", type.GetFullyQualifiedName("."));
        }
    }
}
