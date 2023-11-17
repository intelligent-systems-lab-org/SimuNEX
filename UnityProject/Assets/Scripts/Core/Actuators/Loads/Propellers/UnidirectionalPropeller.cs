using System;
using UnityEngine;

namespace SimuNEX.Loads
{
    /// <summary>
    /// Simple propeller model with constant thrust and torque coefficients with unidirectional thrust.
    /// </summary>
    public class UnidirectionalPropeller : Propeller
    {
        [Parameter]
        /// <summary>
        /// Speed to thrust factor.
        /// </summary>
        public float thrustCoefficient = 1.88865e-5f;

        [Parameter]
        /// <summary>
        /// Speed to torque factor.
        /// </summary>
        public float torqueCoefficient = 1.1e-5f;

        protected override void Initialize()
        {
            _force = rigidBody.gameObject.AddComponent<UnidirectionalPropellerForce>();
            (_force as UnidirectionalPropellerForce).Initialize(this);
        }

        /// <summary>
        /// Implements the PropellerFunction for <see cref="UnidirectionalPropeller"/>
        /// </summary>
        public class UnidirectionalPropellerForce : PropellerForce
        {
            /// <summary>
            /// Set up propeller specific parameters.
            /// </summary>
            /// <param name="propeller"><see cref="UnidirectionalPropellerForce"/> object that the _force is being applied to.</param>
            public void Initialize(UnidirectionalPropeller propeller)
            {
                base.Initialize(propeller);
                parameters = () => new float[]
                {
                    propeller.thrustCoefficient, propeller.torqueCoefficient
                };
            }

            public override float[] PropellerFunction(Func<float> speed, Func<float[]> parameters)
            {
                float _speed = speed();
                float thrust = parameters()[0] * _speed * _speed;
                float torque = parameters()[1] * _speed * Mathf.Abs(_speed);
                return new float[] { thrust, torque };
            }
        }
    }
}
