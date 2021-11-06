using System;
using OneDimensionalOptimization.Enums;
using OneDimensionalOptimization.Extras;
using OneDimensionalOptimization.OptimizationStrategy.Interfaces;

namespace OneDimensionalOptimization.OptimizationStrategy.GoldenRatioStrategy
{
    public class GoldenRatioStrategy : IOptimizationStrategy
    {
        private readonly TargetFunction<double> _optFunc;
        private readonly Target _targetType;
        private readonly double _eps;
        private readonly double _c = (1 + Math.Sqrt(5)) / 2; 
        public GoldenRatioStrategy(TargetFunction<double> optFunc, Target targetType, double eps)
        {
            _targetType = targetType;
            _eps = eps;
            _optFunc = optFunc;
        }

        public OptimizationResult<double> Optimize(double start, double end)
        {
            var pointToSubtr = new Point<double>(); 
            var a = new Point<double>(start, _optFunc);
            var b = new Point<double>(end, _optFunc);
            var x1 = a.X + (b.X - a.X) / _c;
            var x2 = b.X - (b.X - a.X) / _c;
            var firstPoint = new Point<double>(x1, _optFunc);   
            var secondPoint = new Point<double>(x2, _optFunc);
            var thirdPoint = new Point<double>();
            if (NotOptimized(a, b, _eps))
            {
                switch (_targetType) 
                {
                    case Target.Maximize:
                        if (firstPoint.Y > secondPoint.Y)
                        {
                            a = secondPoint;
                            pointToSubtr = firstPoint;
                            var newX = CalcX(a.X, b.X, firstPoint.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }
                        else
                        {
                            b = firstPoint;
                            pointToSubtr = secondPoint;
                            var newX = CalcX(a.X, b.X, secondPoint.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }
                        break;
                    case Target.Minimize:
                        if (firstPoint.Y < secondPoint.Y)
                        {
                            a = secondPoint;
                            pointToSubtr = firstPoint;
                            var newX = CalcX(a.X, b.X, firstPoint.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }
                        else
                        {
                            b = firstPoint;
                            pointToSubtr = secondPoint;
                            var newX = CalcX(a.X, b.X, secondPoint.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_targetType));
                }
            }
            while (NotOptimized(a, b, _eps))
            {
                switch (_targetType)
                {
                    case Target.Maximize:
                        if (thirdPoint.Y > pointToSubtr.Y)
                        {
                            if (thirdPoint.X > pointToSubtr.X)
                            {
                                a = pointToSubtr;
                            }
                            else
                            {
                                b = pointToSubtr;
                            }
                            pointToSubtr = thirdPoint;
                            var newX = CalcX(a.X, b.X, pointToSubtr.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }
                        else
                        {
                            if (thirdPoint.X > pointToSubtr.X)
                            {
                                b = thirdPoint;
                            }
                            else
                            {
                                a = thirdPoint;
                            }
                            var newX = CalcX(a.X, b.X, pointToSubtr.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }

                        break;
                    case Target.Minimize:
                        if (thirdPoint.Y < pointToSubtr.Y)
                        {
                            if (thirdPoint.X > pointToSubtr.X)
                            {
                                a = pointToSubtr;
                            }
                            else
                            {
                                b = pointToSubtr;
                            }

                            pointToSubtr = thirdPoint;
                            var newX = CalcX(a.X, b.X, pointToSubtr.X);
                            thirdPoint = new Point<double>(newX, _optFunc);

                        }
                        else
                        {
                            if (thirdPoint.X > pointToSubtr.X)
                            {
                                b = thirdPoint;
                            }
                            else
                            {
                                a = thirdPoint;
                            }

                            var newX = CalcX(a.X, b.X, pointToSubtr.X);
                            thirdPoint = new Point<double>(newX, _optFunc);
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_targetType));
                }
            }
            var xBetween = a.X + (b.X - a.X) / 2;
            return new OptimizationResult<double>
            {
                CalculationsCount = 0,
                Point = new Point<double>(xBetween, _optFunc)
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