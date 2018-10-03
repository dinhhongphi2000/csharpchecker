using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Windows.Forms;

namespace BuildArchitecture.Gui
{
    public class TreeViewer
    {
        public static void GetTree(string text, TreeNode tree)
        {
            AntlrInputStream stream = new AntlrInputStream(text.ToString());
            GetTreeFromStream(stream, tree);
        }

        /// <summary>
        /// Create TreeNode from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="tree"></param>
        private static void GetTreeFromStream(ICharStream stream, TreeNode tree)
        {
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);
            CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
            Listener listener = new Listener(tree);
            IParseTree parserTree = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(listener, startContext);
        }


        /// <summary>
        /// Find a node in TreeNode that it's context match token
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="tokenStart"></param>
        /// <returns></returns>
        public static TreeNode FindNode(TreeNode tree, int tokenStart)
        {
            try
            {
                var current = tree;
                TreeViewerNodeMeta meta = current.Tag as TreeViewerNodeMeta;
                if ((current.Tag != null) && meta.StartIndex >= tokenStart)
                {
                    if(current.PrevNode != null)
                        return current.PrevNode;
                    else
                    {
                        return current.Parent;
                    }
                }
                else
                {
                    TreeNode result;
                    foreach(TreeNode node in current.Nodes)
                    {
                        result = FindNode(node, tokenStart);
                        if (result != null)
                            return result;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
