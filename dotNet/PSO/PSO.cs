using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;

namespace dotNet.PSO
{
    // ReSharper disable once InconsistentNaming
    public class PSO
    {
        private readonly List<double> _costs;
        public readonly Particle GlobalBest;
        public double ElapsedTime { get; private set; }
        public PSO()
        {
            var start = DateTime.Now;

            var problem = new Problem
            {
                CostFunction = Sphere.Calculate,
                VariablesNumber = 10,
                VariablesMinimum = -5,
                VariablesMaximum = 5
            };

            var problemConfiguration = new ProblemConfig
            {
                Problem = problem,
                MaxIterations = 200,
                PopulationSize = 50,
                PersonalCoefficient = 1.5,
                GlobalCoefficient = 2,
                InertiaWeight = 1,
                InertiaWeightDamp = 0.995
            };

            var problemSolver = new ProblemSolver(problemConfiguration);

            problemSolver.Initialize();

            (GlobalBest, _costs) = problemSolver.ParticleSwarmOptimization();
            Console.WriteLine(@"Best particle");
            GlobalBest.Position.ForEach(x => Console.Write($@" {x}"));


            ElapsedTime = (DateTime.Now - start).TotalSeconds;
            Console.WriteLine($@"Elapsed time is {(DateTime.Now - start).TotalSeconds} second(s)");
        }

        public ChartValues<ObservableValue> GetBestCosts()
        {
            return new ChartValues<ObservableValue>(_costs.Select(x => new ObservableValue(x)));
        }
    }
}