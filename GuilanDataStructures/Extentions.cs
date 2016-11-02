using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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

    }
}
