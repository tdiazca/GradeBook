using System; // everything derived from System in C#
using System.Collections.Generic;
using System.IO; // File lives in this class

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args); // create an event to announce im passing a grade!

    public class NamedObject      // : System.Object   (no need to specify, but ultimate base class is object)
    {
        public NamedObject(string name) // constructor
        {
            Name = name;
        }

        public string Name // so we allow access to name from outside while protecting it!
            // Name now is a property, NOT a field
        {
            get; // getter
            set; // setter ; set this property
         }
 
    }

    public interface IBook // by convention, Interfaces start with 'I'
        // pure implementation
    {
        void AddGrade(double grade);
        Statistics GetStactistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook  // only one base class; as many interfaces as we want
    {
        public Book(string name): base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
            // abstract method allows derived methods behave differently (polymorphism)
        public abstract Statistics GetStatistics(); // virtual says method or event WILL be implemented; otherwise, abstract
    }

    public class DiskBook : Book
    {   
        public DiskBook(string name) : base(name) // constructor
        {
        }

        public override event GradeAddedDelegate GradeAdded;
        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt")) // open file give it a name returns an object to write at the end of that file
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                } // with using, file is closed at the end automatically. Otherwise File.Close or File.Dispose
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine(); // a line at a time
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }

    }
    
    // better have different classes on different files!

    public class InMemoryBook : Book // specify base class (a Book is a NamedObject) and interfaces after a ,
    {
        public InMemoryBook(string name) : base(name) // constructor method 
        {
            grades = new List<double>();
            Name = name; // this. to diff between the parameter (above) and the object (here)
        }

        public void AddLetterGrade(char letter)
        {
            switch(letter)
            {
                case 'A': // '' is a char, with "" is a string
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default: // if the letter is not A,B or C
                    AddGrade(0);
                    break;
            }

        }

        public override void AddGrade(double grade) // method overriding an abstract method of the class
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs()); // this 'indicates I am the sender' ; 
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}"); // nameof allows passing the name as string
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics() // Not void; instead, I want a new object returned of type 'Statistics', which is a class I created somewhere else.
        {
            var result = new Statistics();

            for(var index = 0; index < grades.Count; index += 1)
            {
                result.Add(grades[index]);
            } 

            return result;
        }

        //public - adding public, makes field avail outside of the class

        private List<double> grades; // private - field not accessible outside of the Book class
        public const string CATEGORY = "Science"; // capital to identifiy a const vs a variable


    // Example loops

            /*var index = 0;
            while(index < grades.Count) // if no grades, Count is 0 
                // index is 1 number behind total number of items since starts at 0
            {
                result.Low = Math.Min(grades[index], result.Low);
                result.High = Math.Max(grades[index], result.High);
                result.Average += grades[index];
                index += 1;
            } 

            result.Average /= grades.Count;

            return result;*/

            /*var index = 0;
            do // A DO / WHILE loop will execute at least once!
            {
                result.Low = Math.Min(grades[index], result.Low);
                result.High = Math.Max(grades[index], result.High);
                result.Average += grades[index];
                index += 1;
            } while(index < grades.Count); // index is 1 number behind total number of items since starts at 0

            result.Average /= grades.Count;

            return result;*/

            /*for (var index = 0; index < grades.Count; index += 1) // 1) var declaration, 2 is condition to  check, 3 operation to perform after each interation of the loop
            {
                if(grades[index] == 42.1)
                {
                    continue; //  this skips the code below for this number, then continues next iteration //goto xx;
                }

                result.Low = Math.Min(grades[index], result.Low);
                result.High = Math.Max(grades[index], result.High);
                result.Average += grades[index];

            } 

            result.Average /= grades.Count;

            return result;*/
        }
}