using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Interface for implementing faults.
    /// </summary>
    [Serializable]
    public abstract class Fault
    {
        /// <summary>
        /// Applies the fault to the given value.
        /// </summary>
        /// <param name="val">The value to apply the fault to.</param>
        /// <returns>The modified value with the fault applied.</returns>
        public abstract float FaultFunction(float val);

        /// <summary>
        /// Applies the fault to a Vector3.
        /// </summary>
        /// <param name="vector">The Vector3 to apply the fault to.</param>
        /// <returns>The modified Vector3 with the fault applied.</returns>
        public Vector3 FaultFunction(Vector3 vector)
        {
            return new Vector3(FaultFunction(vector.x), FaultFunction(vector.y), FaultFunction(vector.z));
        }

        /// <summary>
        /// Applies the fault to a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion to apply the fault to.</param>
        /// <returns>The modified Quaternion with the fault applied.</returns>
        public Quaternion FaultFunction(Quaternion quaternion)
        {
            Vector3 euler = quaternion.eulerAngles;
            Vector3 faultedEuler = FaultFunction(euler);
            return Quaternion.Euler(faultedEuler);
        }
    }

    [Serializable]
    public class FaultEntry
    {
        public string property;

        [SerializeReference]
        public List<Fault> Faults;
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
