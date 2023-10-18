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

        public FirstOrderTF(Func<float> timeConstant, Func<float> dcGain, float initialState = 0)
        {
            TimeConstant = timeConstant;
            DCGain = dcGain;

            // Initialize with 1 state (output) and 1 input
            Initialize(1, 1, new Matrix(1, 1, new float[] { initialState }));

            // Set the derivative function for this system
            DerivativeFcn = (states, inputs) =>
            {
                float dotOutput = (1 / TimeConstant()) * (DCGain() * Input - Output);
                return new Matrix(1, 1, new float[] { dotOutput });
            };
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
