using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LD3
{
    class Student
    {
        private const int SURNAME_LENGTH = -20;
        private const int NAME_LENGTH = -20;
        private const int FINAL_POINTS_LENGTH = 17;

        private static Random random;

        private string name;
        private string surname;
        private List<int> homeworks;
        private int exam;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public List<int> Homeworks { get => homeworks; set => homeworks = value; }
        public int Exam { get => exam; set => exam = value; }

        static Student()
        {
            random = new Random();
        }

        public Student()
        {
            Name = "NewName" + random.Next(10000);
            Surname = "NewSurname" + random.Next(10000);
            Homeworks = new List<int>();
            for (int i = 0; i < 4; i++) homeworks.Add(random.Next(1, 10));
            Exam = random.Next(1, 10);
        }

        public override string ToString()
        {
            string ok = $"{Surname} {Name,20}";

            Homeworks.ForEach(homework => ok += $"{homework,5}");

            ok += $"{Exam,5}";
            return ok;
        }

        public static Student CreateFromConsole()
        {
            var student = new Student();

            Console.Write("Input Student name: ");
            student.Name = Console.ReadLine();

            Console.Write("Input Student surname: ");
            student.Surname = Console.ReadLine();

            student.Homeworks = Helper.HandleHomeworksInput("Input Student homework results(1-10, 0 to stop): ");

            student.Exam = Helper.HandleIntegerInput("Input Student exam value(1-10): ", 1, 10);

            return student;
        }

        public static string ToTable(List<Student> students, bool average) 
        {
            StringBuilder table = new StringBuilder();

            table.Append($"{nameof(Surname),SURNAME_LENGTH} {nameof(Name),NAME_LENGTH} Final points ")
                .Append(average ? "(Avg.)\n" : "(Med.)\n")
                .Append('-', table.Length)
                .AppendLine();

            students.ForEach(student => table.Append(student.Display(average)).AppendLine());

            return table.ToString();
        }

        private string Display(bool average) => average ? $"{Surname,SURNAME_LENGTH} {Name,NAME_LENGTH} {FinalPointsAverage(),FINAL_POINTS_LENGTH:n2}"
                                                        : $"{Surname,SURNAME_LENGTH} {Name,NAME_LENGTH} {FinalPointsMedian(Homeworks.Count),FINAL_POINTS_LENGTH:n2}";

        private float FinalPointsAverage() => .3f * (float)Homeworks.Average() + .7f * Exam;

        //need index and index-1 because int/2 returns round up
        private float FinalPointsMedian(int count) => count % 2 == 0 ? (Homeworks[count / 2] + Homeworks[(count / 2) - 1]) / 2.0f : Homeworks[count / 2];
    }
}
