using SimuNEX.Mechanical;
using System;

namespace SimuNEX
{
    /// <summary>
    /// Interface for implementing actuator-based systems.
    /// </summary>
    public abstract class Actuator : FaultSystem
    {
        /// <summary>
        /// <see cref="RigidBody"/> object that the actuator is attached to.
        /// </summary>
        public RigidBody rigidBody;

        /// <summary>
        /// Inputs to the actuator.
        /// </summary>
        public Func<float[]> inputs;

        /// <summary>
        /// Names of input values.
        /// </summary>
        public string[] inputNames;

        /// <summary>
        /// Parameters specific to the actuator.
        /// </summary>
        public Func<float[]> parameters;

        /// <summary>
        /// Number of inputs specific to the <see cref="Actuator"/>.
        /// </summary>
        public int inputSize => (inputs == null) ? 0 : inputs().Length;

        /// <summary>
        /// Sets up properties and defines the actuator's function for simulation.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Gets all inputs specific to the <see cref="Actuator"/>.
        /// </summary>
        /// <returns>The current input values.</returns>
        public float[] GetInput()
        {
            return inputs();
        }

        /// <summary>
        /// Sets all inputs specific to the <see cref="Actuator"/>.
        /// </summary>
        /// <param name="value">The input values to set.</param>
        public abstract void SetInput(float[] value);

        /// <summary>
        /// Initializes the <see cref="inputs"/> and <see cref="parameters"/> array.
        /// </summary>
        public void InitializeVariables()
        {
            this.InitializeVariables<InputAttribute>(out inputs);
            this.InitializeVariables<ParameterAttribute>(out parameters);
        }
    }
}
