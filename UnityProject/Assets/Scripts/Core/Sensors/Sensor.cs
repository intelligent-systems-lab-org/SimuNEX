using SimuNEX.Mechanical;
using System;

namespace SimuNEX
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
    }
}
