using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Faults
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

    /// <summary>
    /// Stores a named property that has a list of associated <see cref="Fault"/> objects.
    /// </summary>
    [Serializable]
    public class FaultEntry
    {
        public string property;

        [SerializeReference]
        public List<Fault> Faults;
    }
}
