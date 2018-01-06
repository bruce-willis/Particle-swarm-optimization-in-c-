using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNet.Model
{
    public static class Functions
    {
        public static double SphereFunction(List<double> list)
        {
            return list.Sum(x => x * x);
        }

        public static string SphereFunctionLatex() => @"f(x)=f(x_1, ..., x_n)=\sum_{i=1}^{n}{x_i^2}";

        public static double GriewankFunction(List<double> list)
        {
            double prod = 1;
            for (int i = 0; i < list.Count; ++i)
            {
                prod *= Math.Cos(list[i] / Math.Sqrt(i + 1));
            }

            return list.Sum(e => e * e) / 4000 - prod + 1;
        }
        public static string GriewankFunctionLatex() => @"f(x) = f(x_1, ..., x_n) = 1 + \sum_{i=1}^{n} \frac{x_i^{2}}{4000} - \prod_{i=1}^{n}cos(\frac{x_i}{\sqrt{i}})";

        public static double RastriginFunction(List<double> list)
        {
            return list.Sum(x => x * x - 10 * Math.Cos(Math.PI * x) + 10);
        }
        public static string RastriginFunctionLatex() => @"f(x) = f(x_1, ..., x_n) = 10n + \sum_{i=1}^{n}(x_i^2 - 10cos(2\pi x_i))";

        public static double RosenbrockFunction(List<double> list)
        {
            double sum = 0;
            for (int i = 0; i < list.Count - 1; ++i)
            {
                sum += 100 * Math.Pow(list[i + 1] - list[i] * list[i], 2) + Math.Pow(list[i] - 1, 2);
            }

            return sum;
        }
        public static string RosenbrockFunctionLatex() => @"f(x) = f(x_1, ..., x_n) =\sum_{i=1}^{n-1}[100(x_{i+1} - x_i^2)^ 2 + (1 - x_i)^2]";
    }
}