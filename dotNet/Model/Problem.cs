using System;
using System.Collections.Generic;

namespace dotNet.Model
{
    public class Problem
    {
        /// <summary>
        /// Cost function
        /// </summary>
        public Func<List<double>, double> CostFunction { get; set; }

        /// <summary>
        /// Number of decision variables 
        /// </summary>
        public int VariablesNumber { get; set; }

        /// <summary>
        /// Lower bound of variables
        /// </summary>
        public double VariablesMinimum { get; set; }

        /// <summary>
        /// Upper bound of variables
        /// </summary>
        public double VariablesMaximum { get; set; }

    }
}