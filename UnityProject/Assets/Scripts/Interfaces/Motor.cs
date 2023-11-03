using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static SimuNEX.StateSpaceTypes;

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
        /// Motor speed.
        /// </summary>
        protected float _speed;

        /// <summary>
        /// Motor speed property.
        /// </summary>
        [Faultable]
        public float motorSpeed => _speed;

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

        [Parameter]
        /// <summary>
        /// The motor inertia in kg.m^2.
        /// </summary>
        public float armatureInertia = 0.5f;

        [Parameter]
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
                motorLoad.AttachActuator(() =>
                {
                    Step();
                    return motorSpeed;
                });
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
        /// Updates the current motor speed.
        /// </summary>
        public void Step()
        {
            _speed = MotorFunction(inputs, parameters);

            List<Fault> speedFaults = GetFaults("motorSpeed");

            if (speedFaults != null)
            {
                foreach (Fault fault in speedFaults)
                {
                    _speed = fault.FaultFunction(_speed);
                }
            }

            integrator.input = _speed;
            integrator.Compute();

            float futurePosition = integrator.output;

            if (futurePosition > positionLimits.max || futurePosition < positionLimits.min)
            {
                motorLoad.normalizedAngle = Mathf.Clamp(
                    futurePosition,
                    positionLimits.min,
                    positionLimits.max
                );
                _speed = 0;
            }
            else
            {
                _speed = Mathf.Clamp(_speed, speedLimits.min, speedLimits.max);
            }
        }
    }
}
