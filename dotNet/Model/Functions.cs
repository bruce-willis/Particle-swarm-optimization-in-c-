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

        public static double GriewankFunction(List<double> list)
        {
            double prod = 1;
            for (int i = 0; i < list.Count; ++i)
            {
                prod *= Math.Cos((list[i] - 100)/ Math.Sqrt(i + 1));
            }

            return (list.Sum(e => (e - 100) * (e - 100))) / 4000 - prod + 1;
        }

        public static double RastriginFunction(List<double> list)
        {
            return list.Sum(x => x*x - 10 * Math.Cos(Math.PI * x) + 10);
        }

        public static double RosenbrockFunction(List<double> list)
        {
            double sum = 0;
            for (int i = 0; i < list.Count - 1; ++i)
            {
                sum += 100 * Math.Pow((list[i + 1] - list[i] * list[i]), 2) + Math.Pow(list[i] - 1, 2);
            }

            return sum;
        }
    }
}