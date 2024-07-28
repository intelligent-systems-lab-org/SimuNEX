using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Defines a port in a <see cref="Model"/> object, holding data.
    /// </summary>
    public abstract class ModelPort : IModelPort
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private float[] _data;

        [SerializeField]
        private Signal _signal;

        /// <summary>
        /// Unique name assigned to the port.
        /// </summary>
        public string name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Returns data stored in the port.
        /// </summary>
        public float[] data
        {
            get => _data;
            set => _data = value;
        }

        /// <summary>
        /// The domain of the data.
        /// </summary>
        public Signal signal
        {
            get => _signal;
            set => _signal = value;
        }

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
    [Serializable]
    public class ModelOutput : ModelPort
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
    [Serializable]
    public class ModelInput : ModelPort
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
