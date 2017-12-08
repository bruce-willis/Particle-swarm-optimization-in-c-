using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace dotNet.Extensions
{
    public static class RandomExtension
    {
        private static readonly Random Random = new Random();
        public static List<double> Uniform(int variablesNumber, double variablesMinimum, double variablesMaximum)
        {
            return Enumerable.Range(0,variablesNumber).Select( _ => variablesMinimum + Random.NextDouble() * (variablesMaximum - variablesMinimum)).ToList();
        }

        public static List<double> Rand(int variablesNumber)
        {
            return Enumerable.Range(0, variablesNumber).Select( _ => Random.NextDouble()).ToList();
        }
    }
}