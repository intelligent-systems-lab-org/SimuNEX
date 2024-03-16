namespace SimuNEX
{
    /// <summary>
    /// Interface for describing dynamic systems.
    /// </summary>
    public interface IDynamics
    {
        /// <summary>
        /// Applies initial conditions at the start of the physics simulation.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// Applys the accumulated inputs to the system.
        /// </summary>
        public void Step();

        /// <summary>
        /// Resets all states to their defaults.
        /// </summary>
        public void ResetAll();
    }
}
