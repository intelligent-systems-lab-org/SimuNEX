namespace SimuNEX
{
    /// <summary>
    /// The domain of the data in a <see cref="ModelPort{T}"/>.
    /// </summary>
    public enum Signal
    {
        Virtual,
        Mechanical,
        Electrical,
        Thermal,
        Data
    }

    /// <summary>
    /// Interface for defining ports in a system.
    /// </summary>
    public interface IPort
    {
        /// <summary>
        /// The name of the port.
        /// </summary>
        string name { get; }

        /// <summary>
        /// The domain of the data.
        /// </summary>
        Signal signal { get; set; }

        /// <summary>
        /// The data in the port.
        /// </summary>
        float[] data { get; set; }
    }

    /// <summary>
    /// Interface for defining models that require initialization callbacks.
    /// </summary>
    public interface IModelInitialization
    {
        /// <summary>
        /// Initialization callback function for <see cref="Runner"/>.
        /// </summary>
        public void Init();
    }
}
