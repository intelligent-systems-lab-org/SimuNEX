using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Defines a port, holding data.
    /// </summary>
    public abstract class Port : IPort
    {
        /// <summary>
        /// The name of the port.
        /// </summary>
        [SerializeField]
        private string _name;

        /// <summary>
        /// The data in the port.
        /// </summary>
        [SerializeField]
        private float[] _data;

        /// <summary>
        /// The domain of the data.
        /// </summary>
        [SerializeField]
        private Signal _signal;

        /// <summary>
        /// (Property) Unique name assigned to the port.
        /// </summary>
        public string name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// (Property) The data stored in the port.
        /// </summary>
        public float[] data
        {
            get => _data;
            set => _data = value;
        }

        /// <summary>
        /// (Property) The domain of the data.
        /// </summary>
        public Signal signal
        {
            get => _signal;
            set => _signal = value;
        }

        /// <summary>
        /// (Property) The size of the data.
        /// </summary>
        public int size => _data.Length;

        /// <summary>
        /// Creates a <see cref="ModelPort{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        protected Port(string name, int size = 1, Signal signal = Signal.Virtual)
        {
            this.name = name;
            this.signal = signal;

            data = new float[size];
        }
    }

    /// <summary>
    /// Defines a port in a <see cref="Model"/> object, holding data.
    /// </summary>
    public abstract class ModelPort : Port
    {
        /// <summary>
        /// The model this port is associated with.
        /// </summary>
        public Model connectedModel;

        /// <summary>
        /// Creates a <see cref="ModelPort{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        /// <param name="model">The model this port will be associated with.</param>
        protected ModelPort(string name, int size = 1, Signal signal = Signal.Virtual, Model model = null)
            : base(name, size, signal)
        {
            connectedModel = model;
        }
    }

    /// <summary>
    /// Defines an output port in a <see cref="Model"/> object.
    /// </summary>
    [Serializable]
    public class ModelOutput : ModelPort
    {
        /// <summary>
        /// Creates a <see cref="ModelOutput"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        /// <param name="model">The model this port will be associated with.</param>
        public ModelOutput(string name, int size = 1, Signal signal = Signal.Virtual, Model model = null)
            : base(name, size, signal, model)
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
        /// Creates a <see cref="ModelInput"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="size">The data size of the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        /// <param name="model">The model this port will be associated with.</param>
        public ModelInput(string name, int size = 1, Signal signal = Signal.Virtual, Model model = null)
            : base(name, size, signal, model)
        {
        }
    }
}
