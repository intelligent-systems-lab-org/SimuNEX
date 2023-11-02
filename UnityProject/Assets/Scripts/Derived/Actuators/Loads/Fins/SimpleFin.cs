using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Simple fin model with constant force and torque coefficients.
    /// </summary>
    public class SimpleFin : Fin
    {
        [Parameter]
        /// <summary>
        /// Fin angle to force factor.
        /// </summary>
        public float forceCoefficient = 1.5f;

        [Parameter]
        /// <summary>
        /// Fin angle to torque factor.
        /// </summary>
        public float torqueCoefficient = 1f;

        protected override void Initialize()
        {
            force = rigidBody.gameObject.AddComponent<SimpleFinForce>();
            (force as SimpleFinForce).Initialize(this);
        }
    }

    /// <summary>
    /// Implements the FinFunction for <see cref="SimpleFin"/>
    /// </summary>
    public class SimpleFinForce : FinForce
    {
        /// <summary>
        /// Set up fin specific parameters.
        /// </summary>
        /// <param name="fin"><see cref="SimpleFin"/> object that the force is being applied to.</param>
        public void Initialize(SimpleFin fin)
        {
            base.Initialize(fin);
            parameters = () => new float[]
            {
                fin.forceCoefficient, fin.torqueCoefficient
            };
        }

        public override float[] FinFunction(Func<float> finAngle, Func<float[]> parameters)
        {
            float _finAngle = finAngle();
            float bodySpeedSquared = Mathf.Pow(thrustSpeed, 2);

            float force = parameters()[0] * _finAngle * bodySpeedSquared;
            float torque = parameters()[1] * _finAngle * bodySpeedSquared;
            return new float[] { force, torque };
        }
    }
}
