using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace LD3
{
    class Helper
    {
        private const string STUDENT_CSV_HEADER = "Name,Surname,H1,H2,H3,H4,H5,Exam";

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
                                " 5 - Measure student generation.\n" +
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
                    TestStudentGeneration();
                    break;

                case -1:
                    return false;

                default:
                    Console.WriteLine("Wrong.");
                    break;
            }
            return true;
        }

        public static int IntegerIsInRange(int number, int min = 1, int max = 10)
        {
            return Enumerable.Range(min, max).Contains(number) ?
              number :
              throw new ArgumentOutOfRangeException(nameof(number), $"Integer is out of allowed range ({min}-{max}).");
        }

        public static void TestStudentGeneration()
        {
            Stopwatch watch = new Stopwatch();

            //measure 10_000 students
            MeasureGeneration(watch, 10_000);

            //measure 100_000 students
            MeasureGeneration(watch, 100_000);

            //measure 1_000_000 students
            MeasureGeneration(watch, 1_000_000);

            //measure 10_000_000 students
            MeasureGeneration(watch, 10_000_000);
        }

        private static void MeasureGeneration(Stopwatch watch, int amount)
        {
            watch.Restart();

            var students = new List<Student>();

            using StreamWriter passedStudents = new StreamWriter($"passed_students_of_{amount}.csv");
            using StreamWriter failedStudents = new StreamWriter($"failed_students_of_{amount}.csv");

            passedStudents.WriteLine(STUDENT_CSV_HEADER);
            failedStudents.WriteLine(STUDENT_CSV_HEADER);

            //generate random students
            for (int i = 0; i < amount; i++)
                students.Add(new Student());

            //sort students in 2 files
            students.ForEach(student =>
            {
                if ((int)student.FinalPointsAverage() >= 5) 
                    passedStudents.WriteLine(student.ToCsvString());

                else failedStudents.WriteLine(student.ToCsvString());
            });

            passedStudents.Close();
            failedStudents.Close();

            watch.Stop();

            Console.WriteLine($"Time elapsed creating {amount:n0} random student records: {watch.Elapsed}");
        }
    }
}
