using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using dotNet.Extensions;
using dotNet.Model;

namespace dotNet.Service
{
    public class PSOSolver
    {
        private ProblemConfig _problemConfig;

        private Particle _globalBest;

        private List<Particle> _population;

        public void Initialize(ProblemConfig problemConfig)
        {
            _problemConfig = problemConfig;
            _globalBest = new Particle { Cost = double.MaxValue };

            _population = Enumerable.Range(0, _problemConfig.PopulationSize).Select(_ => new Particle
            {
                Position = RandomExtension.Uniform(_problemConfig.Problem.VariablesNumber,
                    _problemConfig.Problem.VariablesMinimum, _problemConfig.Problem.VariablesMaximum),
                Velocity = Enumerable.Repeat(0d, _problemConfig.Problem.VariablesNumber).ToList()
            }).ToList();

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
        
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public async Task<double> PerformIteraration()
        {
            await Task.Run(() =>
            {
                foreach (var particle in _population)
                {
                    particle.Velocity = particle.Velocity.Select(x => x * _problemConfig.InertiaWeight).ToList();

                    var tmp_per_rand = RandomExtension.Rand(_problemConfig.Problem.VariablesNumber)
                        .Select(x => x * _problemConfig.PersonalCoefficient).ToList();
                    var tmp_per_dif = particle.BestPosition.Zip(particle.Position, (x, y) => x - y).ToList();
                    var tmp_per = tmp_per_rand.Zip(tmp_per_dif, (x, y) => x * y).ToList();

                    var tmp_gl_rand = RandomExtension.Rand(_problemConfig.Problem.VariablesNumber)
                        .Select(x => x * _problemConfig.GlobalCoefficient).ToList();
                    var tmp_gl_dif = _globalBest.Position.Zip(particle.Position, (x, y) => x - y).ToList();
                    var tmp_gl = tmp_gl_rand.Zip(tmp_gl_dif, (x, y) => x * y).ToList();

                    var tmp = tmp_per.Zip(tmp_gl, (x, y) => x + y).ToList();

                    particle.Velocity = particle.Velocity.Zip(tmp, (x, y) => x + y).ToList();


                    particle.Position = particle.Position.Zip(particle.Velocity, (x, y) => x + y).ToList();
                    particle.Position.ApplyBounds(_problemConfig.Problem.VariablesMinimum,
                        _problemConfig.Problem.VariablesMaximum);


                    particle.Cost = _problemConfig.Problem.CostFunction(particle.Position);


                    if (particle.Cost < particle.BestCost)
                    {
                        particle.BestPosition = particle.Position.ToList();
                        particle.BestCost = particle.Cost;

                        if (particle.Cost < _globalBest.Cost)
                        {
                            _globalBest.Position = particle.Position.ToList();
                            _globalBest.Cost = particle.Cost;
                        }
                    }
                }
            });
            _problemConfig.InertiaWeight *= _problemConfig.InertiaWeightDamp;
            return _globalBest.Cost;
        }
    }
}