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
    /// Interface for defining ports in a <see cref="Model"/>.
    /// </summary>
    public interface IModelPort
    {
        /// <summary>
        /// The name of the port.
        /// </summary>
        string name { get; }

        /// <summary>
        /// Data size of the signal.
        /// </summary>
        int size { get; }

        /// <summary>
        /// The domain of the data.
        /// </summary>
        Signal signal { get; set; }

        /// <summary>
        /// The data in the port.
        /// </summary>
        object data { get; set; }
    }

    /// <summary>
    /// Interface for implementing output ports in <see cref="Model"/>.
    /// </summary>
    public interface IModelOutput
    {
        /// <summary>
        /// The data in the port.
        /// </summary>
        object data { get; set; }
    }

    /// <summary>
    /// Interface for implementing input ports in <see cref="Model"/>.
    /// </summary>
    public interface IModelInput
    {
        /// <summary>
        /// The data in the port.
        /// </summary>
        object data { get; set; }
    }
}
