using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Huffman
{
    public class BinaryTree 
    {
        public char? Character { get; set; }
        public int Repeat { get; set; }
        public bool HasCharacter { get { return (Character != null); } }
        public BinaryTree Left { get; set; }
        public BinaryTree Right { get; set; }

        public BinaryTree(int freq, char? ch = null) : base()
        {
            Repeat = freq;
            Character = ch;

        }

        public List<bool> Traverse(char ch, List<bool> memorizedPath)
        {
            // Set defualt value of tree parameter

            if (HasCharacter)
            {
                if (ch.Equals(Character))
                    return memorizedPath;
                return null;
            }
            else
            {

                List<bool> leftPath = null;
                List<bool> rightPath = null;


                if (Left != null)
                {
                    var leftTraverseData = new List<bool>(memorizedPath);
                    leftTraverseData.Add(true);

                    leftPath = Left.Traverse(ch, leftTraverseData);

                }

                if (Right != null)
                {
                    var rightTraverseData = new List<bool>(memorizedPath);
                    rightTraverseData.Add(false);

                    rightPath = Right.Traverse(ch, rightTraverseData);
                }

                if (leftPath != null)
                    return leftPath;
                return rightPath;


            }
        }
    }
}
