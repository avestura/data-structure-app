using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Generic
{
    public class Treap<T> where T : IComparable
    {

        public T Data { get; set; }

        public int Priority { get; set; }

        public Treap<T> Right { get; set; }
        public Treap<T> Left { get; set; }

        public Treap(T data, int priority)
        {
            Data = data;
            Priority = priority;
        }

        public Treap<T> Insert(T data, int priority)
        {
            return Insert(this, data, priority);
        }



        public static Treap<T> RotateRight(Treap<T> y)
        {
            var x = y.Left; var xSubTree = x.Right;

            x.Right = y; y.Left = xSubTree;

            return x;
        }

        public static Treap<T> LeftRotate(Treap<T> x)
        {
            var y = x.Right; var ySubTree = y.Left;

            y.Left = x;
            x.Right = ySubTree;

            return y;
        }

        #region Static Methods 
        public static Treap<T> Insert(Treap<T> tree, T data, int priority)
        {
            if (tree == null) return new Treap<T>(data, priority);

            if (data.CompareTo(tree.Data) <= 0)
            {
                tree.Left = Insert(tree.Left, data, priority);

                if (tree.Left.Priority > tree.Priority) tree = RotateRight(tree);

            }
            else if (data.CompareTo(tree.Data) > 0)
            {
                tree.Right = Insert(tree.Right, data, priority);

                if (tree.Right.Priority > tree.Priority) tree = LeftRotate(tree);
            }

            return tree;
        }
        #endregion



    }
}
