using System;
using UnityEngine;
using SimuNEX.Mechanical;

namespace SimuNEX
{
    /// <summary>
    /// Interface for implementing fin-like/control surface systems.
    /// </summary>
    public abstract class Fin : MotorLoad
    {
        /// <summary>
        /// Direction that the fluid acts across the moving body.
        /// </summary>
        public Direction flowDirection;

        /// <summary>
        /// Specialized forces for fins.
        /// </summary>
        public abstract class FinForce : Force
        {
            /// <summary>
            /// Returns the current direction that fluid flows across the moving body.
            /// </summary>
            protected Func<Direction> flowDirection;

            /// <summary>
            /// Fin angle.
            /// </summary>
            protected Func<float> finAngle;

            /// <summary>
            /// Parameters specific to the fin.
            /// </summary>
            protected Func<float[]> parameters;

            /// <summary>
            /// Axis of fin rotation.
            /// </summary>
            protected Func<Vector3> normal;

            /// <summary>
            /// Fin thrust and torques stored in an array.
            /// </summary>
            protected float[] outputs = new float[2];

            /// <summary>
            /// Output speed of the motor attached to the fin.
            /// </summary>
            protected Func<float> motorOutput;

            /// <summary>
            /// The fin function (FF) that computes output values based on the provided inputs (e.g. fin angle and other parameters).
            /// </summary>
            /// <param name="finAngle">Current fin angle.</param>
            /// <param name="parameters">Parameters specific to the fin.</param>
            /// <returns>An array of float values where the first element is _force and the second is torque.</returns>
            public abstract float[] FinFunction(Func<float> finAngle, Func<float[]> parameters);

            public override void ApplyForce()
            {
                float _ = motorOutput();
                Vector3 _normal = normal();
                outputs = FinFunction(finAngle, parameters);

                // Determine the _force direction based on the flow direction, transformed to the body frame
                Vector3 forceDirection = rigidBody.transform.TransformDirection(flowDirection().ToVector());

                // Apply the _force perpendicular to the fin normal
                Vector3 appliedForce = Vector3.Cross(forceDirection, _normal).normalized * outputs[0];

                rigidBody.AddLinearForce(appliedForce);
                rigidBody.AddTorque(_normal * outputs[1]);
            }

            /// <summary>
            /// Connects fin object to its associated transforms and <see cref="RigidBody"/>.
            /// </summary>
            /// <param name="fin"><see cref="Fin"/> object that the _force is being applied to.</param>
            public void Initialize(Fin fin)
            {
                normal = () => fin.normal;
                flowDirection = () => fin.flowDirection;
                finAngle = () => fin.normalizedAngle;
                motorOutput = () => fin.motorOutput;
            }

            public float thrustSpeed => flowDirection() switch
            {
                Direction.Left or Direction.Right => rigidBody.velocity.u,
                Direction.Up or Direction.Down => rigidBody.velocity.v,
                Direction.Forward or Direction.Backward => rigidBody.velocity.w,
                _ => 0,
            };
        }
    }
}
