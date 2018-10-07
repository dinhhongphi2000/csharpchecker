using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Windows.Forms;

namespace BuildArchitecture.Gui
{
    /// <summary>
    /// Listener's mission builds Tree of Parser contexts
    /// </summary>
    internal class Listener : CSharpParserBaseListener
    {
        private TreeNode _currentNode;
        public Listener(TreeNode tree)
        {
            _currentNode = tree;
        }

        /// <summary>
        /// Create a node when a rule is visited
        /// </summary>
        /// <param name="context"></param>
        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            //create metadata to save some information of context
            TreeViewerNodeMeta node = new TreeViewerNodeMeta();
            node.StartIndex = context.Start.StartIndex;
            node.StopIndex = context.Stop.StopIndex;

            //current node is parent of context, add context to child list of current node
            if (_currentNode.Text == context.Parent?.GetType().Name)
            {
                _currentNode.Nodes.Add(context.GetType().Name);
                _currentNode = _currentNode.Nodes[_currentNode.Nodes.Count - 1];
                _currentNode.Tag = node;
            }
            else
            {
                //current node is root node
                if (_currentNode.Parent == null)
                {
                    _currentNode.Nodes.Add(context.GetType().Name);
                    _currentNode = _currentNode.Nodes[_currentNode.Nodes.Count - 1];
                    _currentNode.Tag = node;
                }
                else
                {
                    //current node and context node is same lever
                    _currentNode.Parent.Nodes.Add(context.GetType().Name);
                    _currentNode = _currentNode.Parent.Nodes[_currentNode.Parent.Nodes.Count - 1];
                    _currentNode.Tag = node;
                }
            }
            //context is leaf node
            if (context.ChildCount == 1 && context.GetChild(0).ChildCount <= 0)
            {
                node.Token = context.GetText();
                _currentNode.Nodes.Add(context.GetText());
                _currentNode.Nodes[0].Tag = node;
            }
        }

        /// <summary>
        /// Return previous or parent node
        /// </summary>
        /// <param name="context"></param>
        public override void ExitEveryRule([NotNull] ParserRuleContext context)
        {
            base.ExitEveryRule(context);
            if (_currentNode.PrevNode != null)
                _currentNode = _currentNode.PrevNode;
            else
                _currentNode = _currentNode.Parent;
        }
    }
}