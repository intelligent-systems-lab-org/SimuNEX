using UnityEngine;

namespace SimuNEX.Dynamics
{
    public abstract class DynamicModel : Model
    {
        protected StateSpace stateSpace = new();

        [SerializeReference]
        public ODESolver solver = new ForwardEuler();

        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                // Update inputs to the state space
                inputFunction(inputs, stateSpace);

                // integrate next step
                solver.Step(stateSpace);

                // update outputs given the state update
                outputFunction(inputs, outputs, stateSpace);
            };

        /// <summary>
        /// Defines the relationship of the <see cref="DynamicModel"/> with respect to its inputs and outputs.
        /// </summary>
        protected abstract OutputFunction outputFunction { get; }

        /// <summary>
        /// Defines the relationship of the <see cref="DynamicModel"/> with respect to its inputs and states.
        /// </summary>
        protected abstract InputFunction inputFunction { get; }

        /// <summary>
        /// Models the relationship between the model's inputs and outputs.
        /// </summary>
        /// <param name="inputs">The inputs (read-only) to the function.</param>
        /// <param name="outputs">The outputs (read and write) to the function.</param>
        /// <param name="states">The states (read and write) to the states.</param>
        public delegate void OutputFunction(ModelInput[] inputs, ModelOutput[] outputs, StateSpace states);

        /// <summary>
        /// Models the relationship between the model's inputs and states.
        /// </summary>
        /// <param name="inputs">The inputs (read-only) to the function.</param>
        /// <param name="states">The states (read and write) to the states.</param>
        public delegate void InputFunction(ModelInput[] inputs, StateSpace states);
    }
}
