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

        public T data { get; set; }

        /// <summary>
        /// Returns the size of the data depending on the datatype <see cref="T"/>.
        /// </summary>
        public int size => (data as object) switch
        {
            Vector3 => 3,
            Quaternion => 4,
            int or float => 1,
            float[] floats => floats.Length,
            _ => 0,
        };
    }

    public class ModelOutput<T> : ModelPort<T>, IModelOutput
    {
        public ModelOutput(string name)
        {
            this.name = name;
        }
    }

    public class ModelInput<T> : ModelPort<T>, IModelInput
    {
        public ModelInput(string name)
        {
            this.name = name;
        }
    }
}
