using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class Foods : IProducts
    {
        public int Pcode { get; set; }
        public string PName { get; set; }
        public int PPrice { get; set; }
        public string PType { get; set; }
        public int PAmount { get; set; }
        public Foods(int pcode, string pname, int pprice, /*string ptype,*/ int pamount)
        {
            this.PAmount = pamount;
            this.Pcode = pcode;
            this.PName = pname;
            this.PPrice = pprice;
            this.PType = "Foods";
        }
    }
}
