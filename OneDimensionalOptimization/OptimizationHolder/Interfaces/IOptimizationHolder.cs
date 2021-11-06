using System;
using OneDimensionalOptimization.Extras;

namespace OneDimensionalOptimization.OptimizationHolder.Interfaces
{
    public interface IOptimizationHolder
    {
       OptimizationResult<double> Optimize(double start, double end);
    }
}