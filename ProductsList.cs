using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class ProductsList
    {
        public List<IProducts> LstProducts = new List<IProducts>();
        public void AddProducts(IProducts p)
        {
            LstProducts.Add(p);
        }
        public IProducts GetRequest(int indx)//Sends requested goods information 
        {
            return LstProducts[indx];
        }
        public void ShowProductsList(int indx, int writeAear)//Shows products that exists in products list  
        {
            Sort SBC = new Sort();
            LstProducts.Sort(SBC);

            if (indx <= LstProducts.Count)
            {
                IProducts p = LstProducts[indx];

                Console.SetCursorPosition(0, writeAear);
                Console.WriteLine("{0,3}  {1,-15} {2,5}", p.Pcode, p.PName, p.PPrice);
                Console.WriteLine("  ");
            }
        }
        public string ShowHowToUse(IProducts p)//Sends messges based of products types
        {
            string result = "";
            switch (p.PType)
            {
                case "Drinks":
                    result = $"Drink the {p.PName} Please.";
                    break;
                case "Snacks":
                    result = $"Eat the {p.PName} Please.";
                    break;
                case "Foods":
                    result = $"Eat the {p.PName} Please.";
                    break;
                default:
                    break;
            }

            return result;
        }
        public ProductsList()//This is constructor and inputs several goods to list for test
        {
            Drinks drink; Foods foods; Snacks snacks;
            drink = new Drinks(100, "Coca", 15, 125);
            AddProducts(drink);
            drink = new Drinks(101, "Canada", 15, 120);
            AddProducts(drink);
            drink = new Drinks(102, "Beer", 25, 100);
            AddProducts(drink);
            foods = new Foods(300, "Sosage", 35, 70);
            AddProducts(foods);
            foods = new Foods(301, "OstSandwich", 38, 80);
            AddProducts(foods);

            snacks = new Snacks(200, "Cheeps", 18, 200);
            AddProducts(snacks);
            snacks = new Snacks(201, "Kex", 8, 150);
            AddProducts(snacks);
            snacks = new Snacks(202, "Goodis", 18, 200);
            AddProducts(snacks);
        }
    }
}
