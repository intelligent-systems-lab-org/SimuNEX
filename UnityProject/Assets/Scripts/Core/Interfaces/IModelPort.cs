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

        object data { get; set; }
    }

    public interface IModelOutput
    {
        object data { get; set; }
    }

    public interface IModelInput
    {
        object data { get; set; }
    }
}
