using System;
using System.Collections.Generic;

namespace LD3
{
    class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student> { new Student(), new Student(), new Student() };

            do { } while (menu(students));

            //Console.WriteLine(Student.CreateFromConsole());
        }

        private static bool menu(List<Student> students)
        {
            Console.WriteLine("\n 0 - Clear console.\n" +
                                " 1 - Add new student.\n" +
                                " 2 - Show students average final score.\n" +
                                " 3 - Show students median final score.\n" +
                                " 4 - Add random student.\n" +
                                "-1 - Exit.\n");

            switch (Helper.HandleIntegerInput("Input Your choice: "))
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
                    Console.WriteLine(Student.ToTable(students, false));
                    break;

                case 4:
                    var s = new Student();
                    students.Add(s);
                    Console.WriteLine(s);
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
