using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    [Serializable]
    [HelpURL(URLConfig.baseURL + "/learn/core/model")]
    public abstract class Model : MonoBehaviour
    {
        /// <summary>
        /// Returns all output ports.
        /// </summary>
        public ModelOutput[] outports => outputs.ToArray();

        /// <summary>
        /// Returns all input ports.
        /// </summary>
        public ModelInput[] inports => inputs.ToArray();

        /// <summary>
        /// The outputs of the <see cref="Model"/>.
        /// </summary>
        [SerializeField]
        protected List<ModelOutput> outputs = new();

        /// <summary>
        /// The inputs to the <see cref="Model"/>.
        /// </summary>
        [SerializeField]
        protected List<ModelInput> inputs = new();

        /// <summary>
        /// The sample rate.
        /// </summary>
        [Range(0.001f, 1f)]
        public float sampleTime = 0.02f;

        /// <summary>
        /// The sample rate at runtime.
        /// </summary>
        protected float _sampleTime;

        /// <summary>
        /// Time elapsed since the last simulation tick.
        /// </summary>
        protected float timeSinceLastTick;

        /// <summary>
        /// Models the relationship between the model's inputs and outputs.
        /// </summary>
        /// <param name="inputs">The inputs (read-only) to the function.</param>
        /// <param name="outputs">The outputs (read and write) to the function.</param>
        public delegate void ModelFunction(ModelInput[] inputs, ModelOutput[] outputs);

        /// <summary>
        /// Defines the relationship of the <see cref="Model"/> with respect to its inputs and outputs.
        /// </summary>
        protected abstract ModelFunction modelFunction { get; }

        /// <summary>
        /// Function that updates the model.
        /// </summary>
        /// <param name="tickRate">The simulation tick rate set by <see cref="Runner"/>.</param>
        public virtual void Step(float tickRate = Mathf.Infinity)
        {
            timeSinceLastTick += tickRate;

            if (timeSinceLastTick >= sampleTime)
            {
                modelFunction(inports, outports);

                // Subtract from the accumulated time instead of a full reset
                // to account for any potential drift
                timeSinceLastTick -= sampleTime;

                if (this is IModelPostStepCB cb)
                {
                    cb.PostStepFcn();
                }
            }
        }

        /// <summary>
        /// Initializes model properties for simulation.
        /// </summary>
        public void Setup()
        {
            _sampleTime = sampleTime;

            foreach (ModelInput input in inports)
            {
                input.connectedModel = this;
            }

            foreach (ModelOutput output in outports)
            {
                output.connectedModel = this;
            }
        }
    }
}
