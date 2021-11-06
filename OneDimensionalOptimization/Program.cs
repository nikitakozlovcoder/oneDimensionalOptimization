using System;
using OneDimensionalOptimization.Builder.Implementation;
using OneDimensionalOptimization.Enums;

namespace OneDimensionalOptimization
{
    internal static class Program
    {
        private static void Main(string[] _)
        {
            while (true)
            {
                Console.WriteLine("eps:");
                var eps = Convert.ToDouble(Console.ReadLine()?.Replace(".", ","));
                Console.WriteLine("l0:");
                var l0 = Convert.ToDouble(Console.ReadLine()?.Replace(".", ","));
                Console.WriteLine("a:");
                var a = Convert.ToDouble(Console.ReadLine()?.Replace(".", ","));
                Console.WriteLine("b:");
                var b = Convert.ToDouble(Console.ReadLine()?.Replace(".", ","));
                Console.WriteLine("Maximize? [y/n]:");
                var isMaximize = Console.ReadLine() == "y";
            
            
                var builder = new OptimizationBuilder()
                     //.Expression(x =>Math.Pow(x, 4.0) - Math.Pow(x, 3.0) + 5 * Math.Pow(x, 2.0) + x - 1)
                     //.Expression(x =>3*x + 2*Math.Pow(x, 2.0) - 4)
                     //.Expression(x =>x*x + 4*x - 3)
                     //.Expression(x =>5*x + (x+1)*(x+1) - 1)
                    .Expression(x =>x*x -2*x+5)
                    .Target(isMaximize? Target.Maximize : Target.Minimize)
                    .Eps(eps)
                    .Precision(l0);
            
                var dichotomy = builder.Method(OptimizationMethod.Dichotomy).Build();
                var goldenRation = builder.Method(OptimizationMethod.GoldenRatio).Build();
                var res1 = dichotomy.Optimize(a, b);
                Console.WriteLine($"Dichotomy\nPoint: X:{res1.Point.X} Y:{res1.Point.Y}\nCount: {res1.CalculationsCount}");
                var res2 = goldenRation.Optimize(a, b);
                Console.WriteLine($"GoldenRatio\nPoint: X:{res2.Point.X} Y:{res2.Point.Y}\nCount: {res2.CalculationsCount}");
            }
        }
    }
}