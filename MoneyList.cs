using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class MoneyList
    {
        public List<Money> LstMoneys = new List<Money>();
        public void AddMoney(Money m)
        {
            LstMoneys.Add(m);
        }
        public void ShowMoneyList(int volume, int writeAear)
        {
            Console.SetCursorPosition(0, writeAear);
            Console.WriteLine("{0,3}  {1,-15}", LstMoneys[volume].MoneyAmount, LstMoneys[volume].MoneyName);
        }
        public Money GetRequestedMoney(int volume)
        {
            return LstMoneys[volume];
        }
        public MoneyList()//This is constructor and inputs several goods to list for test
        {
            Money m1;
            m1 = new Money(1, "Kr");
            AddMoney(m1);
            m1 = new Money(5, "Kr");
            AddMoney(m1);
            m1 = new Money(10, "Kr");
            AddMoney(m1);
            m1 = new Money(50, "Kr");
            AddMoney(m1);
            m1 = new Money(100, "Kr");
            AddMoney(m1);
            m1 = new Money(200, "Kr");
            AddMoney(m1);
            m1 = new Money(500, "Kr");
            AddMoney(m1);
            m1 = new Money(1000, "Kr");
            AddMoney(m1);
        }
    }
}
