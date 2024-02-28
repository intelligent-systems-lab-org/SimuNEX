using SimuNEX.Loads;
using SimuNEX.Models;
using SimuNEX.Solvers;
using System;
using UnityEngine;

namespace SimuNEX.Actuators
{
    /// <summary>
    /// Interface for implementing motors.
    /// </summary>
    public abstract class Motor : Actuator
    {
        /// <summary>
        /// Motor speed.
        /// </summary>
        [Faultable]
        protected float motorSpeed;

        /// <summary>
        /// <see cref="MotorLoad"/> object that is attached to the motor.
        /// </summary>
        public MotorLoad motorLoad;

        /// <summary>
        /// Bounds for speed values.
        /// </summary>
        [Constraint]
        public Limits speedLimits = new() { max = Mathf.Infinity, min = Mathf.NegativeInfinity };

        /// <summary>
        /// Bounds for position values.
        /// </summary>
        [Constraint]
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

        [SerializeReference]
        [Solver]
        public ODESolver positionSolver;

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

        protected void OnEnable()
        {
            if (TryGetComponent(out motorLoad))
            {
                motorLoad.rigidBody = rigidBody;
            }

            if (motorLoad != null)
            {
                motorLoad.AttachActuator(() =>
                {
                    Step();
                    return motorSpeed;
                });
            }

            integrator = new(solverMethod: positionSolver);
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

        protected override void ComputeStep()
        {
            motorSpeed = MotorFunction(inputs, parameters);
        }

        protected override void ConstraintsStep()
        {
            integrator.input = motorSpeed;
            integrator.Compute();

            float futurePosition = integrator.output % 2 * MathF.PI;

            if (futurePosition > positionLimits.max || futurePosition < positionLimits.min)
            {
                motorLoad.normalizedAngle = Mathf.Clamp(
                    futurePosition,
                    positionLimits.min,
                    positionLimits.max
                );
                motorSpeed = 0;
            }
            else
            {
                motorSpeed = Mathf.Clamp(motorSpeed, speedLimits.min, speedLimits.max);
            }
        }
    }
}
