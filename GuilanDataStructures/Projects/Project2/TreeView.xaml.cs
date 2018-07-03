using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeContainer;
using GraphLayout;
using GuilanDataStructures.DataStructures.Huffman;

namespace GuilanDataStructures.Projects.Project2
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeView : Page
    {

        public CompressedText ReturnPage { get; set; }
        public DataStructures.Huffman.BinaryTree Tree { get; set; }

        public TreeView(CompressedText r, BinaryTree t)
        {
            ReturnPage = r; Tree = t;
            InitializeComponent();
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            var tree = Tree;
            var root = baseTree.AddRoot(tree);

            var tBlock = new TextBlock();
            tBlock.FlowDirection = FlowDirection.RightToLeft;
            tBlock.Text = tree.Repeat.ToString();
            tBlock.FontSize = 13;
            tBlock.Foreground = Brushes.Green;

            root.Content = tBlock;
            AddTree(tree.Left, root, Brushes.DodgerBlue);
            AddTree(tree.Right, root, Brushes.PaleVioletRed);

        }

        private void AddTree(BinaryTree tree, TreeNode parent, Brush color)
        {
            if (tree == null) return;

            TreeNode node;

            if (tree.HasCharacter)
            {
                node = baseTree.AddNode(tree, parent);
                node.Content = new CharFrequency(tree.Character.Value, tree.Repeat, (color == Brushes.PaleVioletRed) ? false:true);
            }
            else
            {
                node = baseTree.AddNode(tree, parent);

                var tBlock = new TextBlock();
                tBlock.FlowDirection = FlowDirection.RightToLeft;
                tBlock.Text = tree.Repeat.ToString();
                tBlock.FontSize = 13;
                tBlock.Foreground = color;

                node.Content = tBlock;
            }

            if (tree.Right != null) AddTree(tree.Right, node, Brushes.PaleVioletRed);

            if (tree.Left != null) AddTree(tree.Left, node, Brushes.DodgerBlue);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReturnPage.Host.hostFrame.Navigate(ReturnPage);
        }
    }
}
