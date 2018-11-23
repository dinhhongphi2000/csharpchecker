using BuildArchitecture;
using BuildArchitecture.Semetic.V2;
using NUnit.Framework;
using System.IO;
using TestBuildArchitecture.Visitor;
using static BuildArchitecture.CSharpParser;

namespace TestBuildArchitecture
{
    [TestFixture]
    public class SemeticAnalysisTest
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

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\EnterNamespace_Create_NamespaceSymbol_Success.cs")]
        public void EnterNamespace_Create_NamespaceSymbol_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            Qualified_identifierVisitor visitor = new Qualified_identifierVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);

            var node = visitor.NameSpaceNode;
            Assert.AreNotEqual(null, node, "node != null");

            Assert.IsInstanceOf(typeof(Qualified_identifierContext), node);
            var identityList = (node as Qualified_identifierContext).identifier();

            for (var i = 0; i < identityList.Length; i++)
            {
                var identity = identityList[i];

                //check exist Symbol and scope in context
                Assert.AreNotEqual(null, identity.Symbol);
                Assert.AreNotEqual(null, identity.Scope);
                Assert.IsInstanceOf(typeof(NamespaceSymbol), identity.Symbol);

                //check property of symbol
                Assert.AreNotEqual(null, identity.Symbol, "identity.Symbol != null");
                Assert.AreEqual(identity.GetText(), identity.Symbol.GetName());

                //check property of scope

                if(i == identityList.Length - 1)
                {
                    //check FullQualifiedName
                    Assert.AreEqual("global.TestBuildArchitecture.DataTest", identity.Symbol.GetFullyQualifiedName("."));
                }
            }
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\EnterClass_definition_Create_Class_Symbol_Success.cs")]
        public void EnterClass_definition_Create_Class_Symbol_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetClass_definitionVisitor classDefinition = new GetClass_definitionVisitor();
            classDefinition.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);

            var classIdentity = classDefinition.ClassIdentity;
            Assert.IsInstanceOf(typeof(IdentifierContext), classIdentity);

            //check exist symbol and scope
            Assert.IsInstanceOf(typeof(ClassSymbol), classIdentity.Symbol);
            Assert.IsInstanceOf(typeof(ClassSymbol), classIdentity.Scope);

            //check property of symbol
            var symbol = classIdentity.Symbol as ClassSymbol;
            Assert.AreEqual(classIdentity.GetText(), symbol.GetName());
            Assert.AreNotEqual(null, symbol.DefNode);
            Assert.AreEqual("global.TestBuildArchitecture.DataTest.Test", symbol.GetFullyQualifiedName("."));

            //check property of scope
            Assert.AreNotEqual(null, symbol.Resolve(symbol.GetName()));
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\CreateNestedClassSymbol_Success.cs")]
        public void CreateNestedClassSymbol_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            CreateNestedClassSymbol_SuccessVisitor visitor = new CreateNestedClassSymbol_SuccessVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);
            var classIdentity = visitor.ClassIdentity;

            //check property of symbol
            var symbol = classIdentity.Symbol as ClassSymbol;
            Assert.AreEqual("global.TestBuildArchitecture.DataTest.MainClass.NestedClass", symbol.GetFullyQualifiedName("."));
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\CreateStructSymbol_Success.cs")]
        public void CreateStructSymbol_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetStructIdentityVisitor visitor = new GetStructIdentityVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);
            var structIdentity = visitor.StructIdentity;

            //check whether symbol and scope exist
            Assert.IsInstanceOf(typeof(StructSymbol), structIdentity.Symbol);
            Assert.IsInstanceOf(typeof(StructSymbol), structIdentity.Scope);

            //check property of struct symbol
            var symbol = structIdentity.Symbol as StructSymbol;
            Assert.AreEqual(structIdentity.GetText(), symbol.GetName());
            Assert.AreSame(structIdentity, symbol.DefNode);
            Assert.AreEqual("global.TestBuildArchitecture.DataTest.StructTest1", symbol.GetFullyQualifiedName("."));

            ////check property scope
            //Assert.AreSame(symbol, symbol.Resolve(symbol.GetName()));
        }
    }
}
