using BuildArchitecture.Gui;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace antlr4ShowTree
{
    public partial class Form1 : Form
    {
        TreeNode root;
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            root = new TreeNode();
            TreeViewer.GetTree(richTextBox1.Text, root);
            treeViewer.Nodes.Clear();
            treeViewer.Nodes.Add(root);
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
    }
}
