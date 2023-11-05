using System;
using UnityEngine;
using static SimuNEX.StateSpaceTypes;

namespace SimuNEX
{
    /// <summary>
    /// Implements a DC motor modeled by a 1st-order transfer function.
    /// </summary>
    public class DCMotor : Motor
    {
        /// <summary>
        /// The stepper method.
        /// </summary>
        public StepperMethod speedStepper;

        [Input]
        /// <summary>
        /// The input voltage.
        /// </summary>
        public float voltage;

        [Parameter]
        /// <summary>
        /// Armature resistance in ohms.
        /// </summary>
        public float armatureResistance = 20f;

        [Parameter]
        /// <summary>
        /// The constant representing the relationship between the back electromotive force (EMF)
        /// and the armature speed in V.s/rads.
        /// </summary>
        public float backEMFConstant = 1f;

        [Parameter]
        /// <summary>
        /// The constant representing the relationship between the motor torque
        /// and the armature current in N.m/A.
        /// </summary>
        public float torqueConstant = 10f;

        /// <summary>
        /// <see cref="FirstOrderTF"/> which defines the transfer function.
        /// </summary>
        private FirstOrderTF stateSpace;

        public override void SetInput(float[] value) => voltage = value[0];

        protected override void Initialize()
        {
            parameters = () => new float[]
            {
                armatureResistance,
                backEMFConstant,
                torqueConstant,
                totalInertia,
                totalDamping
            };

            // Convert physical parameters to 1st order TF parameters
            float timeConstant()
            {
                float[] param = parameters();
                return param[3] * 1 / (param[4] + (param[1] * param[2] / param[0]));
            }

            float DCGain()
            {
                float[] param = parameters();
                return timeConstant() * param[2] / (param[0] * param[3]);
            }

            inputs = () => new float[] { voltage };
            stateSpace = new FirstOrderTF(timeConstant, DCGain, stepperMethod: speedStepper);

            if (motorLoad != null)
            {
                inputNames = new string[] { $"{gameObject.name} {motorLoad.spinnerObject.gameObject.name} Voltage" };
            }
            else
            {
                inputNames = new string[] { $"{gameObject.name} Motor Voltage" };
            }
        }

        public override float MotorFunction(Func<float[]> inputs, Func<float[]> parameters)
        {
            // Overwrite to the actual value
            stateSpace.states[0, 0] = motorSpeed;
            stateSpace.input = inputs()[0];
            stateSpace.Compute();
            return stateSpace.output;
        }
    }
}