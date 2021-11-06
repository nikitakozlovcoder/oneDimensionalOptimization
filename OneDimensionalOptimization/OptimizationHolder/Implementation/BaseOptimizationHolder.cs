using System;
using OneDimensionalOptimization.Extras;
using OneDimensionalOptimization.OptimizationHolder.Interfaces;
using OneDimensionalOptimization.OptimizationStrategy.Interfaces;

namespace OneDimensionalOptimization.OptimizationHolder.Implementation
{
    public class BaseOptimizationHolder : IOptimizationHolder
    {
        private readonly IOptimizationStrategy _optimizationStrategy;

        public BaseOptimizationHolder(IOptimizationStrategy optimizationStrategy)
        {
            _optimizationStrategy = optimizationStrategy;
        }

        public OptimizationResult<double> Optimize(double start, double end)
        {
            return _optimizationStrategy.Optimize(start, end);
        }
        
    }
}