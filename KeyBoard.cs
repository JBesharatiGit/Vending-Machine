using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class KeyBoard
    {
        VendingMachine VM;
        ConsoleKeyInfo k;
        public int volume = 0;

        public KeyBoard(VendingMachine vm)//Get instance of Vending Machine that sended from program.cs
        {
            this.VM = vm;
        }
        public void Payment()//Gets usder Requestes for charging money
        {
            k = new ConsoleKeyInfo();
            VM.Payment(ref volume, k);
            do
            {
                GetKeyEvent(ref volume, ref k);
                VM.Payment(ref volume, k);
            } while (k.Key != ConsoleKey.F10);

            VM.ResetCursorPosition();
            volume = 0;
        }
        public void StopShopping()// cancells shopping and return Money
        {
            VM.StopShopping();
        }
        public void GetRequest()//Gets goods requested by user
        {
            if (VM.CurrentCustomerID > -1)
            {
                k = new ConsoleKeyInfo();
                VM.GetRequest(ref volume, k);
                try
                {
                    do
                    {
                        GetKeyEvent(ref volume, ref k);
                        VM.GetRequest(ref volume, k);
                    } while (k.Key != ConsoleKey.F10);

                    VM.AddRequestToLstCustomer();
                }
                catch (Exception e)
                {
                    VM.ResetCursorPosition();
                    volume = 0;
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }

                VM.ResetCursorPosition();
                volume = 0;
            }
            else
                throw new ArgumentException("Please charge money.", "");
        }
        public void RemoveFromRequest()//Delets an item from requested goods
        {
            VM.RemoveFromRequest();
        }
        public void Purchase()//Commits purchase and ends one customer shopping
        {
            volume = 0;
            VM.Purchase();
            Console.WriteLine("Press Enter to back.>");
        }
        public void ShowReportRequest()//Shows list of requested goods
        {
            VM.ShowReportRequest();
            VM.ResetCursorPosition();
            Console.WriteLine("Press Enter to back.>");
        }
        public void ShowReportPayment()//Shows list of requested Money
        {
            VM.ShowReportPayment();
            VM.ResetCursorPosition();
            Console.WriteLine("Press Enter to back.>");
        }
        public void ShowReportTosell()//Shows report of  subtotal of shopping group by customers
        {
            VM.ShowReportTosell();
            Console.WriteLine("Press Enter to back.>");
        }
        public void Execute()//turns on Vending Machine
        {
            VM.On();
        }
        public void Undo()//turns off Vending Machine
        {
            VM.Off();
        }
        public void GetKeyEvent(ref int volume, ref ConsoleKeyInfo k)//Gets consolekeyinfo
        {
            k = Console.ReadKey();
            switch (k.Key)
            {
                case ConsoleKey.UpArrow:
                    volume++;
                    break;
                case ConsoleKey.DownArrow:
                    if (volume != 0)
                        volume--;
                    Console.WriteLine();
                    break;
                case ConsoleKey.Enter:
                    //volume = -1;
                    break;
                default:
                    break;
            }
        }
     }
}
