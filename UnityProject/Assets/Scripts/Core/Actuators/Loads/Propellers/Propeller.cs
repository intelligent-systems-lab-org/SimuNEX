using SimuNEX.Mechanical;
using System;
using UnityEngine;

namespace SimuNEX.Loads
{
    /// <summary>
    /// Interface for implementing propeller systems.
    /// </summary>
    public abstract class Propeller : MotorLoad
    {
        /// <summary>
        /// Specialized forces for propellers.
        /// </summary>
        public abstract class PropellerForce : Force
        {
            /// <summary>
            /// Propeller rotational speed.
            /// </summary>
            protected Func<float> propellerSpeed;

            /// <summary>
            /// Parameters specific to the propeller.
            /// </summary>
            protected Func<float[]> parameters;

            /// <summary>
            /// Axis of propeller rotation.
            /// </summary>
            protected Func<Vector3> normal;

            /// <summary>
            /// Position of the propeller.
            /// </summary>
            protected Func<Vector3> positionCallback;

            /// <summary>
            /// Propeller thrust and torques stored in an array.
            /// </summary>
            protected float[] outputs = new float[2];

            /// <summary>
            /// The propeller function (PF) that computes output values based on the provided inputs
            /// (e.g. propeller speed and other parameters).
            /// </summary>
            /// <param name="speed">Current propeller speed.</param>
            /// <param name="parameters">Parameters specific to the propeller.</param>
            /// <returns>An array of float values where the first element is _force and the second is torque.</returns>
            public abstract float[] PropellerFunction(Func<float> speed, Func<float[]> parameters);

            public override void ApplyForce()
            {
                Vector3 _normal = normal();
                outputs = PropellerFunction(propellerSpeed, parameters);
                rigidBody.AddLinearForceAtPosition(_normal * outputs[0], positionCallback());
                rigidBody.AddTorque(_normal * outputs[1]);
            }

            /// <summary>
            /// Connects propeller object to its associated transforms and <see cref="RigidBody"/>.
            /// </summary>
            /// <param name="propeller"><see cref="Propeller"/> object that the _force is being applied to.</param>
            public void Initialize(Propeller propeller)
            {
                normal = () => propeller.normal;
                positionCallback = () => propeller.transform.position;
                propellerSpeed = () => propeller.motorOutput;
            }
        }
    }
}
