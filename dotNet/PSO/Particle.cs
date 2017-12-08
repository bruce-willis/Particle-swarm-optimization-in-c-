using System.Collections.Generic;
using System.Windows.Documents;

namespace dotNet.PSO
{
    public class Particle
    {
        public List<double> Position { get; set; }

        public List<double> Velocity { get; set; }

        public double Cost { get; set; }

        public List<double> BestPosition { get; set; }

        public double BestCost { get; set; }
    }
}