using SimuNEX.Models;
using SimuNEX.Solvers;
using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Implements an ideal sensor that measures <see cref="Motor"/> object values.
    /// </summary>
    public class IdealMotorSensor : Sensor
    {
        /// <summary>
        /// Motor speed property.
        /// </summary>
        public float motorSpeed => _speed;

        /// <summary>
        /// Motor position property.
        /// </summary>
        public float motorPosition => _position;

        /// <summary>
        /// Motor torque property.
        /// </summary>
        public float motorTorque => _torque;

        /// <summary>
        /// Measured motor speed.
        /// </summary>
        protected float _speed;

        /// <summary>
        /// Measured motor position.
        /// </summary>
        protected float _position;

        /// <summary>
        /// Measured motor torque.
        /// </summary>
        protected float _torque;

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
            if (readPosition || readTorque)
            {
                stateSpace = new();
                stateSpace.Initialize(2, 1, new Matrix(2, 1), solver: solver);
                stateSpace.DerivativeFcn = (states, inputs) =>
                {
                    // states[0] is position, states[1] is speed
                    // Derivative of position is speed, input is acceleration
                    return new Matrix(2, 1, new float[] { states[1, 0], inputs[0, 0] });
                };

                outputs = () =>
                {
                    if (motor.inputs != null)
                    {
                        if (motor.motorLoad != null)
                        {
                            _speed = motor.motorLoad._speed;
                        }

                        float acceleration = (_speed - stateSpace.states[1, 0]) / Time.deltaTime;

                        stateSpace.inputs[0, 0] = acceleration;
                        stateSpace.Compute();

                        float _position = stateSpace.states[0, 0] % 2 * MathF.PI;

                        // Compute torque using provided relationship
                        _torque = (motor.totalInertia * acceleration) + (motor.totalDamping * _speed);

                        ApplyFaults("motorSpeed", ref _speed);

                        if (readPosition && readTorque)
                        {
                            ApplyFaults("motorPosition", ref _position);
                            ApplyFaults("motorTorque", ref _torque);
                            return new float[] { motorSpeed, motorPosition, motorTorque };
                        }
                        else if (readTorque && !readPosition)
                        {
                            ApplyFaults("motorTorque", ref _torque);
                            return new float[] { motorSpeed, motorTorque };
                        }
                        else if (readPosition && !readTorque)
                        {
                            ApplyFaults("motorPosition", ref _position);
                            return new float[] { motorSpeed, motorPosition };
                        }
                    }

                    return (readPosition && readTorque) ? new float[3] : new float[2];
                };
            }
            else
            {
                outputs = () =>
                {
                    if (motor.inputs == null)
                    {
                        return new float[1];
                    }

                    if (motor.motorLoad != null)
                    {
                        _speed = motor.motorLoad._speed;
                    }

                    ApplyFaults("motorSpeed", ref _speed);

                    return new float[] { motorSpeed };
                };
            }

            if (motor != null)
            {
                if (rigidBody == null)
                {
                    rigidBody = motor.rigidBody;
                }

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
            else
            {
                outputNames = readPosition && !readTorque
                    ? (new string[]
                    {
                        $"{motorName} {loadName} Speed",
                        $"{motorName} {loadName} Position"
                    })
                    : (new string[] { $"{motorName} {loadName} Speed" });
            }
        }

        protected void OnValidate()
        {
            Initialize();
        }

        protected void Awake()
        {
            Initialize();
        }
    }
}
