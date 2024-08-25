namespace SimuNEX.Dynamics
{
    /// <summary>
    /// Models a pure integrator with gain.
    /// </summary>
    public class Integrator : StateSpace
    {
        /// <summary>
        /// The gain of the integrator, which is multiplied by the output of the integrator.
        /// </summary>
        public float Gain;

        /// <summary>
        /// Initializes a new instance of the <see cref="Integrator"/> class.
        /// </summary>
        /// <param name="gain">A function that returns the gain of the system.</param>
        /// <param name="initialState">The initial state value.</param>
        public Integrator
        (
            float gain = 1f,
            float initialState = 0
        )
        {
            Gain = gain;

            // Initialize with 1 state (output) and 1 input
            Initialize
            (
                1,
                1,
                new Matrix(1, 1, new float[] { initialState })
            );

            // The derivative function for an integrator
            DerivativeFcn = (_, inputs) => Gain * inputs;
        }

        /// <summary>
        /// The system's output, which is the value of its sole state.
        /// </summary>
        public float output => _states[0, 0];

        /// <summary>
        /// The system's input.
        /// </summary>
        public float input
        {
            get => _inputs[0, 0];
            set => _inputs[0, 0] = value;
        }
    }

    /// <summary>
    /// Models a 1st-order transfer function.
    /// </summary>
    public class FirstOrderTF : StateSpace
    {
        /// <summary>
        /// The time constant of the system. It represents the speed
        /// at which the system responds to changes in input.
        /// </summary>
        public float TimeConstant;

        /// <summary>
        /// The DC gain of the system. It represents the steady-state
        /// change in output for a given change in input.
        /// </summary>
        public float DCGain;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstOrderTF"/> class.
        /// </summary>
        /// <param name="timeConstant">A function that returns the time constant of the system.</param>
        /// <param name="dcGain">A function that returns the DC gain of the system.</param>
        /// <param name="initialState">The initial state value.</param>
        public FirstOrderTF
        (
            float timeConstant,
            float dcGain,
            float initialState = 0
        )
        {
            TimeConstant = timeConstant;
            DCGain = dcGain;

            // Initialize with 1 state (output) and 1 input
            Initialize
            (
                1,
                1,
                new Matrix(1, 1, new float[] { initialState })
            );

            // The derivative function for a 1st-order TF
            DerivativeFcn = (states, inputs) => 1 / TimeConstant * ((DCGain * inputs) - states);
        }

        /// <summary>
        /// The system's output, which is the value of its sole state.
        /// </summary>
        public float output => _states[0, 0];

        /// <summary>
        /// The system's input.
        /// </summary>
        public float input
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
        public Matrix A;

        /// <summary>
        /// Input matrix.
        /// </summary>
        public Matrix B;

        /// <summary>
        /// Output matrix.
        /// </summary>
        public Matrix C;

        /// <summary>
        /// Direct feedthrough matrix.
        /// </summary>
        public Matrix D;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearStateSpace"/> class.
        /// </summary>
        /// <param name="A">System matrix.</param>
        /// <param name="B">Input matrix.</param>
        /// <param name="C">Output matrix.</param>
        /// <param name="D">Direct feedthrough matrix.</param>
        /// <param name="initialConditions">Initial state values.</param>
        public LinearStateSpace
        (
            Matrix A,
            Matrix B,
            Matrix C = null,
            Matrix D = null,
            float[] initialConditions = null
        )
        {
            this.A = A;
            this.B = B;
            // If C is null, default to identity matrix of size A's row count
            this.C = C ?? Matrix.Eye(A.RowCount);
            this.D = D;

            int stateCount = A.RowCount;
            int inputCount = B.ColCount;

            // If initialConditions is null, default to a zero-filled array
            float[] initialValues = initialConditions ?? new float[stateCount];

            Initialize
            (
                stateCount,
                inputCount,
                new Matrix(stateCount, 1, initialValues)
            );
            DerivativeFcn = (states, inputs) => (A * states) + (B * inputs);
        }

        /// <summary>
        /// The system's output values.
        /// </summary>
        public Matrix outputs => D == null ? C * _states : (C * _states) + (D * _inputs);
    }
}
