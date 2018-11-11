using BuildArchitecture.Gui;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace antlr4ShowTree
{
    public partial class Form1 : Form
    {
        TreeNode root;
        TreeViewerNodeMeta _previousSelection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            var node = TreeViewer.FindNode(root, richTextBox1.SelectionStart);
            if (node != null)
            {
                node.EnsureVisible();
                node.Checked = true;
                treeViewer.SelectedNode = node;
            }
        }

        private void treeViewer_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //remove backcolor of previous node
            if (treeViewer.SelectedNode != null)
            {
                treeViewer.SelectedNode.BackColor = Color.White;
            }
            e.Node.BackColor = Color.Blue;
        }

        private void goToTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewer.SelectedNode;
            var nodeinfo = (TreeViewerNodeMeta)selectedNode.Tag ?? null;
            //remove old selection highlight
            if (_previousSelection != null)
            {
                richTextBox1.Select(_previousSelection.StartIndex, _previousSelection.StopIndex - _previousSelection.StartIndex + 1);
                richTextBox1.SelectionBackColor = Color.White;
            }
            if (nodeinfo != null)
            {
                richTextBox1.Select(nodeinfo.StartIndex, nodeinfo.StopIndex - nodeinfo.StartIndex + 1);
                richTextBox1.SelectionBackColor = Color.Yellow;
                _previousSelection = nodeinfo;
            }
            treeViewer.SelectedNode = selectedNode;
        }

        private void btnReloadTree_Click(object sender, EventArgs e)
        {
            root = new TreeNode();
            TreeViewer.GetTree(richTextBox1.Text, root);
            treeViewer.Nodes.Clear();
            treeViewer.Nodes.Add(root);
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewer.SelectedNode;
            if(selectedNode != null)
            {
                selectedNode.ExpandAll();
            }
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewer.SelectedNode;
            if (selectedNode != null)
            {
                selectedNode.Collapse(false);
            }
        }

        private void getNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewer.SelectedNode;
            if (selectedNode != null)
            {
                Clipboard.SetText(selectedNode.Text);
            }
        }
    }
}
