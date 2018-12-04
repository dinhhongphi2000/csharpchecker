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

        [Test]
        public void TestPrimitiveTypeOfVariable()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\TestPrimitiveTypeOfVariable.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunDefinedPhraseAllfile();
            workSpace.RunResolvePhraseAllFile();

            GetLocalVariable visitor = new GetLocalVariable();
            visitor.Visit(workSpace._parserRuleContextOfFile[file]);
            foreach(var item in visitor.Identifiers)
            {
                var type = (item.Symbol as VariableSymbol).GetSymbolType();
                Assert.AreEqual("int", type.GetName());
            }
        }

        [Test]
        public void GetStructTypeOfLocalVariable()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\GetStructTypeOfLocalVariable.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunDefinedPhraseAllfile();
            workSpace.RunResolvePhraseAllFile();

            GetLocalVariable visitor = new GetLocalVariable();
            visitor.Visit(workSpace._parserRuleContextOfFile[file]);
            foreach (var item in visitor.Identifiers)
            {
                var type = (item.Symbol as VariableSymbol).GetSymbolType() as StructSymbol;
                Assert.AreEqual("Math", type.GetName());
                Assert.AreEqual("global.TestBuildArchitecture.DataTest.Math", type.GetFullyQualifiedName("."));
            }
        }

        [Test]
        public void SetTypeForFieldInClass_Success()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\SetTypeForFieldInClass_Success.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunDefinedPhraseAllfile();
            workSpace.RunResolvePhraseAllFile();

            GetFieldInClassVisotor visitor = new GetFieldInClassVisotor();
            visitor.Visit(workSpace._parserRuleContextOfFile[file]);

            var symbol = visitor.Identifier.Symbol as FieldSymbol;
            var type = symbol.GetSymbolType() as ClassSymbol;
            Assert.IsInstanceOf(typeof(ClassSymbol), type);
            Assert.AreEqual("global.TestBuildArchitecture.B", type.GetFullyQualifiedName("."));
        }

        [Test]
        public void SetTypeForFieldInStruct_Success()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\SetTypeForFieldInStruct_Success.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunDefinedPhraseAllfile();
            workSpace.RunResolvePhraseAllFile();

            GetFieldInClassVisotor visitor = new GetFieldInClassVisotor();
            visitor.Visit(workSpace._parserRuleContextOfFile[file]);

            var symbol = visitor.Identifier.Symbol as FieldSymbol;
            var type = symbol.GetSymbolType() as ClassSymbol;
            Assert.IsInstanceOf(typeof(ClassSymbol), type);
            Assert.AreEqual("global.TestBuildArchitecture.B", type.GetFullyQualifiedName("."));
        }

        [Test]
        public void SetTypeForProperty_Success()
        {
            string file = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\SetTypeForProperty_Success_Dont_Inheritance.cs";
            workSpace.InitOrUpdateParserTreeOfFile(file, GetFileContent(file));
            workSpace.RunDefinedPhraseAllfile();
            workSpace.RunResolvePhraseAllFile();

            GetPropertyIdentityContext visitor = new GetPropertyIdentityContext();
            visitor.Visit(workSpace._parserRuleContextOfFile[file]);

            var symbol = visitor.IdentityContext.Symbol as FieldSymbol;
            Assert.AreEqual("int", symbol.GetSymbolType().GetName());
        }
    }
}
