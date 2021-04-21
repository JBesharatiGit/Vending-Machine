using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class Money
    {
        public int MoneyAmount { get; set; }
        public string MoneyName { get; set; }
        public Money(int amount, string name)
        {
            this.MoneyAmount = amount;
            this.MoneyName = name;
        }
    }
}
