using System;

namespace GradeBook
{
    public class Statistics // computes the following
    {
        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public double High;
        public double Low;
        public char Letter // compute letter grade
        {
            get
            {
                switch(Average)
                {
                    case var d when d >= 90.0:
                        return 'A';

                    case var d when d >= 80.0:
                        return 'B';

                    case var d when d >= 70.00:
                        return 'C';

                    case var d when d >= 60.00:
                        return 'D';
                
                    default:
                        return 'F';
                }
            }
        }

        public double Sum;
        public int Count;

        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }

        public Statistics() // constructor
        {
            Count = 0;
            Sum = 0.0;
            High = double.MinValue; // starts with the smallest possible double flotting point val
            Low = double.MaxValue;
        }
    }
}