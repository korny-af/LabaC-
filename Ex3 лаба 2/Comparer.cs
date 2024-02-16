using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    internal class Comparer
    {
        internal int Compare<T>(T t1, T t2)
        {
            if (t1 == null && t2 == null) return 0;
            string x = t1 as string;
            string y = t2 as string;

            if (x!.Length > y!.Length) return 1;
            if (x!.Length < y!.Length) return -1;
            return 0;
        }
    }
}
