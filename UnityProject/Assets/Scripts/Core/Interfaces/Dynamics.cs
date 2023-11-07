using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Interface for describing dynamic systems.
    /// </summary>
    [RequireComponent(typeof(DynamicSystem))]
    public abstract class Dynamics : MonoBehaviour
    {
        /// <summary>
        /// Configures the system at the start of the physics simulation.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Applys the accumulated inputs to the system.
        /// </summary>
        public abstract void Step();
    }

    /// <summary>
    /// Adds faults to a supported system.
    /// </summary>
    public class FaultSystem : MonoBehaviour
    {
        /// <summary>
        /// List of faults present in the system.
        /// </summary>
        public List<FaultEntry> faults = new();

        /// <summary>
        /// Adds to the list of faults affecting the selected property.
        /// </summary>
        /// <param name="property">Property value that is being faulted.</param>
        /// <param name="fault"><see cref="Fault"/> object that applies the fault.</param>
        public void AddFault(string property, Fault fault)
        {
            FaultEntry entry = faults.Find(fe => fe.property == property);
            if (entry == null)
            {
                entry = new FaultEntry { property = property, Faults = new List<Fault>() };
                faults.Add(entry);
            }

            entry.Faults.Add(fault);
        }

        /// <summary>
        /// Finds the list of faults affecting property.
        /// </summary>
        /// <param name="property">The property of interest.</param>
        /// <returns>The lists of faults affecting property.</returns>
        public List<Fault> GetFaults(string property)
        {
            FaultEntry entry = faults.Find(fe => fe.property == property);
            return entry?.Faults;
        }

        /// <summary>
        /// Applies all faults at once to the property value in place.
        /// </summary>
        /// <typeparam name="T">The <see cref="Faultable"/> property type.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="property">The property value to apply faults.</param>
        /// <exception cref="InvalidOperationException">Thrown for an unsupported tpye.</exception>
        public void ApplyFault<T>(string propertyName, ref T property)
        {
            List<Fault> faults = GetFaults(propertyName);

            if (faults != null)
            {
                foreach (Fault fault in faults)
                {
                    property = property switch
                    {
                        float f => (T)(object)fault.FaultFunction(f),
                        Vector3 v => (T)(object)fault.FaultFunction(v),
                        Quaternion q => (T)(object)fault.FaultFunction(q),
                        _ => throw new InvalidOperationException("Unsupported type"),
                    };
                }
            }
        }
    }
}
