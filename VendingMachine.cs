using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class VendingMachine
    {


        #region fields and Properties

        public ProductsList productslist = new ProductsList();
        public MoneyList moneylist = new MoneyList();
        public Customer customer;  

        public List<Customer> LstCustomer = new List<Customer>();

        public int volume = 0;
        public const int writeAear = 15;
        private int _LastCursorPosition = writeAear + 2;
        public int _currentCustomerID = -1;

        public int LastCursorPosition { get { return _LastCursorPosition; } set { _LastCursorPosition = value; } }
        public int CurrentCustomerID { get { return _currentCustomerID; } set { _currentCustomerID = value; } }


        #endregion
        public void Payment(ref int volume, ConsoleKeyInfo k)//Gets usder Requestes for charging money
        {
            if (CurrentCustomerID == -1)
                CreateCurrentCustomer();

            if (volume < moneylist.LstMoneys.Count)
            {
                if (k.Key != ConsoleKey.Enter)
                {
                    moneylist.ShowMoneyList(volume, writeAear);

                    Console.SetCursorPosition(10, (writeAear + 1));
                    Console.Write("{0,4}{1,-15} ", "Code ", "Name");

                }
                else
                {
                    customer.LstMoneysRequest.Add(moneylist.GetRequestedMoney(volume));
                    customer.MoneyPool += moneylist.GetRequestedMoney(volume).MoneyAmount;
                    Console.SetCursorPosition(10, LastCursorPosition);
                    Console.WriteLine("{0,4}  {1,-15} {2}", customer.LstMoneysRequest.Last().MoneyAmount, customer.LstMoneysRequest.Last().MoneyName, "                       ");
                    LastCursorPosition = Console.CursorTop;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(10, (LastCursorPosition));
                Console.Write("Select next(Arrow-Up); Add(Enter); Back(F10)");
                Console.ResetColor();

            }
            else
            {
                volume = 0;

                moneylist.ShowMoneyList(volume, writeAear);
                LastCursorPosition = volume;
            }

        }
        public void AddPaymentToLstCustomer()
        {
            LstCustomer[CurrentCustomerID].LstMoneysRequest = customer.LstMoneysRequest;
        }
        public void CreateCurrentCustomer()//Make a customer  for every shopp and payment
        {
            customer = new Customer();
            customer.MoneyPool = 0;
            customer.TotalRequestsPrice = 0;

            LstCustomer.Add(customer);
            CurrentCustomerID = LstCustomer.Count() - 1;
        }
        public void GetRequest(ref int volume, ConsoleKeyInfo k)//Gets goods requested by user
        {

            if (volume < productslist.LstProducts.Count)
            {
                if (k.Key != ConsoleKey.Enter)
                {
                    productslist.ShowProductsList(volume, writeAear);
                    //lstLastIndex = volume;

                    Console.SetCursorPosition(10, (writeAear + 1));
                    Console.Write("{0,3}{1,-15} {2,5} {3,5} {4,-30}", "Code ", "Name", "Price", "Money", "Message");


                }
                else
                    AddToBasket(volume);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(10, (LastCursorPosition));
                Console.Write("Select next(Arrow-Up); Add to basket(Enter); Back(F10)");
                Console.ResetColor();


            }
            else
            {
                volume = 0;

                productslist.ShowProductsList(volume, writeAear);
                //lstLastIndex = volume;
            }
        }
        public void AddToBasket(int volume)//Adds requested good to shopping basket and checks if it is frequent.
        {
            if (customer.MoneyPool > customer.TotalRequestsPrice + productslist.GetRequest(volume).PPrice)
            {
                string howToUse = "";

                IProducts iproducts = productslist.GetRequest(volume);
                if (ChekFrequent(iproducts) == true)
                {
                    customer.LstProductsRequest.Add(iproducts);
                    customer.TotalRequestsPrice += productslist.GetRequest(volume).PPrice;
                    howToUse = productslist.ShowHowToUse(iproducts);
                    Console.SetCursorPosition(10, (LastCursorPosition));
                    Console.WriteLine("{0,3}  {1,-15} {2,5} {3,5} {4,5} {5}", customer.LstProductsRequest.Last().Pcode,
                                  customer.LstProductsRequest.Last().PName,
                                  customer.LstProductsRequest.Last().PPrice,
                                  customer.MoneyPool - customer.TotalRequestsPrice, howToUse, "             ");
                    LastCursorPosition = Console.CursorTop;
                }
            }
            else
                throw new ArgumentException("Total request ofgoods must be less than charged money.           ", "");
        }
        public bool ChekFrequent(IProducts iproducts)//Checks if the current good is in shopping basket
        {
            char result = ' ';
            bool yesNo = true;
            if (customer.LstProductsRequest.Contains(iproducts) == true)
            {
                do
                {
                    Console.SetCursorPosition(10, (LastCursorPosition));
                    Console.Write("                                                          ");
                    Console.SetCursorPosition(10, (LastCursorPosition));
                    Console.Write("Alredy Selected good.Will you Add again?(y/n)");
                    yesNo = char.TryParse(Console.ReadLine(), out result);
                } while (char.ToLower(result) != 'y' && char.ToLower(result) != 'n');

                if (char.ToLower(result) == 'y')
                {
                    yesNo = true;
                }
                else
                    yesNo = false;
                Console.SetCursorPosition(10, (LastCursorPosition));
                Console.Write("                                                    ");
            }
            return yesNo;
        }
        public void AddRequestToLstCustomer()
        {
            LstCustomer[CurrentCustomerID].LstProductsRequest = customer.LstProductsRequest;
        }
        public void RemoveFromRequest()//Delets an item from requested goods
        {
            if (CurrentCustomerID != -1)
            {
                ShowReportRequest();
                if (customer.LstProductsRequest.Count > 0)
                {
                    Console.Write("Enter Row number; For Cancell(0 Enter) : ");
                    int indx = int.Parse(Console.ReadLine());
                    if (customer.LstProductsRequest.Count >= indx && indx > 0)
                    {
                        customer.TotalRequestsPrice -= customer.LstProductsRequest[indx-1].PPrice;
                        customer.RemoveFromRequest(indx - 1);
                        ResetCursorPosition();
                    }
                    else
                    {
                        ResetCursorPosition();
                        throw new ArgumentOutOfRangeException("Item did not found or Cancelled.");
                    }
                }
            }
            else
                throw new ArgumentNullException("There is not any Shopp.");
        }
        public void StopShopping()// cancells shopping and return Money if is was befor commit purchase
        {
            if (CurrentCustomerID != -1)
            {
                customer.LstProductsRequest = new List<IProducts>();
                Console.WriteLine("Shopping stoped:");
                Purchase();
            }
            else
                throw new ArgumentNullException("There is not any Charged Money.");
        }
        public void Purchase()//Commits purchase and ends one customer shopping
        {
            if (CurrentCustomerID != -1)
            {
                AddPaymentToLstCustomer();
                AddRequestToLstCustomer();

                CsutomerBalance();

                LastCursorPosition = 15;
                volume = 0;
                CurrentCustomerID = -1;
            }
            else
                throw new ArgumentNullException("There is not any Shopp.");
        }
        public void CsutomerBalance()//Gets reports of sharged money and requested goods and calculats rest of money
        {
            int totalPrice = 0;
            int totalAmount = 0;
            int MoneyPool = 0;
            foreach (var item in LstCustomer[CurrentCustomerID].LstProductsRequest)
            {
                totalPrice += item.PPrice;
                totalAmount++;
            }
            foreach (var item in LstCustomer[CurrentCustomerID].LstMoneysRequest)
            {
                MoneyPool += item.MoneyAmount;
            }
            LstCustomer[CurrentCustomerID].MoneyPool = MoneyPool;
            LstCustomer[CurrentCustomerID].TotalRequestsPrice = totalPrice;
            LstCustomer[CurrentCustomerID].MoneyReturn = MoneyPool - totalPrice;

            ShowReportRequest();
            Console.WriteLine(" ");
            ShowReportPayment();
            Console.WriteLine("   ");
            Console.WriteLine($"Charged Money  : {MoneyPool}");
            Console.WriteLine($"Total Price    :{totalPrice}");
            Console.WriteLine($"Money to Return: {MoneyPool - totalPrice}");

            customer = new Customer();
            CurrentCustomerID = -1;
        }

        #region Reports
        public void ShowReportRequest()//Shows list of requested goods
        {
            int totalPrice = 0;

            Console.SetCursorPosition(0, (LastCursorPosition));
            Console.WriteLine("Goods :             ");
            Console.WriteLine("------------------------------");
            int row = 0;
            foreach (var item in LstCustomer[CurrentCustomerID].LstProductsRequest /*customer.LstProductsRequest*/)
            {
                row++;
                Console.WriteLine("{0,-3} {1,3}  {2,-15} {3,5}", row, item.Pcode, item.PName, item.PPrice);
                totalPrice += item.PPrice;
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine("Total  :{0}", totalPrice);
            LastCursorPosition = Console.CursorTop;
        }
        public void ShowReportPayment()//Shows list of requested Money
        {
            int totalMoney = 0;

            Console.SetCursorPosition(0, (LastCursorPosition));
            Console.WriteLine("Money :             ");
            Console.WriteLine("--------------------------");
            foreach (var item in LstCustomer[CurrentCustomerID].LstMoneysRequest /*customer.LstMoneysRequest*/)
            {
                Console.WriteLine("{0,3}  {1,-15}", item.MoneyAmount, item.MoneyName);
                totalMoney += item.MoneyAmount;
            }

            Console.WriteLine("--------------------------");
            Console.WriteLine("Total  :{0} ", totalMoney);
            LastCursorPosition = Console.CursorTop;
            //Console.ReadKey();
        }
        public void ShowReportTosell()//Shows report of  subtotal of shopping group by customers
        {
            int cID = CurrentCustomerID;

            Console.WriteLine("{0,5} {1,5} {2,5} {3,5}", "CustNo", "Money", "Price", "Return");

            for (int i = 0; i < LstCustomer.Count; i++)
            {
                CurrentCustomerID = i;
                Console.WriteLine("{0,5} {1,5} {2,5} {3,5}", i, LstCustomer[i].MoneyPool, LstCustomer[i].TotalRequestsPrice, LstCustomer[i].MoneyReturn);
            }
            CurrentCustomerID = cID;
        }
        #endregion
        public void Off()//turns off Vending Machine
        {
            if (CurrentCustomerID != -1)
                throw new ArgumentException("Please  Complete the purchase or Stop shopping.", "");
            else
                Console.WriteLine("The Vending Machine is Off.");
        }
        public void On()//turns on Vending Machine
        {
            Console.WriteLine("The Vending Machine is On.");
        }
        public void ResetCursorPosition()
        {
            LastCursorPosition = writeAear + 2;
        }
   
    }
}
