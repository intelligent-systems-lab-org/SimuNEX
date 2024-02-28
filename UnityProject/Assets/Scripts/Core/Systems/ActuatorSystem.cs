using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimuNEX.Mechanical;
using SimuNEX.Actuators;

namespace SimuNEX
{
    /// <summary>
    /// Groups all attached <see cref="Actuator"/> objects.
    /// </summary>
    [DisallowMultipleComponent]
    public class ActuatorSystem : MonoBehaviour
    {
        /// <summary>
        /// Connected <see cref="Actuator"/> objects.
        /// </summary>
        public List<Actuator> actuators;

        /// <summary>
        /// All <see cref="Actuator"/> inputs concatenated in a array.
        /// </summary>
        public float[] inputs;

        /// <summary>
        /// Attached <see cref="RigidBody"/>.
        /// </summary>
        private RigidBody rigidBody;

        /// <summary>
        /// Total number of inputs.
        /// </summary>
        private int NumInputs;

        protected void OnValidate() => UpdateActuatorList();
        protected void OnEnable() => UpdateActuatorList();

        /// <summary>
        /// Obtains the current list of attached <see cref="Actuator"/> objects.
        /// </summary>
        public void UpdateActuatorList()
        {
            rigidBody = GetComponent<RigidBody>();
            actuators = new List<Actuator>(GetComponentsInChildren<Actuator>());

            NumInputs = 0;
            foreach (Actuator actuator in actuators)
            {
                actuator.rigidBody = rigidBody;
                NumInputs += actuator.inputSize;
            }

            inputs = new float[NumInputs];
        }

        /// <summary>
        /// Sets the <see cref="Actuator"/> inputs to the assigned values.
        /// </summary>
        public void SetActuatorInputs()
        {
            int idx = 0;
            foreach (Actuator actuator in actuators)
            {
                float[] slice = inputs.Skip(idx).Take(actuator.inputSize).ToArray();
                actuator.SetInputs(slice);
                idx += actuator.inputSize;
            }
        }

        /// <summary>
        /// Gets the current <see cref="Actuator"/> input values.
        /// </summary>
        public void GetActuatorInputs()
        {
            int idx = 0;
            foreach (Actuator actuator in actuators)
            {
                float[] currentActuatorInputs = actuator.Input;
                Array.Copy(currentActuatorInputs, 0, inputs, idx, currentActuatorInputs.Length);
                idx += currentActuatorInputs.Length;
            }
        }

        /// <summary>
        /// Sets all <see cref="Actuator"/> objects to their default values.
        /// </summary>
        public void ResetAll()
        {
            for (int i = 0; i < NumInputs; i++)
            {
                inputs[i] = 0;
            }
            SetActuatorInputs();
        }

        /// <summary>
        /// Outputs a detailed description of the <see cref="ActuatorSystem"/>.
        /// </summary>
        /// <returns>The outputted info which contains details about the <see cref="ActuatorSystem"/>.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();
            builder.AppendLine($"ActuatorSystem ({actuators.Count} actuators):");

            foreach (Actuator actuator in actuators)
            {
                builder.AppendLine($"   - {actuator.GetType().Name}, InputSize: {actuator.inputSize}");
            }

            return builder.ToString().TrimEnd();
        }

    }
}
