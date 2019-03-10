using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using introToLinq.Models;

namespace introToLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            //Query syntax:
            IEnumerable<int> numQuery1 =
                from num in numbers
                where num % 2 == 0
                orderby num
                select num;

            //Method syntax:
            IEnumerable<int> numQuery2 = numbers.Where(num => num % 2 == 0).OrderBy(n => n);

            foreach (int i in numQuery1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            foreach (int i in numQuery2)
            {
                Console.Write(i + " ");
            }

            // Keep the console open in debug mode.
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();


            // Create the first data source.
            List<Student> students = new List<Student>()
            {
                new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
                new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
                new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
                new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
                new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
                new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
                new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
                new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
                new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
                new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
                new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
                new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}}
            };

            // Create the second data source.
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher { First="Ann", Last="Beebe", ID=945, City="Seattle" },
                new Teacher { First="Alex", Last="Robinson", ID=956, City="Redmond" },
                new Teacher { First="Michiyo", Last="Sato", ID=972, City="Tacoma" }
            };



            // Create the query.
            var peopleInSeattle = (from student in students
                                   where student.City == "Seattle"
                                   select student.Last)
                        .Concat(from teacher in teachers
                                where teacher.City == "Seattle"
                                select teacher.Last);

            Console.WriteLine("The following students and teachers live in Seattle:");
            // Execute the query.
            foreach (var person in peopleInSeattle)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();


            // Create the query.
            var studentsToXML = new XElement("Root",
                from student in students
                let scores = string.Join(",", student.Scores)
                select new XElement("student",
                           new XElement("First", student.First),
                           new XElement("Last", student.Last),
                           new XElement("Scores", scores)
                        ) // end "student"
                    ); // end "Root"

            // Execute the query.
            Console.WriteLine(studentsToXML);

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();


            // Data source.
            double[] radii = { 1, 2, 3 };

            // Query.
            IEnumerable<string> query =
                from rad in radii
                select $"Area = {rad * rad * Math.PI:F2}";

            // Query execution. 
            foreach (string s in query)
                Console.WriteLine(s);

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();

            IEnumerable<Student> studentQuery =
                from student in students
                where student.Scores[0] > 90
                select student;

            foreach (Student student in studentQuery)
            {
                Console.WriteLine("{0}, {1}", student.Last, student.First);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            IEnumerable<Student> studentQuery2 =
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                select student;

            foreach (Student student in studentQuery2)
            {
                Console.WriteLine("{0}, {1}", student.Last, student.First);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            IEnumerable<Student> studentQuery3 =
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                orderby student.Last ascending
                select student;

            foreach (Student student in studentQuery3)
            {
                Console.WriteLine("{0}, {1} {2}", student.Last, student.First, student.Scores[0]);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            IEnumerable<Student> studentQuery4 =
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                orderby student.Scores[0] descending
                select student;

            foreach (Student student in studentQuery4)
            {
                Console.WriteLine("{0}, {1} {2}", student.Last, student.First, student.Scores[0]);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();



            Console.WriteLine();

            // Auto typage
            var studentQuery5 = 
                from student in students 
                group student by student.Last[0];

            // studentGroup is a IGrouping<char, Student>
            foreach (var studentGroup in studentQuery5)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (Student student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}",
                              student.Last, student.First);
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            foreach (var groupOfStudents in studentQuery5)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine("   {0}, {1}",
                        student.Last, student.First);
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();


            Console.WriteLine();

            // Auto typage
            var studentQuery6 =
                from student in students
                group student by student.Last[0] into studentGroup
                orderby studentGroup.Key
                select studentGroup;

            // studentGroup is a IGrouping<char, Student>
            foreach (var groupOfStudents in studentQuery6)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine("   {0}, {1}",
                        student.Last, student.First);
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();


            Console.WriteLine();

            // Auto typage
            var studentQuery7 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                    student.Scores[2] + student.Scores[3]
                where totalScore / 4 < student.Scores[0]
                select student.Last + " " + student.First;

            foreach (string s in studentQuery7)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            // Auto typage
            var studentQuery8 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                    student.Scores[2] + student.Scores[3]
                select totalScore;

            double averageScore = studentQuery8.Average();
            Console.WriteLine("Class average score = {0}", averageScore);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            // Auto typage
            IEnumerable<string> studentQuery9 =
                from student in students
                where student.Last == "Garcia"
                select student.First;

            Console.WriteLine("The Garcias in the class are:");
            foreach (string s in studentQuery9)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine();

            // Auto typage
            var studentQuery10 =
                from student in students
                let x = student.Scores[0] + student.Scores[1] +
                    student.Scores[2] + student.Scores[3]
                where x > averageScore
                select new { id = student.ID, score = x };

            foreach (var item in studentQuery10)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", item.id, item.score);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
