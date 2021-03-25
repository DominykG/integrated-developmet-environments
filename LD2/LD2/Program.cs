using System;

namespace LD2
{
    class Program
    {
        static void Main(string[] args)
        {
            do { } while (menu());
        }

        private static bool menu()
        {

            var teaList = Tea.FromCsvFile(@Environment.CurrentDirectory + "\\TeaList.csv");

            foreach (Tea tea in teaList)
                Console.WriteLine(tea);

            Console.WriteLine("\n 0 - Clear console.\n" +
                                " 1 - Calculate average amount of days until expiry.\n" +
                                " 2 - Filter tea by production date.\n" +
                                " 3 - Filter tea by manufacturer, days until expiry and price.\n" +
                                "-1 - Exit.\n");

            switch (handleUserInput("\nPlease enter your choice: "))
            {
                case 0:
                    Console.Clear();
                    break;

                case 1:
                    Console.WriteLine("\nAverage days until expiry: " + Tea.AverageDaysUntilExpiry(teaList));
                    break;

                case 2:
                    try 
                    {
                        Console.Write("Enter date: ");
                        var productionDate = DateTime.Parse(Console.ReadLine());
                        Tea.ToCsvFile(Tea.Filter(teaList, productionDate), "FilterByDate");
                    }
                    catch (Exception e) 
                    { 
                        Console.WriteLine($"{e.Message} Please try again."); 
                    }
                    break;

                case 3:
                    try
                    {
                        Console.Write("Enter manufacturer: ");
                        var manufacturer = Console.ReadLine();
                        Console.Write("Enter daysUntilExpiry: ");
                        var daysUntilExpiry = int.Parse(Console.ReadLine());
                        Console.Write("Enter price: ");
                        var price = float.Parse(Console.ReadLine());
                        Tea.ToCsvFile(Tea.Filter(teaList, manufacturer, daysUntilExpiry, price), "OtherFilter");
                    }
                    catch (Exception e) 
                    {
                        Console.WriteLine($"{e.Message} Please try again."); 
                    }
                    break;

                case -1:
                    return false;
            }
            Console.WriteLine();
            return true;
        }

        private static int handleUserInput(string message, int lowerBound = int.MinValue, int upperBound = int.MaxValue)
        {
            int choice;

            while (true)
            {
                Console.Write(message);
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < lowerBound || choice > upperBound)
                    {
                        Console.WriteLine("The number is out of bounds. Please try again");
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Please try again.");
                    continue;
                }
                break;
            }
            return choice;
        }
    }
}
