namespace SimuNEX
{
    /// <summary>
    /// Defines a port in a <see cref="Model"/> object, holding data.
    /// </summary>
    public abstract class ModelPort : IModelPort
    {
        /// <summary>
        /// Unique name assigned to the port.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Returns data stored in the port.
        /// </summary>
        public float[] data { get; set; }

        /// <summary>
        /// The domain of the data.
        /// </summary>
        public Signal signal { get; set; }

        /// <summary>
        /// Creates a <see cref="ModelPort{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        protected ModelPort(string name, int size = 1, Signal signal = Signal.Virtual)
        {
            this.name = name;
            this.signal = signal;

            data = new float[size];
        }
    }

    /// <summary>
    /// Defines an output port in a <see cref="Model"/> object.
    /// </summary>
    public class ModelOutput : ModelPort, IModelOutput
    {
        /// <summary>
        /// Creates a <see cref="ModelOutput{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        public ModelOutput(string name, int size = 1, Signal signal = Signal.Virtual) : base(name, size, signal)
        {
        }
    }

    /// <summary>
    /// Defines an input port in a <see cref="Model"/> object.
    /// </summary>
    public class ModelInput: ModelPort, IModelInput
    {
        /// <summary>
        /// Creates a <see cref="ModelInput{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        public ModelInput(string name, int size = 1, Signal signal = Signal.Virtual) : base(name, size, signal)
        {
        }
    }
}
