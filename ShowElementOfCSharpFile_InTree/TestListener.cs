using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace ShowElementOfCSharpFile_InTree
{
    class TestListener : CSharpParserBaseListener
    {
        TreeScope _tree;
        TreeScope currentNode;

        public TestListener(TreeScope root)
        {
            if (root == null)
                throw new ArgumentNullException("Parameter root must not null");
            this._tree = root;
        }
        public override void EnterCompilation_unit([NotNull] CSharpParser.Compilation_unitContext context)
        {
            this.currentNode = _tree;
            base.EnterCompilation_unit(context);
        }
        public override void EnterNamespace([NotNull] CSharpParser.NamespaceContext context)
        {

            string name = context.qualified_identifier().GetText();
            TreeScope node = currentNode.AddChild(name, "namespace");
            if (node != null)
            {
                this.currentNode = node;
            }
            base.EnterNamespace(context);
        }

        public override void ExitNamespace([NotNull] CSharpParser.NamespaceContext context)
        {
            this.currentNode = this.currentNode.Parent;
            base.ExitNamespace(context);
        }

        public override void EnterClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            TreeScope node = currentNode.AddChild(context.identifier().GetText(), "class");
            if (node != null)
            {
                this.currentNode = node;
            }
            base.EnterClass_definition(context);
        }

        public override void ExitClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            this.currentNode = this.currentNode.Parent;
            base.ExitClass_definition(context);
        }

        public override void EnterMethod_member_name([NotNull] CSharpParser.Method_member_nameContext context)
        {
            TreeScope node = currentNode.AddChild(context.GetText(), "method");
            if (node != null)
            {
                this.currentNode = node;
            }
            base.EnterMethod_member_name(context);
        }

        public override void ExitMethod_member_name([NotNull] CSharpParser.Method_member_nameContext context)
        {
            this.currentNode = this.currentNode.Parent;
            base.ExitMethod_member_name(context);
        }

        public void ShowTree(TreeScope node, System.IO.TextWriter output)
        {
            ShowNodeName(node, output);
            if (node.Child != null)
                foreach (var item in node.Child)
                {
                    ShowTree(item, output);
                }
        }

        private void ShowNodeName(TreeScope node, System.IO.TextWriter writer)
        {
            string prefix = "";
            for (int i = 0; i <= node.Level; i++)
                prefix += "|  ";
            writer.WriteLine("{0}{1} {2}", prefix, node.Type, node.Name);
        }
    }
}
