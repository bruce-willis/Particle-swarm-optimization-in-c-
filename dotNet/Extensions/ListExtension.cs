using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNet.Extensions
{
    public static class ListExtension
    {
        public static void ApplyBounds(this List<double> list, double variablesMinimum, double variablesMaximum)
        {
            for (var i = 0; i < list.Count; ++i)
            {
                list[i] = Math.Max(variablesMinimum, Math.Min(list[i], variablesMaximum));
            }
        }
    }
}