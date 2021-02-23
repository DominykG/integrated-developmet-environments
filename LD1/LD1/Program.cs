using System;
using System.Collections.Generic;

namespace LD1
{
    class Program
    {
        static void Main(string[] args)
        {
            do{} while (menu(handleUserInput("Enter a choice ([1-14] to execute a task; 0 to clear console; -1 to exit): ")));
        }

        private static bool menu(int choice)
        {
            switch (choice)
            {
                case 0:
                    Console.Clear();
                    break;

                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 8:
                    break;

                case 9:
                    break;

                case 10:
                    break;

                case 11:
                    break;

                case 12:
                    break;

                case 13:
                    break;

                case 14:
                    break;

                case -1:
                    return false;
            }
            return true;
        }

        private static int handleUserInput(string message)
        {
            int choice;

            while (true)
            {
                Console.Write(message);
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
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
