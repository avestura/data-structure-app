using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Generic
{
    public class BinaryTree<T>
    {

        private BinaryTree<T> left;
        private BinaryTree<T> right;

        public T Data { get; set; }
        public int TreeNumber { get; set; }

        public BinaryTree<T> Left
        {
            get { return left; }
            set { left = value; /*left.Parent = this;*/ }
        }
        public BinaryTree<T> Right
        {
            get { return right; }
            set { right = value; /*right.Parent = this;*/ }
        }

        //public BinaryTree<T> Parent { get; private set; }

        public BinaryTree<T> RightestChild()
        {
            BinaryTree<T> tree = Right;
            while (tree.Right != null)
            {
                tree = tree.right;
            }
            return tree;
        }

        public BinaryTree<T> LeftestChild()
        {
            BinaryTree<T> tree = Left;
            while (tree.Left != null)
            {
                tree = tree.Left;
            }
            return tree;
        }


    }
}
