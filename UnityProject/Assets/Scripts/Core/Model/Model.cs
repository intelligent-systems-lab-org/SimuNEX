using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    public interface IModel
    {
        IModelPort[] ports { get; }
        IBehavioral behavorial { get; }
    }

    public abstract class Model : MonoBehaviour, IModel
    {
        /// <summary>
        /// Ports associated with the model.
        /// </summary>
        protected List<IModelPort> _ports = new();

        private bool _portsInitialized;

        /// <summary>
        /// Returns all output ports.
        /// </summary>
        public IEnumerable<IModelOutput> outports
        {
            get
            {
                foreach (IModelPort port in _ports)
                {
                    if (port is IModelOutput outport)
                    {
                        yield return outport;
                    }
                }
            }
        }

        /// <summary>
        /// Returns all input ports.
        /// </summary>
        public IEnumerable<IModelInput> inports
        {
            get
            {
                foreach (IModelPort port in _ports)
                {
                    if (port is IModelInput inport)
                    {
                        yield return inport;
                    }
                }
            }
        }

        /// <summary>
        /// Defines the ports the <see cref="Model"/> has.
        /// </summary>
        public abstract IModelPort[] ports { get; }

        public abstract IBehavioral behavorial { get; }

        public void Init()
        {
            InitPorts();
        }

        /// <summary>
        /// Initializes the ports for the model.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if no ports are specified.</exception>
        public void InitPorts()
        {
            if (!_portsInitialized)
            {
                if (ports == null || ports.Length == 0)
                {
                    throw new InvalidOperationException("Derived classes must specify at least one port.");
                }

                _ports.AddRange(ports);
                _portsInitialized = true;
            }
        }

        protected void Awake()
        {
            Init();
        }
    }
}
