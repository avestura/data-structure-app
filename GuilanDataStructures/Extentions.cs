using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GuilanDataStructures.DataStructures.Generic;

namespace GuilanDataStructures
{
    public static class Extentions
    {

        public static bool IsEmpty(this Stack stack)
        {
            if (stack.Count == 0) return true; return false;
        }

        public static bool IsEmpty(this Stack<int> stack)
        {
            if (stack.Count == 0) return true; return false;
        }

        public static string ConvertTreeToString(this Treap<char> treap)
        {
            if (treap == null) return string.Empty;
            return $"({ConvertTreeToString(treap.Left)}{treap.Data}/{treap.Priority}{ConvertTreeToString(treap.Right)})";
        }

    }
}
