using System;

namespace SimuNEX.Dynamics
{
    /// <summary>
    /// Interface for modelling systems represented by state-spaces.
    /// </summary>
    public class StateSpace
    {
        /// <summary>
        /// Input vector.
        /// </summary>
        protected Matrix _inputs;

        /// <summary>
        /// State vector.
        /// </summary>
        protected Matrix _states;

        /// <summary>
        /// Number of inputs.
        /// </summary>
        public int inputSize;

        /// <summary>
        /// Number of states.
        /// </summary>
        public int stateSize;

        /// <summary>
        /// Initializes the state and input matrices with the given initial conditions.
        /// </summary>
        /// <param name="numStates">The number of states in the system.</param>
        /// <param name="numInputs">The number of inputs to the system.</param>
        /// <param name="initialConditions">The matrix representing the initial state conditions. The number of rows should match <paramref name="numStates"/>.</param>
        /// <param name="derivativeFunction">The function representing the system's differential equations. If not provided, must be set externally before any computations.</param>
        /// <exception cref="ArgumentException">Thrown when the number of rows in <paramref name="initialConditions"/> doesn't match <paramref name="numStates"/>.</exception>
        public void Initialize
        (
            int numStates,
            int numInputs,
            Matrix initialConditions,
            DerivativeFunction derivativeFunction = null
        )
        {
            inputSize = numInputs;
            stateSize = numStates;
            _inputs = (numInputs > 0) ? new Matrix(inputSize, 1, new float[inputSize]) : null;

            if (initialConditions.RowCount != stateSize || initialConditions.ColCount != 1)
            {
                throw new ArgumentException(@"
                    The initialConditions matrix must have the same number of rows 
                    as numStates and exactly one column.");
            }

            _states = initialConditions;
            DerivativeFcn = derivativeFunction;
        }

        /// <summary>
        /// The input vector.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public Matrix inputs
        {
            get => _inputs;

            set
            {
                if (value.RowCount != inputSize || value.ColCount != 1)
                {
                    throw new ArgumentException();
                }

                _inputs = value;
            }
        }

        /// <summary>
        /// The state vector.
        /// </summary>
        public Matrix states
        {
            get => _states;
            set => _states = value;
        }

        /// <summary>
        /// Delegate for the derivative function.
        /// </summary>
        /// <param name="states">The state vector.</param>
        /// <param name="inputs">The input vector.</param>
        public delegate Matrix DerivativeFunction(Matrix states, Matrix inputs);

        /// <summary>
        /// The derivative function used for numerical integration.
        /// </summary>
        public DerivativeFunction DerivativeFcn { get; set; }
    }
}
