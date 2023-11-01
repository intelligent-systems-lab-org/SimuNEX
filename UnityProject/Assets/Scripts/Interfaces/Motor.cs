using System;
using static SimuNEX.StateSpaceTypes;
using UnityEngine;
using System.Diagnostics;

namespace SimuNEX
{
    /// <summary>
    /// For defining quantities with bounds.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Min = {min}, Max = {max}")]
    public struct Limits
    {
        public float max;
        public float min;
    }

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
        /// Bounds for speed values.
        /// </summary>
        public Limits speedLimits = new() { max = Mathf.Infinity, min = Mathf.NegativeInfinity };

        /// <summary>
        /// Bounds for position values.
        /// </summary>
        public Limits positionLimits = new() { max = Mathf.Infinity, min = Mathf.NegativeInfinity };

        /// <summary>
        /// The motor inertia in kg.m^2.
        /// </summary>
        public float armatureInertia = 0.5f;

        /// <summary>
        /// The motor damping coefficient in N.m.s/rad.
        /// </summary>
        public float armatureDamping;

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

        protected void OnValidate()
        {
            InitializeBase();
            Initialize();
        }

        protected void Awake()
        {
            InitializeBase();
            Initialize();
        }

        /// <summary>
        /// Locates <see cref="RigidBody"/> and initializes <see cref="integrator"/>.
        /// </summary>
        protected void InitializeBase()
        {
            if (TryGetComponent(out motorLoad))
            {
                motorLoad.rigidBody = rigidBody;
            }

            integrator = new(stepperMethod: positionStepper);
        }

        protected void OnEnable()
        {
            if (motorLoad != null)
            {
                motorLoad.AttachActuator(() => motorOutput);
            }
        }

        protected void OnDisable()
        {
            if (motorLoad != null)
            {
                motorLoad.DetachActuator();
            }
        }

        /// <summary>
        /// Obtains the total inertia given an attached <see cref="MotorLoad"/>.
        /// </summary>
        public float totalInertia =>
            (motorLoad != null) ? armatureInertia + motorLoad.loadInertia : armatureInertia;

        /// <summary>
        /// Obtains the total damping given an attached <see cref="MotorLoad"/>.
        /// </summary>
        public float totalDamping =>
            (motorLoad != null) ? armatureDamping + motorLoad.loadDamping : armatureDamping;

        /// <summary>
        /// The output of the motor given constraints.
        /// </summary>
        public float motorOutput
        {
            get
            {
                float speed = MotorFunction(inputs, parameters);

                integrator.input = speed;
                integrator.Compute();

                float futurePosition = integrator.output;

                if (futurePosition > positionLimits.max || futurePosition < positionLimits.min)
                {
                    motorLoad.normalizedAngle = Mathf.Clamp(
                        futurePosition,
                        positionLimits.min,
                        positionLimits.max
                    );
                    speed = 0;
                }
                else
                {
                    speed = Mathf.Clamp(speed, speedLimits.min, speedLimits.max);
                }

                return speed;
            }
        }
    }
}
