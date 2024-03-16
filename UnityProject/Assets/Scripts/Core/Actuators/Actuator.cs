using SimuNEX.Mechanical;
using System;

namespace SimuNEX.Actuators
{
    /// <summary>
    /// Interface for implementing actuator-based systems.
    /// </summary>
    public abstract class Actuator : FaultEntity, IDynamics
    {
        /// <summary>
        /// <see cref="RigidBody"/> object that the actuator is attached to.
        /// </summary>
        public RigidBody rigidBody;

        /// <summary>
        /// Inputs to the <see cref="Actuator"/>.
        /// </summary>
        public Func<float[]> inputs;

        /// <summary>
        /// Names of input values.
        /// </summary>
        public string[] inputNames;

        /// <summary>
        /// Parameters specific to the <see cref="Actuator"/>.
        /// </summary>
        public Func<float[]> parameters;

        /// <summary>
        /// Number of inputs specific to the <see cref="Actuator"/>.
        /// </summary>
        public int inputSize => (inputs == null) ? 0 : inputs().Length;

        /// <summary>
        /// Sets up properties and defines the actuator's function for simulation.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Gets all inputs specific to the <see cref="Actuator"/>.
        /// </summary>
        /// <returns>The current input values.</returns>
        public float[] Input => inputs();

        /// <summary>
        /// Sets all inputs specific to the <see cref="Actuator"/>.
        /// </summary>
        /// <param name="value">The input values to set.</param>
        public abstract void SetInputs(float[] value);

        /// <summary>
        /// Initializes the <see cref="inputs"/> and <see cref="parameters"/> array.
        /// </summary>
        public void MapVariables()
        {
            this.InitializeVariables<InputAttribute>(out inputs);
            this.InitializeVariables<ParameterAttribute>(out parameters);
        }

        protected void OnValidate()
        {
            MapVariables();
            Initialize();
        }

        protected void Start()
        {
            MapVariables();
            Initialize();
        }

        /// <summary>
        /// Updates the current output values.
        /// </summary>
        public void Step()
        {
            ComputeStep();
            if (faultables != null)
            {
                FaultStep();
            }

            ConstraintsStep();
        }

        /// <summary>
        /// Computes the output values before faults and constraints are applied.
        /// </summary>
        protected abstract void ComputeStep();

        /// <summary>
        /// Applies constraints to the output values.
        /// </summary>
        protected abstract void ConstraintsStep();

        /// <summary>
        /// Work in progress
        /// </summary>
        public void ResetAll()
        {
            throw new NotImplementedException();
        }
    }
}
