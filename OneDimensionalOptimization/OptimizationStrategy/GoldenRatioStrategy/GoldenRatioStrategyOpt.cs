using System;
using System.Collections.Generic;
using System.Linq;
using OneDimensionalOptimization.Enums;
using OneDimensionalOptimization.Extras;
using OneDimensionalOptimization.OptimizationStrategy.Interfaces;

namespace OneDimensionalOptimization.OptimizationStrategy.GoldenRatioStrategy
{
    public class GoldenRatioStrategyOpt : IOptimizationStrategy
    {
        private readonly TargetFunction<double> _optFunc;
        private readonly Target _targetType;
        private readonly double _eps;
        private readonly double _c = (1 + Math.Sqrt(5)) / 2; 
        public GoldenRatioStrategyOpt(TargetFunction<double> optFunc, Target targetType, double eps)
        {
            _targetType = targetType;
            _eps = eps;
            _optFunc = optFunc;
        }

        public OptimizationResult<double> Optimize(double start, double end)
        {
            
            var a = new Point<double>(start, _optFunc);
            var b = new Point<double>(end, _optFunc);
            var x1 = b.X - (b.X - a.X) / _c;
            var x2 = a.X + (b.X - a.X) / _c;
            var firstPoint = new Point<double>(x1, _optFunc);   
            var secondPoint = new Point<double>(x2, _optFunc);
            var calculationCount = 2;
            while (NotOptimized(a, b, _eps))
            {
                switch (_targetType)
                {
                    case Target.Minimize:
                        if (firstPoint.Y < secondPoint.Y)
                        {
                            b = secondPoint;
                            secondPoint = firstPoint;
                            firstPoint = new Point<double>(CalcX(a.X, b.X, firstPoint.X), _optFunc);
                           
                        }
                        else
                        {
                            a = firstPoint;
                            firstPoint = secondPoint;
                            secondPoint = new Point<double>(CalcX(a.X, b.X, secondPoint.X), _optFunc);
                        }
                        break;
                    case Target.Maximize:
                        if (firstPoint.Y > secondPoint.Y)
                        {
                            b = secondPoint;
                            secondPoint = firstPoint;
                            firstPoint = new Point<double>(CalcX(a.X, b.X, firstPoint.X), _optFunc);
                        }
                        else
                        {
                            a = firstPoint;
                            firstPoint = secondPoint;
                            secondPoint = new Point<double>(CalcX(a.X, b.X, secondPoint.X), _optFunc);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_targetType));
                }
                if (NotOptimized(a, b, _eps))
                {
                    calculationCount++;
                }
            }
            
            return new OptimizationResult<double>
            {
                CalculationsCount = calculationCount,
                Point = new List<Point<double>>{firstPoint, a, b, secondPoint}.OrderBy(p=>_targetType == Target.Minimize ? p.Y: -p.Y).First()
            };
        }
        private static bool NotOptimized(Point<double> lPoint, Point<double> rPoint, double eps)
        {
            return rPoint.X - lPoint.X > eps;
        }

        private static double CalcX(double a, double b, double x)
        {
            return a + b - x;
        }

    }
}