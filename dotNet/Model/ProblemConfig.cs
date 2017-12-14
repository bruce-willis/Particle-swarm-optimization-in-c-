namespace dotNet.Model
{
    public class ProblemConfig
    {
        /// <summary>
        /// Problem
        /// </summary>
        public Problem Problem { get; set; }

        /// <summary>
        /// Maximum number of iterations
        /// </summary>
        public int MaxIterations { get; set; } = 100;

        /// <summary>
        /// Population size (Swarm size)
        /// </summary>
        public int PopulationSize { get; set; } = 100;

        /// <summary>
        /// Personal learing coefficient
        /// </summary>
        public double PersonalCoefficient { get; set; } = 1.4962;

        /// <summary>
        /// Global learing coefficient
        /// </summary>
        public double GlobalCoefficient { get; set; } = 1.4962;

        /// <summary>
        /// Inertia weight
        /// </summary>
        public double InertiaWeight { get; set; } = 0.7298;

        /// <summary>
        /// Inertia weight damping ratio
        /// </summary>
        public double InertiaWeightDamp { get; set; } = 1.0;
    }
}