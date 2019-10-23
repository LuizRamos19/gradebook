using System;
using System.Collections.Generic;
namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Luiz's Grade Book");
            book.GradeAdded += OnGradeAdded;
            // book.GradeAdded += OnGradeAdded;
            // book.GradeAdded -= OnGradeAdded;
            // book.GradeAdded += OnGradeAdded;
            // In delegate events we can only use plus add and plus subtract method,
            // pass a value to the variable is not permitted
            // book.GradeAdded = null;

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"The book name is {book.Name}");
            Console.WriteLine($"The average grade is ${stats.Average:N2}"); // apenas duas casa decimais depois da vírgula
            Console.WriteLine($"The lowest grade is ${stats.Low}");
            Console.WriteLine($"The highest grade is ${stats.High}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
            // var numbers = new[] { 3.1, 3.2, 3.3 };
            // var grades = new List<double>() { 12.7, 10.3, 6.11, 4.1 };  // a Collection List da lib do .NET nos fornece alguns métodos como .Add
            // grades.Add(56.1);
        }

        private static void EnterGrades(IBook book)
        {
            while (true)    // stop loop only if the user enter a 'q' letter to exit
            {
                Console.WriteLine("Enter a grade or 'q' to exit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
