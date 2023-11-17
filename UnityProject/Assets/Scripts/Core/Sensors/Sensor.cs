using SimuNEX.Mechanical;
using System;

namespace SimuNEX.Sensors
{
    /// <summary>
    /// Interface for implementing sensor-based systems.
    /// </summary>
    public abstract class Sensor : FaultSystem
    {
        /// <summary>
        /// <see cref="RigidBody"/> object that the sensor is attached to.
        /// </summary>
        public RigidBody rigidBody;

        /// <summary>
        /// Inputs to the sensor.
        /// </summary>
        protected Func<float[]> outputs;

        /// <summary>
        /// Names of output values.
        /// </summary>
        public string[] outputNames;

        /// <summary>
        /// Parameters specific to the sensor.
        /// </summary>
        public Func<float[]> parameters;

        /// <summary>
        /// Number of outputs specific to the <see cref="Sensor"/>.
        /// </summary>
        public int outputSize => (outputs == null) ? 0 : outputs().Length;

        /// <summary>
        /// Sets up properties and defines the sensor's function for simulation.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Gets all outputs specific to the <see cref="Sensor"/>.
        /// </summary>
        /// <returns>The current sensor readings.</returns>
        public float[] GetOutput()
        {
            return outputs();
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
        }

        /// <summary>
        /// Computes the output values before faults are applied.
        /// </summary>
        protected abstract void ComputeStep();

        /// <summary>
        /// Initializes the <see cref="outputs"/> and <see cref="parameters"/> array.
        /// </summary>
        public void InitializeVariables()
        {
            this.InitializeVariables<OutputAttribute>(out outputs, Step);
            this.InitializeVariables<ParameterAttribute>(out parameters);
        }
    }
}
