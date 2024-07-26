using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    public abstract class Model : MonoBehaviour, IModel
    {
        /// <summary>
        /// Returns all output ports.
        /// </summary>
        public IModelOutput[] outports => outputs.ToArray();

        /// <summary>
        /// Returns all input ports.
        /// </summary>
        public IModelInput[] inports => inputs.ToArray();

        /// <summary>
        /// The outputs of the <see cref="Model"/>.
        /// </summary>
        protected List<IModelOutput> outputs = new();

        /// <summary>
        /// The inputs to the <see cref="Model"/>.
        /// </summary>
        protected List<IModelInput> inputs = new();

        public delegate void ModelFunction(IModelInput[] inputs, IModelOutput[] outputs);

        protected abstract ModelFunction modelFunction { get; }
    }
}
