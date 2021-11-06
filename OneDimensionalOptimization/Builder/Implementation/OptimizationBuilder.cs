using System;
using OneDimensionalOptimization.Builder.Interfaces;
using OneDimensionalOptimization.Enums;
using OneDimensionalOptimization.Extras;
using OneDimensionalOptimization.OptimizationHolder.Interfaces;
using OneDimensionalOptimization.OptimizationStrategy.DichotomyStrategy;
using OneDimensionalOptimization.OptimizationStrategy.GoldenRatioStrategy;
using OneDimensionalOptimization.OptimizationStrategy.Interfaces;
using OneDimensionalOptimization.OptimizationHolder.Implementation;

namespace OneDimensionalOptimization.Builder.Implementation
{
    public class OptimizationBuilder : IOptimizationBuilder

    {
        private Target? _targetType;
        private OptimizationMethod? _optimizationMethod;
        private Func<double, double> _expression;
        private double? _eps;
        private double? _precision;

        public IOptimizationBuilder Target(Target targetType)
        {
            _targetType = targetType;
            return this;
        }

        public IOptimizationBuilder Method(OptimizationMethod optimizationMethod)
        {
            _optimizationMethod = optimizationMethod;
            return this;
        }

        public IOptimizationBuilder Expression(Func<double, double> expression)
        {
            _expression = expression;
            return this;
        }
        
        public IOptimizationBuilder Eps(double eps)
        {
            _eps = eps;
            return this;
        }
        
        public IOptimizationBuilder Precision(double precision)
        {
            _precision = precision;
            return this;
        }

        public IOptimizationHolder Build()
        {
            if (!_targetType.HasValue || !_eps.HasValue || _expression == null || !_optimizationMethod.HasValue)
            {
                throw new ArgumentException("Cant build object");
            }
            IOptimizationStrategy optimizationStrategy = _optimizationMethod switch
            {
                OptimizationMethod.Dichotomy => new DichotomyStrategy(new TargetFunction<double>(_expression), _targetType.Value, _eps.Value, _precision?? _eps.Value),
                OptimizationMethod.GoldenRatio => new GoldenRatioStrategyOpt(new TargetFunction<double>(_expression), _targetType.Value,_precision?? _eps.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(_optimizationMethod))
            };
            return new BaseOptimizationHolder(optimizationStrategy);
        }
    }
}