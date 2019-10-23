using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    // by convention, a delegate method hava two parameter, the
    // object sender and the EventArgs
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        // private string name; // in c#, is convenient that, when a attribute or a method is public, it's needs to be in uppercase
        // public string Name
        // {
        //     get
        //     {
        //         return name;
        //     }
        //     set
        //     {
        //         // when we put a value to a property variable,
        //         // like `var Name = ""`, this access the set
        //         // property and C# implicit have a variable that
        //         // is called value
        //         // if (!String.IsNullOrEmpty(value))
        //         // {
        //         name = value;
        //         // }
        //     }
        // }
        // private string name;

        // the difference between using a field or a property is that
        // in the property we can declare that the read of that property
        // is public but the set is private, so another classes can only 
        // read that property and not pass a value for that property.
        // example below
        public string Name
        {
            get;
            // private set;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {

        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {

        }

        public override event GradeAddedDelegate GradeAdded;
        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }
        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }

    public class InMemoryBook : Book
    {
        // the constructor need to have the same name as the class name without an return or void type return
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':   // exist a difference between "" and '' in C#. "" means string, '' means char
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        public override void AddGrade(double grade)     //override on the method AddGrade from the abstract class
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        // defining a event
        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            // foreach (var grade in grades)
            // {
            //     result.Low = Math.Min(grade, result.Low);
            //     result.High = Math.Max(grade, result.High);
            //     result.Average += grade;
            // }
            for (var index = 0; index < grades.Count; index++)
            {
                // if (grades[index] == 42.1)
                // {
                // goto done;  // esse goto faz com que a aplicação pule para o done:
                // }
                result.Add(grades[index]);
            };
            // done:
            return result;
        }

        private List<double> grades;

        // another way of doing a variable readonly
        // readonly string category = "Science";

        // another way of doing a variable that cannot be change
        // is to use the const attribute
        // by convention, all const variables has it's name in UPPERCASE
        public const string CATEGORY = "Science";
    }
}