using System;
using System.Linq.Expressions;

namespace OneDimensionalOptimization.Extras
{
    public class Point<T> where T: IComparable<T>
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }
            
        public Point(){}

        public Point(T x, TargetFunction<T> exp)
        {
            X = x;
            Y = exp.Calculate(x);

        }

    }
}