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
        /// The stepper method.
        /// </summary>
        public StepperMethod stepper;

        protected override void Initialize()
        {
            if (readPosition || readTorque)
            {
                stateSpace = new();
                stateSpace.Initialize(2, 1, new Matrix(2, 1), stepper: StateSpace.CreateStepper(stepper));
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
                        float speed = motor.MotorFunction(motor.inputs, motor.parameters);
                        float acceleration = (speed - stateSpace.states[1, 0]) / Time.deltaTime;

                        stateSpace.inputs[0, 0] = acceleration;
                        stateSpace.Compute();

                        float integratedPosition = stateSpace.states[0, 0] % 2 * MathF.PI;

                        // Compute torque using provided relationship
                        float torque = (motor.totalInertia * acceleration) + (motor.totalDamping * speed);

                        if (readPosition && readTorque)
                        {
                            return new float[] { speed, integratedPosition, torque };
                        }
                        else if (readTorque && !readPosition)
                        {
                            return new float[] { speed, torque };
                        }
                        else if (readPosition && !readTorque)
                        {
                            return new float[] { speed, integratedPosition };
                        }
                    }

                    return (readPosition && readTorque) ? new float[3] : new float[2];
                };
            }
            else
            {
                outputs = () => motor.inputs != null
                    ? (new float[] { motor.MotorFunction(motor.inputs, motor.parameters) })
                    : (new float[1]);
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
                outputNames = new string[]
                {
                    $"{motorName} {loadName} Speed",
                    $"{motorName} {loadName} Position",
                    $"{motorName} {loadName} Torque"
                };
            }
            else if (readTorque && !readPosition)
            {
                outputNames = new string[]
                {
                    $"{motorName} {loadName} Speed",
                    $"{motorName} {loadName} Torque"
                };
            }
            else if (readPosition && !readTorque)
            {
                outputNames = new string[]
                {
                    $"{motorName} {loadName} Speed",
                    $"{motorName} {loadName} Position"
                };
            }
            else
            {
                outputNames = new string[] { $"{motorName} {loadName} Speed" };
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
