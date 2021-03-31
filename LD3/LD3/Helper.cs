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

        public static bool menu(List<Student> students)
        {

            Console.WriteLine("\n 0 - Clear console.\n" +
                                " 1 - Add new student.\n" +
                                " 2 - Show students as table.\n" +
                                " 3 - Read students from file.\n" +
                                " 4 - Add random student.\n" +
                                " 5 - Show student marks.\n" +
                                "-1 - Exit.\n");

            switch (HandleIntegerInput("Input Your choice: "))
            {
                case 0:
                    Console.Clear();
                    break;

                case 1:
                    students.Add(Student.CreateFromConsole());
                    break;

                case 2:
                    Console.WriteLine(Student.ToTable(students, true));
                    break;

                case 3:
                    Console.Write("Enter file name: ");
                    string filename = Console.ReadLine();
                    try
                    {
                        students.AddRange(Student.ReadFromFile(@Environment.CurrentDirectory + $"/{filename}"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\n{e.Message} Please fix errors and try again.");
                    }

                    break;

                case 4:
                    var s = new Student();
                    students.Add(s);
                    Console.WriteLine(s);
                    break;

                case 5:
                    Console.WriteLine(students);
                    break;

                case -1:
                    return false;

                default:
                    Console.WriteLine("Wrong.");
                    break;
            }
            return true;
        }
    }
}
