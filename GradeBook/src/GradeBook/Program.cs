using System; // compiler to look for types in this namespace
using System.Collections.Generic;

namespace GradeBook // where we are defining our classes etc
{

//// CLIENT CODE

    class Program // defines a type
    {
        static void Main(string[] args) //method - void if method does not return a val
        {

            InMemoryBook book2 = null; // Instanciate a class. null value in a var = Better avoid using this var.

            IBook book = new DiskBook("Teresa's Grade Book");
            book.GradeAdded += OneGradeAdded; // subscribe to an event and listen to that

            EnterGrades(book);

            var stats = book.GetStatistics();

            //Console.WriteLine(InMemoryBook.CATEGORY); // only acess within the class cause its a constant. Cannot do book.Category
            Console.WriteLine($"For the book names{book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low:N1}");
            Console.WriteLine($"The highest grade is {stats.High:N1}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");

        }

        private static void EnterGrades(Book book) // Book is a base class
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine(); // returns a string

                if (input == "q") // "q" because 'q' would be char type, not stribg
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade); // this might trow an exception!    see code           
                }
                catch (ArgumentException ex) // catches exception type that I know might arise
                {
                    Console.WriteLine(ex.Message);
                    // throw; // rethrow exception IF you want the program to actually terminate
                }
                catch (FormatException ex)  // catches exception type that I know might arise
                {
                    Console.WriteLine(ex.Message);
                }
                finally // usufeul for doing stuff like closing a program etc
                {
                    Console.WriteLine("Done");
                }
            }
        }

        static void OneGradeAdded(object sender, EventArgs e) // event handeler
        {
            Console.WriteLine("A grade was added");
        }
    }
}


