using System;

namespace dotNet.PSO
{
    public class PSO
    {
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

            var (globalbest, population) = problemSolver.ParticleSwarmOptimization();

            Console.WriteLine(@"Best particle");
            globalbest.Position.ForEach(x => Console.Write($@" {x}"));

            Console.WriteLine($@"Elapsed time is {(DateTime.Now - start).TotalSeconds} second(s)");
        }
    }
}