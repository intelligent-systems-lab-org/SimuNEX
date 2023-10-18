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
            Initialize(1, 1, new Matrix(1, 1, new float[] { initialState }), integrator: new Integrators.RK4());
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

    /// <summary>
    /// Models state spaces given by A*states + B*inputs.
    /// </summary>
    public class LinearStateSpace : StateSpace
    {
        /// <summary>
        /// System matrix.
        /// </summary>
        public Func<Matrix> A;

        /// <summary>
        /// Input matrix.
        /// </summary>
        public Func<Matrix> B;

        /// <summary>
        /// Output matrix.
        /// </summary>
        public Func<Matrix> C;

        /// <summary>
        /// Direct feedthrough matrix.
        /// </summary>
        public Func<Matrix> D;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearStateSpace"/> class.
        /// </summary>
        /// <param name="A">System matrix.</param>
        /// <param name="B">Input matrix.</param>
        /// <param name="C">Output matrix.</param>
        /// <param name="D">Direct feedthrough matrix.</param>
        /// <param name="initialConditions">Initial state values.</param>
        public LinearStateSpace(Func<Matrix> A, Func<Matrix> B, Func<Matrix> C = null, Func<Matrix> D = null, float[] initialConditions = null)
        {
            this.A = A;
            this.B = B;
            // If C is null, default to identity matrix of size A's row count
            this.C = C ?? (() => Matrix.Eye(A().RowCount));
            this.D = D;

            int stateCount = A().RowCount;
            int inputCount = B().ColCount;

            // If initialConditions is null, default to a zero-filled array
            float[] initialValues = initialConditions ?? new float[stateCount];

            Initialize(stateCount, inputCount, new Matrix(stateCount, 1, initialValues), integrator: new Integrators.RK4());
            DerivativeFcn = (states, inputs) => A() * states + B() * inputs;
        }

        /// <summary>
        /// The system's output values.
        /// </summary>
        public Matrix Outputs
        {
            get
            {
                if (D == null)
                    return C() * _states;
                else
                    return C() * _states + D() * _inputs;
            }
        }
    }
}
