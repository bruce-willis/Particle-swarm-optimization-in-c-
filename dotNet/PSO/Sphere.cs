using System.Collections.Generic;
using System.Linq;

namespace dotNet.PSO
{
    public static class Sphere
    {
        public static double Calculate(List<double> list)
        {
            return list.Sum(x => x * x);
        }
    }
}