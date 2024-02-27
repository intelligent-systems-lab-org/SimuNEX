using SimuNEX.Mechanical;
using SimuNEX.Sensors;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Groups all attached <see cref="Sensor"/> objects.
    /// </summary>
    [DisallowMultipleComponent]
    public class SensorSystem : MonoBehaviour
    {
        /// <summary>
        /// Connected <see cref="Sensor"/> objects.
        /// </summary>
        public List<Sensor> sensors;

        /// <summary>
        /// All <see cref="Sensor"/> readings concatenated in a array.
        /// </summary>
        public float[] outputs;

        /// <summary>
        /// Attached <see cref="RigidBody"/>.
        /// </summary>
        private RigidBody rigidBody;

        /// <summary>
        /// Total number of outputs.
        /// </summary>
        private int NumOutputs;

        protected void OnValidate() => UpdateSensorList();
        protected void Awake() => UpdateSensorList();

        /// <summary>
        /// Obtains the current list of attached <see cref="Sensor"/> objects.
        /// </summary>
        public void UpdateSensorList()
        {
            rigidBody = GetComponent<RigidBody>();
            sensors = new List<Sensor>(GetComponentsInChildren<Sensor>());

            NumOutputs = 0;
            foreach (Sensor sensor in sensors)
            {
                sensor.rigidBody = rigidBody;
                NumOutputs += sensor.outputSize;
            }

            outputs = new float[NumOutputs];
        }

        /// <summary>
        /// Gets the current <see cref="Sensor"/> values.
        /// </summary>
        public void GetSensorOutputs()
        {
            int idx = 0;
            foreach (Sensor sensor in sensors)
            {
                float[] currentSensorOutputs = sensor.GetOutput();
                Array.Copy(currentSensorOutputs, 0, outputs, idx, currentSensorOutputs.Length);
                idx += currentSensorOutputs.Length;
            }
        }

        /// <summary>
        /// Outputs a detailed description of the SensorSystem object.
        /// </summary>
        /// <returns>The outputted info which contains details about the SensorSystem.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();
            builder.AppendLine($"SensorSystem ({sensors.Count} sensors):");

            foreach (Sensor sensor in sensors)
            {
                builder.AppendLine($"   - {sensor.GetType().Name}, OutputSize: {sensor.outputSize}");
            }

            return builder.ToString().TrimEnd();
        }
    }
}
