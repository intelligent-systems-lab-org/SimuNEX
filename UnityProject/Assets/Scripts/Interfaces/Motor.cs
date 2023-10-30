using System;
using static SimuNEX.StateSpaceTypes;
using UnityEngine;

namespace SimuNEX 
{
    /// <summary>
    /// Interface for implementing motors.
    /// </summary>
    public abstract class Motor : Actuator
    {
        /// <summary>
        /// <see cref="MotorLoad"/> object that is attached to the motor.
        /// </summary>
        public MotorLoad motorLoad;

        /// <summary>
        /// Maximum output value.
        /// </summary>
        public float maxSpeed = Mathf.Infinity;

        /// <summary>
        /// Minimum output value.
        /// </summary>
        public float minSpeed = Mathf.NegativeInfinity;

        /// <summary>
        /// Maximum angular position in radians.
        /// </summary>
        public float maxPosition = Mathf.Infinity;

        /// <summary>
        /// Minimum angular position in radians.
        /// </summary>
        public float minPosition = Mathf.NegativeInfinity;

        /// <summary>
        /// The motor inertia in kg.m^2.
        /// </summary>
        public float armatureInertia = 0.5f;

        /// <summary>
        /// The motor damping coefficient in N.m.s/rad.
        /// </summary>
        public float armatureDamping = 0;

        /// <summary>
        /// The stepper method for position prediction.
        /// </summary>
        public StepperMethod positionStepper;

        /// <summary>
        /// Speed integrator for predicting position.
        /// </summary>
        private Integrator integrator;

        /// <summary>
        /// The motor function (MF) that computes output values based on the provided inputs and parameters.
        /// </summary>
        /// <param name="inputs">Input values to the motors (e.g., voltage).</param>
        /// <param name="parameters">Parameters specific to the motor (e.g., back EMF constant).</param>
        /// <returns>The output angular velocity.</returns>
        public abstract float MotorFunction(Func<float[]> inputs, Func<float[]> parameters);

        private void OnValidate()
        {
            if (TryGetComponent(out motorLoad))
            {
                motorLoad.rigidBody = rigidBody;
            }
            integrator = new(() => 1, stepperMethod: positionStepper);
            Initialize();
        }

        private void Awake()
        {
            if (TryGetComponent(out motorLoad))
            {
                motorLoad.rigidBody = rigidBody;
            }
            integrator = new(() => 1, stepperMethod: positionStepper);
            Initialize();
        }

        private void OnEnable()
        {
            if (motorLoad != null)
            {
                motorLoad.AttachActuator(() => motorOutput);
            }
        }

        private void OnDisable()
        {
            if (motorLoad != null)
            {
                motorLoad.DetachActuator();
            }
        }

        /// <summary>
        /// Obtains the total inertia given an attached <see cref="MotorLoad"/>.
        /// </summary>
        public float totalInertia 
            => (motorLoad != null)? armatureInertia + motorLoad.loadInertia : armatureInertia;

        /// <summary>
        /// Obtains the total damping given an attached <see cref="MotorLoad"/>.
        /// </summary>
        public float totalDamping => 
            (motorLoad != null)? armatureDamping + motorLoad.loadDamping : armatureDamping;

        /// <summary>
        /// The output of the motor given constraints.
        /// </summary>
        public float motorOutput
        {
            get {
                float speed = MotorFunction(inputs, parameters);
                
                integrator.input = speed;
                integrator.Compute();

                float futurePosition = integrator.output;

                if (futurePosition > maxPosition 
                    || futurePosition < minPosition)
                {
                    motorLoad.normalizedAngle = Mathf.Clamp(futurePosition, minPosition, maxPosition);
                    speed = 0;
                }
                else
                {
                    speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
                }
                return speed;
            }
        }
    }
}