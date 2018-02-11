using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuilanDataStructures.DataStructures
{

    public class PriorityQueue
    {

        List<Huffman.BinaryTree> Trees { get; set; } = new List<Huffman.BinaryTree>();

        public int Size { get { return Trees.Count; } }


        private int MinHeapify(int position)
        {
            if (position > Trees.Count - 1) return -1;

            while (position > 0)
            {
                if (Trees[ParentLocationOf(position)].Repeat > Trees[position].Repeat)
                {
                    SwapByIndex(ParentLocationOf(position), position);
                    position = ParentLocationOf(position);
                }
                else break;
            }
            return position;

        }

        private void SwapByIndex(int index1, int index2)
        {
            var temp = Trees[index1];
            Trees[index1] = Trees[index2];
            Trees[index2] = temp;
        }

        public int ParentLocationOf(int position)
        {
            return (position - 1) / 2;
        }
        public bool HasLeft(int position)
        {
            return ((2 * position + 1) < Size);
        }
        public bool HasRight(int position)
        {
            return ((2 * position + 2) < Size);
        }
        public int LeftPositionOf(int position)
        {
            if (!HasLeft(position)) return -1;
            return (2 * position + 1);
        }
        public int RightPositionOf(int position)
        {
            if (!HasRight(position)) return -1;
            return (2 * position + 2);
        }

        public void EnqueueNew(int priority, char? data)
        {

            Trees.Add(new Huffman.BinaryTree(priority, data));

            MinHeapify(Trees.Count - 1);

        }

        public void Enqueue(Huffman.BinaryTree tree)
        {
            Trees.Add(tree);
            MinHeapify(Trees.Count - 1);
        }

        private void MoveDown(int index)
        {
            int lowerIndex = index;

            if (HasLeft(index) && Trees[LeftPositionOf(index)].Repeat < Trees[lowerIndex].Repeat) lowerIndex = LeftPositionOf(index);
            if (HasRight(index) && Trees[RightPositionOf(index)].Repeat < Trees[lowerIndex].Repeat)lowerIndex = RightPositionOf(index);

            if (lowerIndex != index)
            {
                SwapByIndex(lowerIndex, index);
                MoveDown(lowerIndex);
            }

        }

        public Huffman.BinaryTree DequeueData()
        {
            Huffman.BinaryTree tree = Trees[0];
            Trees.RemoveAt(0);
            MoveLastItemToFirst();
            MoveDown(0);
            return tree;
        }



        private void MoveLastItemToFirst()
        {
            if (Trees.Count == 0) return;
            var tree = Trees[Trees.Count - 1];
            Trees.RemoveAt(Trees.Count - 1);
            Trees.Insert(0, tree);
        }


        public void Clear()
        {
            Trees.Clear();
        }

        


    }
}
