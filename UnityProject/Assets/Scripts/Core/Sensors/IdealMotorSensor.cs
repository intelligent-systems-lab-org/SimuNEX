using SimuNEX.Actuators;
using SimuNEX.Models;
using SimuNEX.Solvers;
using System;
using UnityEngine;

namespace SimuNEX.Sensors
{
    /// <summary>
    /// Implements an ideal sensor that measures <see cref="Motor"/> object values.
    /// </summary>
    public class IdealMotorSensor : Sensor
    {
        /// <summary>
        /// Measured motor speed.
        /// </summary>
        [Output]
        [Faultable]
        [SerializeField]
        protected float motorSpeed;

        /// <summary>
        /// Measured motor position.
        /// </summary>
        [Output(omitFieldName: "readPosition")]
        [SerializeField]
        protected float motorPosition;

        /// <summary>
        /// Measured motor torque.
        /// </summary>
        [Output(omitFieldName: "readTorque")]
        [SerializeField]
        protected float motorTorque;

        /// <summary>
        /// Enable position as a sensor reading.
        /// </summary>
        public bool readPosition;

        /// <summary>
        /// Enables torque as a sensor reading.
        /// </summary>
        public bool readTorque;

        /// <summary>
        /// <see cref="Motor"/> object to read speed values.
        /// </summary>
        public Motor motor;

        /// <summary>
        /// The state space for obtaining additional data such as position.
        /// </summary>
        private StateSpace stateSpace;

        /// <summary>
        /// The solver method.
        /// </summary>
        [SerializeReference]
        public ODESolver solver;

        protected override void Initialize()
        {
            if (motor != null)
            {
                if (rigidBody == null)
                {
                    rigidBody = motor.rigidBody;
                }

                stateSpace = new();
                stateSpace.Initialize(2, 1, new Matrix(2, 1), solver: solver);
                stateSpace.DerivativeFcn = (states, inputs) =>
                {
                    // states[0] is position, states[1] is speed
                    // Derivative of position is speed, input is acceleration
                    return new Matrix(2, 1, new float[] { states[1, 0], inputs[0, 0] });
                };
                SetOutputNames();
            }
        }

        /// <summary>
        /// Dynamically sets the output names given the chosen options.
        /// </summary>
        private void SetOutputNames()
        {
            string motorName = motor.gameObject.name;
            string loadName = motor.motorLoad.spinnerObject.gameObject.name;

            if (readPosition && readTorque)
            {
                outputNames = (new string[]
                {
                    $"{motorName} {loadName} Speed",
                    $"{motorName} {loadName} Position",
                    $"{motorName} {loadName} Torque"
                });
            }
            else if (readTorque && !readPosition)
            {
                outputNames = (new string[]
                {
                    $"{motorName} {loadName} Speed",
                    $"{motorName} {loadName} Torque"
                });
            }
            else if (readPosition && !readTorque)
            {
                outputNames = (new string[]
                {
                    $"{motorName} {loadName} Speed",
                    $"{motorName} {loadName} Position"
                });
            }
            else
            {
                outputNames = (new string[] { $"{motorName} {loadName} Speed" });
            }
        }

        protected void OnEnable()
        {
            InitializeVariables();
            Initialize();
        }

        protected override void ComputeStep()
        {
            if (motor.inputs != null)
            {
                if (readPosition || readTorque)
                {
                    if (motor.inputs != null)
                    {
                        float acceleration = (motorSpeed - stateSpace.states[1, 0]) / Time.deltaTime;

                        stateSpace.inputs[0, 0] = acceleration;
                        stateSpace.Compute();

                        motorPosition = motor.motorLoad != null ?
                            motor.motorLoad.normalizedAngle
                            : stateSpace.states[0, 0] % 2 * MathF.PI;

                        // Compute torque using provided relationship
                        motorTorque = (motor.totalInertia * acceleration) + (motor.totalDamping * motorSpeed);
                    }
                }

                motorSpeed = motor.motorLoad != null ? motor.motorLoad._speed : 0;
            }
        }
    }
}
