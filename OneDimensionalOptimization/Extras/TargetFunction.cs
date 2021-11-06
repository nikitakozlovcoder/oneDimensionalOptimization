using System;

namespace OneDimensionalOptimization.Extras
{
    public class TargetFunction<T> where T: IComparable<T>
    {
        private readonly Func<T, T> _exp;

        public TargetFunction(Func<T, T> exp)
        {
            _exp = exp;
        }

        public T Calculate(T x)
        {
            return _exp(x);
        }
    }
}