using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class Customer
    {
        public Customer() { }
        public List<IProducts> LstProductsRequest = new List<IProducts>();
        public List<Money> LstMoneysRequest = new List<Money>();
        public int MoneyPool { get; set; }
        public int TotalRequestsPrice { get; set; }
        public int MoneyReturn { get; set; }
        public void RemoveFromRequest(int indx)
        {
            LstProductsRequest.RemoveAt(indx);
        }
    }
}
