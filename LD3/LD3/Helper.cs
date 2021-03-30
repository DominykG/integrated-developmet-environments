using System;
using System.Collections.Generic;
using System.Text;

namespace LD3
{
    class Helper
    {
        public static int HandleIntegerInput(string message, int lowerBound = int.MinValue, int upperBound = int.MaxValue)
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

        public static List<int> HandleHomeworksInput(string message, int lowerBound = 1, int upperBound = 10)
        {
            var homeworks = new List<int>();
            int choice;

            Console.Write(message);
            while (true)
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 0) break;
                    if (choice < lowerBound || choice > upperBound)
                    {
                        Console.WriteLine("The number is out of bounds. Please try again");
                        continue;
                    }
                    homeworks.Add(choice);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Please try again.");
                    continue;
                }                
            }

            return homeworks;
        }
    }
}
