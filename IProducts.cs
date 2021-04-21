using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    interface IProducts
    {
        int Pcode { get; set; }
        string PName { get; set; }
        int PPrice { get; set; }
        string PType { get; set; }
        int PAmount { get; set; }


    }
}
