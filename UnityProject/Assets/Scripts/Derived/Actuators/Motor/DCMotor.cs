using System;
using static SimuNEX.StateSpaceTypes;
using UnityEngine;

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
        public StepperMethod stepper;

        /// <summary>
        /// The input voltage.
        /// </summary>
        public float voltage = 0;

        /// <summary>
        /// Armature resistance in ohms.
        /// </summary>
        public float armatureResistance = 20f;

        /// <summary>
        /// The constant representing the relationship between the back electromotive force (EMF) 
        /// and the armature speed in V.s/rads.
        /// </summary>
        public float backEMFConstant = 1f;

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
            };
            float DCGain() 
            {
                float[] param = parameters();
                return timeConstant() * param[2] / (param[0] * param[3]); 
            };

            inputs = () => new float[] { voltage };
            stateSpace = new FirstOrderTF(timeConstant, DCGain, stepperMethod: stepper);
        }

        public override float MotorFunction(Func<float[]> inputs, Func<float[]> parameters)
        {
            stateSpace.input = inputs()[0];
            stateSpace.Compute();

            // Apply saturation
            stateSpace.states[0, 0] = Mathf.Clamp(stateSpace.output, lowerSaturation, upperSaturation);

            return stateSpace.output;
        }
    }
}