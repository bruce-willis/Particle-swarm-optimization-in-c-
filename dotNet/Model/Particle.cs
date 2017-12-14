using System.Collections.Generic;

namespace dotNet.Model
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