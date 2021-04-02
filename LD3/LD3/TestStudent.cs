using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace LD3
{
    class TestStudent
    {
        private const string STUDENT_CSV_HEADER = "Name,Surname,H1,H2,H3,H4,H5,Exam";

        private static List<Student> students10k, students100k, students1M, students10M;

        private static Stopwatch watch;

        public static void TestSorting()
        {
            if (students10k == null || students10k.Count < 10_000)
                throw new ArgumentNullException();

            TestSortingStrategyOne(watch, students10k, null, 10_000);
            TestSortingStrategyTwoList(watch, students10k, 10_000);
            TestSortingStrategyOne(watch, students10k, true, 10_000);
            TestSortingStrategyTwoLinkedList(watch, students10k, 10_000);
            TestSortingStrategyOne(watch, students10k, false, 10_000);
            TestSortingStrategyTwoQueue(watch, students10k, 10_000);
            Console.WriteLine();

            TestSortingStrategyOne(watch, students100k, null, 100_000);
            TestSortingStrategyTwoList(watch, students100k, 100_000);
            TestSortingStrategyOne(watch, students100k, true, 100_000);
            TestSortingStrategyTwoLinkedList(watch, students100k, 100_000);
            TestSortingStrategyOne(watch, students100k, false, 100_000);
            TestSortingStrategyTwoQueue(watch, students100k, 100_000);
            Console.WriteLine();

            TestSortingStrategyOne(watch, students1M, null, 1_000_000);
            TestSortingStrategyTwoList(watch, students1M, 1_000_000);
            TestSortingStrategyOne(watch, students1M, true, 1_000_000);
            TestSortingStrategyTwoLinkedList(watch, students1M, 1_000_000);
            TestSortingStrategyOne(watch, students1M, false, 1_000_000);
            TestSortingStrategyTwoQueue(watch, students1M, 1_000_000);
            Console.WriteLine();

            TestSortingStrategyOne(watch, students10M, null, 10_000_000);
            TestSortingStrategyTwoList(watch, students10M, 10_000_000);
            TestSortingStrategyOne(watch, students10M, true, 10_000_000);
            TestSortingStrategyTwoLinkedList(watch, students10M, 10_000_000);
            TestSortingStrategyOne(watch, students10M, false, 10_000_000);
            TestSortingStrategyTwoQueue(watch, students10M, 10_000_000);
            Console.WriteLine();
        }

        private static void TestSortingStrategyOne(Stopwatch watch, List<Student> s, bool? container, int amount)
        {
            var students = DecideContainer(s, container);

            var passedStudents = (IEnumerable<Student>)Activator.CreateInstance(students.GetType());
            var failedStudents = (IEnumerable<Student>)Activator.CreateInstance(students.GetType());

            watch.Restart();

            passedStudents = students.Where(student => student.FinalPointsAverage() >= 5);
            failedStudents = students.Where(student => student.FinalPointsAverage() < 5);

            watch.Stop();

            Console.Write($"Strategy 1. Time elapsed using {students.GetType().Name} sorting {amount:n0} students. {watch.Elapsed}.");
            Console.WriteLine($" Passed: {passedStudents.Count()} Failed: {failedStudents.Count()}");
        }

        private static void TestSortingStrategyTwoList(Stopwatch watch, List<Student> s, int amount)
        {
            var students = new List<Student>(s);

            watch.Restart();

            List<Student> failedStudents = new List<Student>(students.Where(student => student.FinalPointsAverage() < 5));
            students.RemoveAll(student => student.FinalPointsAverage() < 5);

            watch.Stop();

            Console.Write($"Strategy 2. Time elapsed using {students.GetType().Name} sorting {amount:n0} students. {watch.Elapsed}.");
            Console.WriteLine($" Passed: {students.Count()} Failed: {failedStudents.Count()}");
        }

        private static void TestSortingStrategyTwoLinkedList(Stopwatch watch, List<Student> s, int amount)
        {
            var students = new LinkedList<Student>(s);
            LinkedListNode<Student> nextStudent;

            watch.Restart();

            var student = students.First;

            LinkedList<Student> failedStudents = new LinkedList<Student>();

            while (student != null)
            {
                nextStudent = student.Next;
                if (student.Value.FinalPointsAverage() < 5)
                {
                    failedStudents.AddLast(student.Value);
                    students.Remove(student);
                }
                student = nextStudent;
            }

            watch.Stop();

            Console.Write($"Strategy 2. Time elapsed using {students.GetType().Name} sorting {amount:n0} students. {watch.Elapsed}.");
            Console.WriteLine($" Passed: {students.Count()} Failed: {failedStudents.Count()}");
        }

        private static void TestSortingStrategyTwoQueue(Stopwatch watch, List<Student> s, int amount)
        {
            var students = new Queue<Student>(s);

            watch.Restart();

            Queue<Student> failedStudents = new Queue<Student>(students.Where(student => student.FinalPointsAverage() < 5));
            students = new Queue<Student>(students.Where(student => student.FinalPointsAverage() >= 5));

            watch.Stop();

            Console.Write($"Strategy 2. Time elapsed using {students.GetType().Name} sorting {amount:n0} students. {watch.Elapsed}.");
            Console.WriteLine($" Passed: {students.Count()} Failed: {failedStudents.Count()}");
        }

        public static void TestGeneration()
        {
            Stopwatch watch = new Stopwatch();

            TestStudentGeneration(watch, 10_000);

            TestStudentGeneration(watch, 100_000);

            TestStudentGeneration(watch, 1_000_000);

            TestStudentGeneration(watch, 10_000_000);
        }

        private static void TestStudentGeneration(Stopwatch watch, int amount)
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

        public static void TestContainers()
        {
            if (students10k == null || students10k.Count < 10_000)
                throw new ArgumentNullException();

            TestContainer(watch, students10k, null, 10_000);
            TestContainer(watch, students10k, true, 10_000);
            TestContainer(watch, students10k, false, 10_000);
            Console.WriteLine();

            TestContainer(watch, students100k, null, 100_000);
            TestContainer(watch, students100k, true, 100_000);
            TestContainer(watch, students100k, false, 100_000);
            Console.WriteLine();

            TestContainer(watch, students1M, null, 1_000_000);
            TestContainer(watch, students1M, true, 1_000_000);
            TestContainer(watch, students1M, false, 1_000_000);
            Console.WriteLine();

            TestContainer(watch, students10M, null, 10_000_000);
            TestContainer(watch, students10M, true, 10_000_000);
            TestContainer(watch, students10M, false, 10_000_000);
            Console.WriteLine();
        }

        private static void TestContainer(Stopwatch watch, List<Student> s, bool? container, int amount)
        {
            watch.Restart();

            var students = DecideContainer(s, container);

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

            Console.WriteLine($"Time elapsed sorting and writing using {students.GetType().Name} for {amount:n0} students. {watch.Elapsed}");
        }

        private static IEnumerable<Student> DecideContainer(List<Student> students, bool? container)
        {
            switch (container)
            {
                case null:
                    return new List<Student>(students);
                case true:
                    return new LinkedList<Student>(students);
                case false:
                    return new Queue<Student>(students);
            }
        }

        public static void GenerateStudentFiles()
        {
            GenerateStudents(10_000);
            GenerateStudents(100_000);
            GenerateStudents(1_000_000);
            GenerateStudents(10_000_000);
        }

        private static void GenerateStudents(int amount)
        {
            using StreamWriter students = new StreamWriter($"{amount}_students.csv");

            students.WriteLine(STUDENT_CSV_HEADER);

            //generate random students
            for (int i = 0; i < amount; i++)
                students.WriteLine(new Student().ToCsvString());

            students.Close();
        }

        public static void PopulateStudents()
        {
            Console.WriteLine("Populating students for testing");

            watch = new Stopwatch();

            string filename;

            filename = @Environment.CurrentDirectory + $"/{10_000}_students.csv";
            students10k = new List<Student>(Student.ReadFromFile(filename));

            Console.WriteLine("Finished populating 10,000 students for testing");

            filename = @Environment.CurrentDirectory + $"/{100_000}_students.csv";
            students100k = new List<Student>(Student.ReadFromFile(filename));

            Console.WriteLine("Finished populating 100,000 students for testing");

            filename = @Environment.CurrentDirectory + $"/{1_000_000}_students.csv";
            students1M = new List<Student>(Student.ReadFromFile(filename));

            Console.WriteLine("Finished populating 1,000,000 students for testing");

            filename = @Environment.CurrentDirectory + $"/{10_000_000}_students.csv";
            students10M = new List<Student>(Student.ReadFromFile(filename));

            Console.WriteLine("Finished populating 10,000,000 students for testing");
        }
    }
}
