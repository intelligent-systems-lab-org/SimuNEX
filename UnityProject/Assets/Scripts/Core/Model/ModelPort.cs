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

        protected T typedData { get; set; }

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

        public Signal signal { get; set; }

        /// <summary>
        /// Returns the size of the data depending on the datatype <see cref="T"/>.
        /// </summary>
        public int size => (typedData as object) switch
        {
            Vector3 => 3,
            Quaternion => 4,
            int or float => 1,
            float[] floats => floats.Length,
            _ => 0,
        };

        protected ModelPort(string name, Signal signal = Signal.Virtual)
        {
            this.name = name;
            this.signal = signal;
        }
    }

    public class ModelOutput<T> : ModelPort<T>, IModelOutput
    {
        public ModelOutput(string name, Signal signal = Signal.Virtual) : base(name, signal)
        {
        }
    }

    public class ModelInput<T> : ModelPort<T>, IModelInput
    {
        public ModelInput(string name, Signal signal = Signal.Virtual) : base(name, signal)
        {
        }
    }
}
