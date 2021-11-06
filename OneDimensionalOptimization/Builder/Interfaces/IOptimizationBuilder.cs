using System;
using OneDimensionalOptimization.Enums;
using OneDimensionalOptimization.OptimizationHolder.Interfaces;

namespace OneDimensionalOptimization.Builder.Interfaces
{
    public interface IOptimizationBuilder
    {
        IOptimizationBuilder Target(Target targetType);
        IOptimizationBuilder Method(OptimizationMethod optimizationMethod);
        IOptimizationBuilder Expression(Func<double, double> expression);
        IOptimizationBuilder Eps(double eps);
        IOptimizationBuilder Precision(double precision);
        IOptimizationHolder Build();
    }
}