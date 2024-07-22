using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    public interface IModel
    {
        IEnumerable<IModelOutput> outports { get; }
        IEnumerable<IModelInput> inports { get; }
        IBehavioral behavorial { get; }
    }

    public abstract class Model : MonoBehaviour, IModel
    {
        /// <summary>
        /// Inputs to the <see cref="Model"/>.
        /// </summary>
        protected List<IModelInput> _inports = new();

        /// <summary>
        /// Outputs to the <see cref="Model"/>.
        /// </summary>
        protected List<IModelOutput> _outports = new();

        /// <summary>
        /// Returns all output ports.
        /// </summary>
        public IEnumerable<IModelOutput> outports => _outports.AsReadOnly();

        /// <summary>
        /// Returns all input ports.
        /// </summary>
        public IEnumerable<IModelInput> inports => _inports.AsReadOnly();

        protected void PortMap()
        {
            _outports.AddRange(behavorial.states);
            _inports.AddRange(behavorial.inputs);
        }

        public abstract IBehavioral behavorial { get; }
    }
}
