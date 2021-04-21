using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class Sort : IComparer<IProducts>
    {
        public int Compare(IProducts x, IProducts y)
        {
            return x.Pcode.CompareTo(y.Pcode);
        }
    }
}
