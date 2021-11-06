using System;

namespace OneDimensionalOptimization.Extras
{
    public class OptimizationResult<T> where T : IComparable<T>
    {
        public Point<T> Point { get; set; }
        public long CalculationsCount { get; set; }
    }
}