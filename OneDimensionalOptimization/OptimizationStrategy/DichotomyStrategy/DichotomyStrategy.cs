using System;
using OneDimensionalOptimization.Enums;
using OneDimensionalOptimization.Extras;
using OneDimensionalOptimization.OptimizationStrategy.Interfaces;

namespace OneDimensionalOptimization.OptimizationStrategy.DichotomyStrategy
{
    public class DichotomyStrategy : IOptimizationStrategy
    {
        private readonly TargetFunction<double> _optFunc;
        private readonly Target _targetType;
        private readonly double _eps;
        private readonly double _l;
        public DichotomyStrategy(TargetFunction<double> optFunc, Target targetType, double eps, double l)
        {
            _optFunc = optFunc;
            _targetType = targetType;
            _eps = eps;
            _l = l;
        }
        
        public OptimizationResult<double> Optimize(double start, double end)
        {
            var calculationsCount = 0;
            var a = new Point<double>(start, _optFunc);
            var b = new Point<double>(end, _optFunc);
            var canPrecalculate = CanPrecalculateIterationCount();
            var iterationsCount = canPrecalculate ? CalcIterationsCount(start, end) : 0;
            for (var i = 0; !canPrecalculate && NotOptimized(a, b, _eps) || i < iterationsCount; i++)
            {
                calculationsCount += 2;
                var (xLeft, xRight) = GetX(a.X, b.X, _eps);
                var lPoint = new Point<double>(xLeft, _optFunc);
                var rPoint = new Point<double>(xRight, _optFunc);
                switch (_targetType) 
                {
                    case Target.Minimize:
                        if (lPoint.Y > rPoint.Y)
                        {
                            a = lPoint;
                        }
                        else
                        {
                            b = rPoint;
                        }
                        break;
                    case Target.Maximize:
                        if (lPoint.Y < rPoint.Y)
                        {
                            a = lPoint;
                        }
                        else
                        {
                            b = rPoint;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_targetType));
                } 
            }
            var xBetween = a.X + (b.X - a.X) / 2;
            
            return new OptimizationResult<double>
            {
                CalculationsCount = calculationsCount,
                Point = new Point<double>(xBetween, _optFunc)
            };
        }
        private bool CanPrecalculateIterationCount()
        {
            return _eps < _l && Math.Abs(_eps - _l) > 0.001;
        }
        private static (double xLeft, double xRight) GetX(double a, double b, double eps)
        {
            var xLeft = a + (b - a) / 2 - eps / 2;
            var xRight = a + (b - a) / 2 + eps / 2;
            return (xLeft, xRight);
        }
        
        private static bool NotOptimized(Point<double> lPoint, Point<double> rPoint, double eps)
        {
            return rPoint.X - lPoint.X > eps;
        }

        private long CalcIterationsCount(double a, double b)
        {
            var nDouble = 2 / Math.Log(2) * Math.Log((b - a - _eps) / ( _l - _eps));
            var n = Convert.ToInt64(Math.Ceiling(nDouble));
            return (n%2 == 0 ? n : n+1)/2;
        }
        
    }
}