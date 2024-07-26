using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Defines a port in a <see cref="Model"/> object, holding data.
    /// </summary>
    /// <typeparam name="T">The datatype of the data in the port.</typeparam>
    public abstract class ModelPort<T> : IModelPort
    {
        /// <summary>
        /// Unique name assigned to the port.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Returns data stored in the port with the actual datatype.
        /// </summary>
        protected T typedData { get; set; }

        /// <summary>
        /// Returns data stored in the port stored as an object.
        /// </summary>
        /// <exception cref="InvalidCastException">Throws if the value is set to the incorrect datatype.</exception>
        public object data
        {
            get => typedData;

            set
            {
                if (value is T typedValue)
                {
                    typedData = typedValue;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast {value.GetType()} to {typeof(T)}");
                }
            }
        }

        /// <summary>
        /// The domain of the data.
        /// </summary>
        public Signal signal { get; set; }

        /// <summary>
        /// Returns the size of the data depending on the datatype <see cref="T"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Throws if datatype is unsupported.</exception>
        public int size => (typedData as object) switch
        {
            Vector3 => 3,
            Quaternion => 4,
            int or float => 1,
            float[] floats => floats.Length,
            Vector6DOF => 6,
            _ => throw new ArgumentException("Unsupported datatype"),
        };

        /// <summary>
        /// Creates a <see cref="ModelPort{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        protected ModelPort(string name, Signal signal = Signal.Virtual)
        {
            this.name = name;
            this.signal = signal;
        }
    }

    /// <summary>
    /// Defines an output port in a <see cref="Model"/> object.
    /// </summary>
    /// <typeparam name="T">The datatype of the data in the port.</typeparam>
    public class ModelOutput<T> : ModelPort<T>, IModelOutput
    {
        /// <summary>
        /// Creates a <see cref="ModelOutput{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        public ModelOutput(string name, Signal signal = Signal.Virtual) : base(name, signal)
        {
        }
    }

    /// <summary>
    /// Defines an input port in a <see cref="Model"/> object.
    /// </summary>
    /// <typeparam name="T">The datatype of the data in the port.</typeparam>
    public class ModelInput<T> : ModelPort<T>, IModelInput
    {
        /// <summary>
        /// Creates a <see cref="ModelInput{T}"/> with the given name and data domain.
        /// </summary>
        /// <param name="name">The name assigned to the port.</param>
        /// <param name="signal">The domain of the port data. Is <see cref="Signal.Virtual"/> by default.</param>
        public ModelInput(string name, Signal signal = Signal.Virtual) : base(name, signal)
        {
        }
    }
}
