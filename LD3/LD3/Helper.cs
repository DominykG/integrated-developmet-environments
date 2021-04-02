using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace LD3
{
    class Helper
    {
        public static bool Menu(List<Student> students)
        {
            Console.WriteLine("\n 0 - Clear console.\n" +
                                " 1 - Add new student.\n" +
                                " 2 - Show students as table.\n" +
                                " 3 - Read students from file.\n" +
                                " 4 - Add random student.\n" +
                                " 5 - Measure student generation.\n" +
                                " 6 - Measure sorting and writing to file using different containers.\n" +
                                " 7 - Measure sorting using different containers and strategies.\n" +
                                " 8 - Populate students.\n" +
                                " 9 - Generate student files.\n" +
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
                    Console.WriteLine(Student.ToTable(students.OrderBy(student => student.Name).ToList(), true));
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
                    var student = new Student();
                    Console.WriteLine(student);
                    students.Add(student);
                    break;

                case 5:
                    TestStudent.TestGeneration();
                    break;

                case 6:
                    try
                    {
                        TestStudent.TestContainers();
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Populate students before testing sorting.");
                    }
                    break;

                case 7:
                    try
                    {
                        TestStudent.TestSorting();
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Populate students before testing sorting.");
                    }
                    break;

                case 8:
                    TestStudent.PopulateStudents();
                    break;

                case 9:
                    TestStudent.GenerateStudentFiles();
                    break;

                case -1:
                    return false;

                default:
                    Console.WriteLine("Wrong.");
                    break;
            }
            return true;
        }

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

        public static int IntegerIsInRange(int number, int min = 1, int max = 10)
        {
            return Enumerable.Range(min, max).Contains(number) ?
              number :
              throw new ArgumentOutOfRangeException(nameof(number), $"Integer is out of allowed range ({min}-{max}).");
        }
    }
}
