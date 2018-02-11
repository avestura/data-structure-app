using GuilanDataStructures.DataStructures;
using GuilanDataStructures.DataStructures.Generic;
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

namespace GuilanDataStructures.Projects.Project3
{
    /// <summary>
    /// Interaction logic for Treap.xaml
    /// </summary>
    public partial class Treap : Page
    {

        public bool TextBoxLoadOnce = false;

        public Treap()
        {
            InitializeComponent();
        }


        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!TextBoxLoadOnce)
            {
                //inputTextbox.Text = "a/7 b/6 c/5 d/4 e/3 f/2 g/1\na/1 b/2 c/3 d/4 e/5 f/6 g/7\na/3 b/6 c/4 d/7 e/2 f/5 g/1";
                inputTextbox.Text = "a/3 b/6 c/4 d/7 e/2 f/5 g/1";
                TextBoxLoadOnce = true;

                
                
            }
        }

        private void inputTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                warningText.Visibility = Visibility.Collapsed;
                baseTree.Visibility = Visibility.Visible;
                baseTree.Clear();

                string[] token = inputTextbox.Text.Split(' ');

                Treap<char> treap = null;

                for(int i = 0; i < token.Length; i++)
                {
                    var splitToken = token[i].Split('/');
                    char c = splitToken[0][0];
                    int priority = int.Parse(splitToken[1]);

                    if (treap == null)
                        treap = new Treap<char>(c, priority);
                    else
                    {
                        treap = treap.Insert(c, priority);
                    }

                }

                outputText.Text = treap.ConvertTreeToString();

                var root  = baseTree.AddRoot(treap.Data + "/" + treap.Priority);

                bool hasLeft = treap.Left != null;
                bool hasRight = treap.Right != null;

                if (hasRight && !hasLeft)
                {
                    var noElementTree = baseTree.AddNode(treap, root);
                    noElementTree.Content = new NullChild(true);
                }

                AddTree(treap.Left, root);
                AddTree(treap.Right, root);


                if (hasLeft && !hasRight)
                {
                    var noElementTree = baseTree.AddNode(treap, root);
                    noElementTree.Content = new NullChild(false);
                }




            }
            catch {
                warningText.Visibility = Visibility.Visible;
                baseTree.Visibility = Visibility.Collapsed;
                outputText.Text = "فرمت ورودی معتبر نیست";
            }
             
        }

        private void AddTree(Treap<char> tree, TreeNode parent)
        {
            if (tree == null) return;

            TreeNode node;


            node = baseTree.AddNode(tree, parent);
            var tBlock = new TextBlock();
            tBlock.Text = tree.Data.ToString() + " / " + tree.Priority ;
            tBlock.Foreground = Brushes.Black;
            node.Content = tBlock;

            bool hasLeft = tree.Left != null;
            bool hasRight = tree.Right != null;

            if (hasRight && !hasLeft)
            {
                var noElementTree = baseTree.AddNode(tree, node);
                noElementTree.Content = new NullChild(true);
            }

            if (tree.Left != null) AddTree(tree.Left, node);

            if (tree.Right != null) AddTree(tree.Right, node);


        

            if (hasLeft && !hasRight)
            {
                var noElementTree = baseTree.AddNode(tree, node);
                noElementTree.Content = new NullChild(false);
            }
       

           
      

          
        }

       
    }
}
