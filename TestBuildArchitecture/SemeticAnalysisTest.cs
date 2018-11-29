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

                if (i == identityList.Length - 1)
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
            Assert.AreSame(symbol, symbol.Resolve(symbol.GetName()));
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\CreatePropertySymbol_For_Class_Success_PropertyInClass.cs", TestName = "PropertyInClass")]
        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\CreatePropertySymbol_For_Class_Success_PropertyInStruct.cs", TestName = "PropertyInStruct")]
        public void CreatePropertySymbol_For_Class_And_Struct_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetPropertyIdentityContext visitor = new GetPropertyIdentityContext();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);
            var propertyIdentityContext = visitor.IdentityContext;

            //symbol and scope is setted for context
            Assert.IsInstanceOf(typeof(FieldSymbol), propertyIdentityContext.Symbol);
            var testName = TestContext.CurrentContext.Test.Name;
            if (testName == "PropertyInClass")
                Assert.IsInstanceOf(typeof(ClassSymbol), propertyIdentityContext.Scope);
            else
                Assert.IsInstanceOf(typeof(StructSymbol), propertyIdentityContext.Scope);

            //check property of property symbol
            var symbol = propertyIdentityContext.Symbol as FieldSymbol;
            Assert.AreEqual(propertyIdentityContext.GetText(), symbol.GetName());
            Assert.AreSame(propertyIdentityContext, symbol.DefNode);

            //find symbol in scope
            Assert.AreSame(symbol, propertyIdentityContext.Scope.Resolve(symbol.GetName()));
            if (testName == "PropertyInClass")
                Assert.AreEqual("global.TestBuildArchitecture.DataTest.Test.Property", symbol.GetFullyQualifiedName("."));
            else
                Assert.AreEqual("global.TestBuildArchitecture.DataTest.StructTest.Property", symbol.GetFullyQualifiedName("."));
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\CreateFieldSymbol_Success_For_Class_And_Struct_Success_Class.cs", TestName = "FieldInClass")]
        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\CreateFieldSymbol_Success_For_Class_And_Struct_Success_struct.cs", TestName = "FieldInStruct")]
        public void CreateFieldSymbol_Success_For_Class_And_Struct_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetIdentityContextOfFieldVisitor visitor = new GetIdentityContextOfFieldVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);
            var identifiers = visitor.Identifiers;

            foreach (var id in identifiers)
            {
                //symbol and scope is setted for context
                Assert.IsInstanceOf(typeof(FieldSymbol), id.Symbol);
                var testName = TestContext.CurrentContext.Test.Name;
                if (testName == "FieldInClass")
                    Assert.IsInstanceOf(typeof(ClassSymbol), id.Scope);
                else
                    Assert.IsInstanceOf(typeof(StructSymbol), id.Scope);

                //check property of property symbol
                var symbol = id.Symbol as FieldSymbol;
                Assert.AreEqual(id.GetText(), symbol.GetName());
                Assert.AreSame(id, symbol.DefNode);

                //find symbol in scope
                Assert.AreSame(symbol, id.Scope.Resolve(symbol.GetName()));
                Assert.AreEqual("global.TestBuildArchitecture.DataTest.Test." + id.GetText(), symbol.GetFullyQualifiedName("."));
            }
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\ParameterSymbolCreate_Success.cs")]
        public void ParameterSymbolCreate_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetFunctionParameterVisitor visitor = new GetFunctionParameterVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);

            var parametersContext = visitor.Parameters;
            foreach (var param in parametersContext)
            {
                var symbol = param.Symbol as ParameterSymbol;
                Assert.IsInstanceOf(typeof(FunctionSymbol), param.Scope);
                Assert.IsInstanceOf(typeof(ParameterSymbol), symbol);

                //check property of symbol
                Assert.AreEqual(param.GetText(), symbol.GetName());
                Assert.AreSame(param, symbol.DefNode);

                //check symbol in scope
                Assert.AreSame(symbol, param.Scope.Resolve(symbol.GetName()));
            }
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\FunctionSymbolCreate_Success.cs", TestName = "FunctionSymbolCreate_Success")]
        public void FunctionSymbolCreate_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetFunctionIdentityVisitor visitor = new GetFunctionIdentityVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);

            var functionIdentify = visitor.FunctionIdentifier;

            //symbol and scope is exactly typed
            Assert.IsInstanceOf(typeof(FunctionSymbol), functionIdentify.Symbol);
            Assert.IsInstanceOf(typeof(FunctionSymbol), functionIdentify.Scope);

            //check property of symbol
            var symbol = functionIdentify.Symbol as FunctionSymbol;
            Assert.AreEqual(functionIdentify.GetText(), symbol.GetName());
            Assert.AreSame(functionIdentify, symbol.DefNode);

            //symbol exists in parent scope
            Assert.AreSame(symbol, symbol.Resolve(symbol.GetName()));
            Assert.AreEqual("global.TestBuildArchitecture.DataTest.Test.Plus", symbol.GetFullyQualifiedName("."));
        }

        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\LocalVariableSymbol_Create_Success_Create_LocalSymbol_In_Function.cs", TestName = "Create_LocalSymbol_In_Function")]
        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\LocalVariableSymbol_Create_Success_Create_LocalSymbol_In_Block_No_Name.cs", TestName = "Create_LocalSymbol_In_Block_No_Name")]
        [TestCase(@"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\DataTest\LocalVariableSymbol_Create_Success_Create_LocalSymbol_In_For_Loop.cs", TestName = "Create_LocalSymbol_In_For_Loop")]
        public void LocalVariableSymbol_Create_Success(string cSharpFilePath)
        {
            workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
            workSpace.RunSemeticAnalysis(cSharpFilePath);

            GetVariableIdentityLocalVisitor visitor = new GetVariableIdentityLocalVisitor();
            visitor.Visit(workSpace._parserRuleContextOfFile[cSharpFilePath]);
            var identifierList = visitor.IdentitiesContext;

            foreach (var id in identifierList)
            {
                //check valid scope and symbol
                Assert.IsInstanceOf(typeof(VariableSymbol), id.Symbol);
                var testName = TestContext.CurrentContext.Test.Name;
                Assert.IsInstanceOf(typeof(LocalScope), id.Scope);

                //check property of symbol
                var symbol = id.Symbol as VariableSymbol;
                Assert.AreEqual(id.GetText(), symbol.GetName());
                Assert.AreSame(id, symbol.DefNode);

                //check symbol exists in scope
                Assert.AreSame(symbol, id.Scope.Resolve(symbol.GetName()));
            }
        }

        //[TestCase("", TestName = "NotFound_Class_In_Other_Namespace_Not_Use_using")]
        //[TestCase("", TestName = "NotFound_Function_In_Other_Class")]
        //[TestCase("", TestName = "NotFound_Variable_In_Other_Block_Same_Function")]
        //public void Exit_Block_Dont_FindSymbol(string cSharpFilePath)
        //{
        //    workSpace.InitOrUpdateParserTreeOfFile(cSharpFilePath, GetFileContent(cSharpFilePath));
        //    workSpace.RunSemeticAnalysis(cSharpFilePath);

        //    var testName = TestContext.CurrentContext.Test.Name;
        //    switch (testName)
        //    {
        //        case "NotFound_Class_In_Other_Namespace_Not_Use_using":
        //            break;
        //        case "NotFound_Function_In_Other_Class":
        //            break;
        //        case "NotFound_Variable_In_Other_Block_Same_Function":
        //            break;
        //    }
        //}
    }
}
