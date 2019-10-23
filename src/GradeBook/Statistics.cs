using System;

namespace GradeBook
{
    public class Statistics
    {
        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
            Console.WriteLine(Math.Min(number, Low));
        }
        public Statistics()
        {
            Count = 0;
            Sum = 0.0;
            High = double.MaxValue;
            Low = double.MinValue;      // check the MinValue of a double value
        }

        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    case var d when d >= 70.0:
                        return 'C';
                    case var d when d >= 60.0:
                        return 'D';
                    case var d when d >= 50.0:
                        return 'E';
                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Count;
    }
}