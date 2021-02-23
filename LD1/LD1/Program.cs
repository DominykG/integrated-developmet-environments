using System;

namespace LD1
{
    class Program
    {
        static void Main(string[] args)
        {
            do{} while (menu(handleUserInput("Enter a choice ([1-14] to execute a task; 0 to clear console; -1 to exit): ")));
        }

        private static void task1()
        {
            //Initial code
            //
            //int n;
            //double f;
            //Console.WriteLine( ”Enter a positive number N:” );
            //N = Convert.ToInt32(Console.ReadLine());
            //for (i = 1; i <= N; i++) f * = i;
            //Console.WriteLine("{ 0}! = { 1} ", n, f);

            //Fixed code
            Console.WriteLine("Task1 Calculate the factorial");
            //Initial value of variable `f` was missing
            double f = 1;
            //Variable `n` was wrongly redefined as `N`
            int n = handleUserInput("Enter a positive number N:");
            //Variable `i` was not defined
            for (int i = 1; i <= n; i++) f *= i;
            Console.WriteLine($"{n} ! = {f} ");
        }

        private static void task2()
        {
            Console.WriteLine("Task2 Calculate the average");

            int amount = handleUserInput("Enter the amount of numbers:"); //User decides the amount of numbers
            double total = 0.0f;
            for (int i = 0; i < amount; i++) //User inputs a number the `amount` number of times
            {
                total += handleUserInput("Enter the number:"); 
            }
            Console.WriteLine($"The average is: {total / amount}."); //The average of all input numbers is calculated and displayed
        }

        private static void task3()
        {
            Console.WriteLine("Task3 Squares of first five positive odd numbers");

            for (int i = 1; i <= 9; i += 2) //Loop will start at 1 (1st positive odd number) ant stop at 9 (5th positive odd number)
            {
                Console.WriteLine($"The number is: {i}, squared: {i * i}."); //Display the number as is and squared
            }
        }

        private static bool menu(int choice)
        {
            switch (choice)
            {
                case 0:
                    Console.Clear();
                    break;

                case 1:
                    task1();
                    break;

                case 2:
                    task2();
                    break;

                case 3:
                    task3();
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

        //This function ensures that the string user inputs is an integer
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
