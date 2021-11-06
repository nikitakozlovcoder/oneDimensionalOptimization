using System;
using OneDimensionalOptimization.Extras;

namespace OneDimensionalOptimization.OptimizationStrategy.Interfaces
{
    public interface IOptimizationStrategy
    {
        OptimizationResult<double> Optimize(double start, double end);
    }
}