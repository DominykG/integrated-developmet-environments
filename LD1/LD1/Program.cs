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

        private static void task4()
        {
            Console.WriteLine("Task4 Calculate area of a shape (rectangular, triangular or trapezoidal) ");

            float area;

            switch (handleUserInput("Area of what shape You want to calculate? (1 rectangular, 2 triangular, 3 trapezoidal)"))
            {
                case 1:
                    area = handleUserInput("Input length `l`:") * handleUserInput("Input width `w`:"); //calculate the area of a rectangular
                    Console.WriteLine($"Area of an rectangular is: {area}");
                    break;
                case 2:
                    area = handleUserInput("Input base `b`:") * handleUserInput("Input hight `h`:") / 2; //calculate the area of a triangular
                    Console.WriteLine($"Area of an triangular is: {area}");
                    break;
                case 3:
                    area = (handleUserInput("Input base `a`:") + handleUserInput("Input base `b`:")) / 2 * handleUserInput("Input height `h`:"); //calculate the area of a trapezoidal
                    Console.WriteLine($"Area of an trapezoidal is: {area}");
                    break;
            }
        }

        private static void task5()
        {
            Console.WriteLine("Task5 Check input character");

            char input = Console.ReadLine()[0];
            if (char.IsNumber(input)) //ASCII number
            {
                Console.WriteLine("Input character is a number");
            }
            else if (char.IsLower(input)) //ASCII lower case characters
            {
                Console.WriteLine("Input character is a lower case character");
            }
            else if (char.IsUpper(input)) //ASCII upper case characters
            {
                Console.WriteLine("Input character is a upper case character");
            }
            else // not a number or a character
            {
                Console.WriteLine("Input character is not a number or a character");
            }
        }

        private static void task6()
        {
            //Initial code
            //
            //i = 1;
            //while (i < 10)
            //{
            //    j = i * i - 1;
            //    k = 2 * j - 1;
            //    Console.WriteLine(" i = { 0}; j = { 1}; k = { 2}", i, j, k);
            //}

            //Fixed code
            Console.WriteLine("Task6 Fix the loop");
            int i = 1, j, k; //variable `i` was not defined

            while (i < 10)
            {
                j = i * i - 1; //variable `j` was not defined
                k = 2 * j - 1; //variable `k` was not defined
                Console.WriteLine($" i = {i}; j = {j}; k = {k}");
                i += 1; //Stop condition was missing
            }
        }

        private static void task7()
        {
            Console.WriteLine("Task7 Function `y = -2.4 * x^2 + 5 * x - 3` output");
            //In range of x from -2 to 2 by step 0.5
            for (float x = -2.0f; x <= 2.0f; x += 0.5f)
            {
                Console.WriteLine($"x = {x}, y = {-2.4 * x * x + 5 * x - 3}");
            }
        }

        private static void task8()
        {
            Console.WriteLine("Task8 print ASCII codes of letters `a` to `z`");

            for (int character = 97; character <= 122; character++) //ASCII lower case characters span from 97 to 122 inclusively
            {
                Console.WriteLine($"{character} = {(char)character}");
            }
        }

        private static bool task9()
        {
            Console.WriteLine("Task9 Check if input integer is prime or not");

            int input = handleUserInput("Input a number:");
            //Two `2` is a prime number
            if (input == 2)
            {
                Console.WriteLine("Is a prime number");
                return true;
            }
            //One `1` and any number that devides by `2` without a remainder is not a prime number
            if (input <= 1 || input % 2 == 0)
            {
                Console.WriteLine("Is not a prime number");
                return false;
            }

            int boundary = (int)Math.Floor(Math.Sqrt(input)); //Checking only divisors up to square root of input (Trial division method)
            
            //Starting at 3 because every number before was already checked
            //Uncrement by two because all whole numbers will be divided by 2 without remainder and that was already checked
            for (int divisor = 3; divisor <= boundary; divisor += 2) 
            {
                if (input % divisor == 0)
                {
                    Console.WriteLine("Is not a prime number");
                    return false;
                }
            }
            //If for loop above didnt determine that the input number is not prime then the number is prime
            Console.WriteLine("Is a prime number");

            return true;
        }

        private static bool task10()
        {
            Console.WriteLine("Task10 Number geusser");
            Random random = new Random();
            int secretNumber = random.Next(1, 11), guess;
            for (int i = 0; i < 5; i++)
            {
                guess = handleUserInput("Enter Your guess (between 1 - 10):", 1, 10);
                if (guess == secretNumber)
                {
                    Console.WriteLine("You have guessed!");
                    return true;
                }
            }

            Console.WriteLine("Yoi have lost!");
            return false;
        }

        private static void task11()
        {
            Console.WriteLine("Task11 ");

        }

        private static void task12()
        {
            Console.WriteLine("Task12 Greatest common divisor");

            int num1 = handleUserInput("Input first number:");
            int num2 = handleUserInput("Input second number:");

            for (int i = Math.Min(num1, num2); i >= 1; i--) //Common divisor can not be larger than the smallest number 
            {
                if (num1 % i == 0 && num2 % i == 0)
                {
                    Console.WriteLine($"Largert common divisor is {i}"); //Largest common divisor found
                    break;
                }
            }

        }

        private static void task13()
        {
            Console.WriteLine("Task13 Count digits in a string");

            var digits = new Dictionary<char, int>();

            string input = Console.ReadLine();

            foreach (char character in input)
            {
                if (char.IsDigit(character)) //If a character in a string is a digit
                {
                    if (!digits.ContainsKey(character)) //If digit dictionary does not contain such digit
                        digits.Add(character, 1); //Create new dictionary entry
                    else
                        digits[character] += 1; //Update existing dictionary entry
                }
            }

            foreach (var kvp in digits)
            {
                // Display all digits ant amount of times they are repeated in the nput steing
                Console.WriteLine($"digit: {kvp.Key}, amount: {kvp.Value}"); 
            }
        }

        private static bool task14()
        {
            Console.WriteLine("Task14 Vilnius founding date guesser");

            int answer = 1323;

            for (int i = 0; i < 3; i++)
            {
                if (handleUserInput("Input Your guess: ") == answer)
                {
                    Console.WriteLine("You have guessed!");
                    return true;
                }
            }

            Console.WriteLine("You miss, Vilnius founding date is – 1323");
            return false;
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
                    task4();
                    break;

                case 5:
                    task5();
                    break;

                case 6:
                    task6();
                    break;

                case 7:
                    task7();
                    break;

                case 8:
                    task8();
                    break;

                case 9:
                    task9();
                    break;

                case 10:
                    task10();
                    break;

                case 11:
                    task11();
                    break;

                case 12:
                    task12();
                    break;

                case 13:
                    task13();
                    break;

                case 14:
                    task14();
                    break;

                case -1:
                    return false;
            }
            return true;
        }

        //This function ensures that the string user inputs is an integer with possibility to set upper and lower bound
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
