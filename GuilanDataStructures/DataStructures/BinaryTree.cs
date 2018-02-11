using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures
{
    public class BinaryTree
    {

        private BinaryTree left;
        private BinaryTree right;

        public object Data { get; set;  }
        public BinaryTree Left
        {
            get { return left; }
            set { left = value; left.Parent = this; }
        }
        public BinaryTree Right
        {
            get { return right; }
            set { right = value; right.Parent = this; }
        }

        public BinaryTree Parent { get; private set; }

        public BinaryTree RightestChild()
        {
            BinaryTree tree = Right;
            while(tree.Right != null)
            {
                tree = tree.right;
            }
            return tree;
        }

        public BinaryTree LeftestChild()
        {
            BinaryTree tree = Left;
            while (tree.Left != null)
            {
                tree = tree.Left;
            }
            return tree;
        }
    }
}
