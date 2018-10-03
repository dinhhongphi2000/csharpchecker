﻿using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Windows.Forms;

namespace BuildArchitecture.Gui
{
    internal class Listener : CSharpParserBaseListener
    {
        private TreeNode _currentNode;
        public Listener(TreeNode tree)
        {
            _currentNode = tree;
        }
        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            TreeViewerNodeMeta node = new TreeViewerNodeMeta();
            node.StartIndex = context.Start.StartIndex;
            node.StopIndex = context.Stop.StopIndex;
            if (_currentNode.Text == context.Parent?.GetType().Name)
            {
                _currentNode.Nodes.Add(context.GetType().Name);
                _currentNode = _currentNode.Nodes[_currentNode.Nodes.Count - 1];
                _currentNode.Tag = node;
            }
            else
            {
                if (_currentNode.Parent == null)
                {
                    _currentNode.Nodes.Add(context.GetType().Name);
                    _currentNode = _currentNode.Nodes[_currentNode.Nodes.Count - 1];
                    _currentNode.Tag = node;
                }
                else
                {
                    _currentNode.Parent.Nodes.Add(context.GetType().Name);
                    _currentNode = _currentNode.Parent.Nodes[_currentNode.Parent.Nodes.Count - 1];
                    _currentNode.Tag = node;
                }
            }
            if (context.ChildCount == 1 && context.GetChild(0).ChildCount <= 0)
            {
                node.Token = context.GetText();
                _currentNode.Nodes.Add(context.GetText());
                _currentNode.Nodes[0].Tag = node;
            }
        }

        public override void ExitEveryRule([NotNull] ParserRuleContext context)
        {
            base.ExitEveryRule(context);
            if (_currentNode.PrevNode != null)
                _currentNode = _currentNode.PrevNode;
            else
                _currentNode = _currentNode.Parent;
        }
    }

    public class TreeViewer
    {
        public static void GetTree(string text, TreeNode tree)
        {
            AntlrInputStream stream = new AntlrInputStream(text.ToString());
            GetTreeFromStream(stream, tree);
        }

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
