using System.Collections.Generic;

namespace LD3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Helper.GenerateStudents(10_000);
            Helper.GenerateStudents(100_000);
            Helper.GenerateStudents(1_000_000);
            Helper.GenerateStudents(10_000_000);*/

            var students = new List<Student>();

            do { } while (Helper.Menu(students));
        }
    }
}
