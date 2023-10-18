using System;

public static class StateSpaceTypes
{
    /// <summary>
    /// Models a 1st-order transfer function.
    /// </summary>
    public class FirstOrderTF : StateSpace
    {
        /// <summary>
        /// The time constant of the system. It represents the speed 
        /// at which the system responds to changes in input.
        /// </summary>
        public Func<float> TimeConstant;

        /// <summary>
        /// The DC gain of the system. It represents the steady-state 
        /// change in output for a given change in input.
        /// </summary>
        public Func<float> DCGain;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstOrderTF"/> class.
        /// </summary>
        /// <param name="timeConstant">A function that returns the time constant of the system.</param>
        /// <param name="dcGain">A function that returns the DC gain of the system.</param>
        /// <param name="initialState">The initial state value.</param>
        public FirstOrderTF(Func<float> timeConstant, Func<float> dcGain, float initialState = 0)
        {
            TimeConstant = timeConstant;
            DCGain = dcGain;

            // Initialize with 1 state (output) and 1 input
            Initialize(1, 1, new Matrix(1, 1, new float[] { initialState }));
            // The derivative function for a 1st-order TF
            DerivativeFcn = (states, inputs) => (1 / TimeConstant()) * (DCGain() * inputs - states);
        }

        /// <summary>
        /// The system's output, which is the value of its sole state.
        /// </summary>
        public float Output => _states[0, 0];

        /// <summary>
        /// The system's input.
        /// </summary>
        public float Input
        {
            get => _inputs[0, 0];
            set => _inputs[0, 0] = value;
        }
    }
}
