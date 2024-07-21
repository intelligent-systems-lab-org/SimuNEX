namespace SimuNEX
{
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
        public int size { get; }
    }

    public interface IModelOutput
    {
    }

    public interface IModelInput
    {
    }
}
