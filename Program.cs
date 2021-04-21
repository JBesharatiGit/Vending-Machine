using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    class Program
    {
        static int menuItem = -1;
        static int isStop = 1;
        static bool mOn = false;

        static void Main(string[] args)
        {
            #region Setting
            Console.WindowWidth = 100;
            Console.WindowHeight = 50;
            Console.WindowTop = 0;
            Console.WindowLeft = 0;
            #endregion

            VendingMachine VM = new VendingMachine();// Make an Instance of Vending Machine Class
           
            KeyBoard keyboard = new KeyBoard(VM);//Send instance of Vending Machine to Kebord Class

            PrintMenu();

            while (isStop != 0)
            {
                try
                {
                    PrintRequesMenuItem();

                    menuItem = Convert.ToInt32(ChekVaildInput());
                    if (mOn == false && menuItem > 1) { menuItem = -1; }
                    switch (menuItem)
                    {
                        case 0:// Turn off the Vending Machine
                            SelectedMenuItem(menuItem);
                            keyboard.Undo();
                            EndingProgram();
                            break;

                        case 1://Turn on Vending Machine and Activate all menuitems
                            mOn = true;
                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            keyboard.Execute();
                            break;
                        case 2:// To charge money
                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.Payment();

                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.ShowReportPayment();
                            Console.ReadKey();

                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            break;
                        case 3://To request Goods

                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.GetRequest();

                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.ShowReportRequest();
                            Console.ReadKey();

                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            break;
                        case 4:// Remove item from request list
                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.RemoveFromRequest();
                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.ShowReportRequest();
                            Console.ReadKey();

                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            break;
                        case 5://To complete selling and show Total report
                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.Purchase();
                            Console.ReadKey();
                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            break;
                        case 6:// To stop shopping 
                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            keyboard.StopShopping();
                            break;
                        case 7://Show all Purchases(Administrative report)
                            PrintMenu();
                            SelectedMenuItem(menuItem);

                            keyboard.ShowReportTosell();

                            Console.ReadKey();
                            PrintMenu();
                            SelectedMenuItem(menuItem);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }
        #region
        public static void PrintMenu()
        {
            //Skriver ut en röd linje som beskriver hur användaren ska börja.
            Console.Clear();
            Console.WriteLine();//Instoppar  en tom linje 
            Console.ForegroundColor = ConsoleColor.Red;//Ändrar Text färg för första linje
            Console.WriteLine("*** Select a MenuItem! Write Row number then press Enter.***\n" +
                                  "Last Selected Item   :");//Skriver ut första linje innan MenySystem 

            Console.ResetColor();//Ändrar Text färg tillbaka för Menysystem

            //Skriver ut MenyAlternativ på grön färg 
            Console.ForegroundColor = ConsoleColor.Green;//Ändrar Text färg för MenyAlternativ
            Console.WriteLine("--------------------------------------------------------------------------");
            if (mOn == false)
            {
                Console.WriteLine(
                              "0. Turn Off. \n" +
                              "1. Turn On. \n"
                              );//Skriver ut MenyAlternativ
            }
            else
            {
                Console.WriteLine(
                              "0. Turn Off. \n" +
                              "1. Turn On. \n" +
                              "2. Charge Money.\n" +
                              "3. Get request.\n" +
                              "4. Remove item from request list\n" +
                              "5. Purchase. \n" +
                              "6. Stop shopping.\n" +
                              "7. Show General Selling Report.(Administrative report)\n"
                              );//Skriver ut MenyAlternativ
            }

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.ResetColor();//Ändrar tillbaka text färg 
        }
        static void PrintRequesMenuItem()
        {
            int currenCuerser = Console.CursorTop;// tar cursors nuvarande position            

            if (currenCuerser > 50)//Om cursor positions top > 50 leder cursor till linje 17 som början av program och ber att användaren välja meyAlternativ nummer
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Tryck Enter-->Representerar MenySystem");
                Console.ResetColor();
                Console.ReadKey();
                PrintMenu();
                SelectedMenuItem(menuItem);
                Console.ForegroundColor = ConsoleColor.Red;//Ändrar Text färg för första linje
                Console.WriteLine("*Inmata en menyalternativ nummer!*");
                Console.ResetColor();//Ändrar Text färg tillbaka för Menysystem

            }
            else//Ber att användaren välja meyAlternativ nummer
            {
                Console.ForegroundColor = ConsoleColor.Red;//Ändrar Text färg för första linje
                Console.WriteLine("*Mata in en menyalternativ nummer!*");
                Console.ResetColor();//Ändrar Text färg tillbaka för Menysystem
            }


        }
        static void SelectedMenuItem(int SelectedItem)
        {
            int currenCuerser = Console.CursorTop;
            Console.SetCursorPosition(23, 2);// sätter cursor position på rad 2 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[" + SelectedItem + "]");//skriver ut sista MenyAlternativ numret som har valt
            Console.ResetColor();
            Console.SetCursorPosition(0, currenCuerser);//Sätter cursor position på line 17 lika som position i programmets start
        }

        //Den här funktionen Kontrollerar att användaren skriver in tal annars loopar till skriver rätt värde 
        public static double ChekVaildInput()
        {
            double InputValue = -1;
            bool Isvlue = false;

            do//Kontrollerar om användare matar in tal 
            {
                Isvlue = Double.TryParse(Console.ReadLine(), out InputValue);
                if (Isvlue == false)
                {
                    Console.WriteLine("Ogiltig Input, Skriv in en av de MenyAlternativNummer!");
                }
            } while (Isvlue == false);
            return InputValue;
        }
        public static void EndingProgram()
        {
            isStop = 0;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("***Programmet är slut.***");
            Console.ResetColor();
            Console.ReadKey();
        }
        #endregion
    }
}
