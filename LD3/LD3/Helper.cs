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

        public static bool Menu(List<Student> students)
        {
            Console.WriteLine("\n 0 - Clear console.\n" +
                                " 1 - Add new student.\n" +
                                " 2 - Show students as table.\n" +
                                " 3 - Read students from file.\n" +
                                " 4 - Add random student.\n" +
                                " 5 - Measure student generation.\n" +
                                " 6 - Measure generation with different containers.\n" +
                                " 7 - Measure sorting.\n" +
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
                    TestStudentGeneration();
                    break;

                case 6:
                    TestContainers();
                    break;

                case 7:
                    TestSorting();
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

        private static void TestSorting()
        {
            Stopwatch watch = new Stopwatch();

            TestSortingStrategyOne(watch, null, 10_000);
            TestSortingStrategyOne(watch, true, 10_000);
            TestSortingStrategyOne(watch, false, 10_000);
            Console.WriteLine();

            TestSortingStrategyOne(watch, null, 100_000);
            TestSortingStrategyOne(watch, true, 100_000);
            TestSortingStrategyOne(watch, false, 100_000);
            Console.WriteLine();

            TestSortingStrategyOne(watch, null, 1_000_000);
            TestSortingStrategyOne(watch, true, 1_000_000);
            TestSortingStrategyOne(watch, false, 1_000_000);
            Console.WriteLine();

            TestSortingStrategyOne(watch, null, 10_000_000);
            TestSortingStrategyOne(watch, true, 10_000_000);
            TestSortingStrategyOne(watch, false, 10_000_000);
            Console.WriteLine();
        }

        private static void TestSortingStrategyOne(Stopwatch watch, bool? container, int amount)
        {
            var filename = @Environment.CurrentDirectory + $"/{amount}_students.csv";

            var students = DecideContainer(filename, container);

            var passedStudents = (IEnumerable<Student>)Activator.CreateInstance(students.GetType());
            var failedStudents = (IEnumerable<Student>)Activator.CreateInstance(students.GetType());

            watch.Restart();

            foreach (Student student in students)
            {
                if (student.FinalPointsAverage() >= 5)
                    passedStudents.Append(student);

                else failedStudents.Append(student);
            }

            watch.Stop();

            Console.WriteLine($"Time elapsed using {students.GetType().Name} sorting {amount:n0} students. {watch.Elapsed}");
        }       

        private static void TestStudentGeneration()
        {
            Stopwatch watch = new Stopwatch();

            MeasureGeneration(watch, 10_000);

            MeasureGeneration(watch, 100_000);

            MeasureGeneration(watch, 1_000_000);

            MeasureGeneration(watch, 10_000_000);
        }

        private static void TestContainers()
        {
            Stopwatch watch = new Stopwatch();

            TestContainer(watch, null, 10_000);
            TestContainer(watch, true, 10_000);
            TestContainer(watch, false, 10_000);
            Console.WriteLine();

            TestContainer(watch, null, 100_000);
            TestContainer(watch, true, 100_000);
            TestContainer(watch, false, 100_000);
            Console.WriteLine();

            TestContainer(watch, null, 1_000_000);
            TestContainer(watch, true, 1_000_000);
            TestContainer(watch, false, 1_000_000);
            Console.WriteLine();

            TestContainer(watch, null, 10_000_000);
            TestContainer(watch, true, 10_000_000);
            TestContainer(watch, false, 10_000_000);
            Console.WriteLine();
        }

        private static void TestContainer(Stopwatch watch, bool? container, int amount)
        {
            var filename = @Environment.CurrentDirectory + $"/{amount}_students.csv";

            watch.Restart();

            var students = DecideContainer(filename, container);

            using StreamWriter passedStudents = new StreamWriter($"passed_students_of_{amount}_using_{students.GetType().Name}.csv");
            using StreamWriter failedStudents = new StreamWriter($"failed_students_of_{amount}_using_{students.GetType().Name}.csv");

            foreach (Student student in students)
            {
                if (student.FinalPointsAverage() >= 5)
                    passedStudents.WriteLine(student.ToCsvString());

                else failedStudents.WriteLine(student.ToCsvString());
            }

            passedStudents.Close();
            failedStudents.Close();

            watch.Stop();

            Console.WriteLine($"Time elapsed using {students.GetType().Name} for {amount:n0} of students. {watch.Elapsed}");
        }

        private static IEnumerable<Student> DecideContainer(string filename, bool? container)
        {
            switch(container)
            {
                case null:
                    return new List<Student>(Student.ReadFromFile(filename));
                case true:
                    return new LinkedList<Student>(Student.ReadFromFile(filename));
                case false:
                    return new Queue<Student>(Student.ReadFromFile(filename));
            }
        }

        public static void GenerateStudents(int amount = 1_000_000)
        {
            using StreamWriter students = new StreamWriter($"{amount}_students.csv");

            students.WriteLine(STUDENT_CSV_HEADER);

            //generate random students
            for (int i = 0; i < amount; i++)
                students.WriteLine(new Student().ToCsvString());

            students.Close();
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
