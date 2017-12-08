using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace dotNet.PSO
{
    public class ProblemSolver
    {
        private readonly ProblemConfig _problemConfig;

        private Particle _globalBest;

        private List<Particle> _population;

        public ProblemSolver(ProblemConfig problemConfig)
        {
            _problemConfig = problemConfig;
        }

        public void Initialize()
        {
            _globalBest = new Particle { Cost = double.MaxValue };

            _population = Enumerable.Repeat(new Particle
            {
                Position = Random.Uniform(_problemConfig.Problem.VariablesNumber, _problemConfig.Problem.VariablesMinimum, _problemConfig.Problem.VariablesMaximum),
                Velocity = Enumerable.Repeat(0d, _problemConfig.Problem.VariablesNumber).ToList()
            }, _problemConfig.PopulationSize).ToList();

            foreach (var particle in _population)
            {
                particle.BestCost = particle.Cost = _problemConfig.Problem.CostFunction(particle.Position);
                particle.BestPosition = particle.Position.ToList();

                if (particle.Cost < _globalBest.Cost)
                {
                    _globalBest.Position = particle.Position.ToList();
                    _globalBest.Cost = particle.Cost;
                }
            }
        }

        public (Particle, List<Particle>) ParticleSwarmOptimization()
        {
            for (var iteration = 0; iteration < _problemConfig.MaxIterations; ++iteration)
            {
                //foreach (var particle in _population)
                //{
                //    particle.Velocity 
                //}
            }

            return (_globalBest, _population);
        }
    }
}