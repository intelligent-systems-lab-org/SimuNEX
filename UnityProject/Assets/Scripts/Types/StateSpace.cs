using System;
using static SimuNEX.Steppers;

namespace SimuNEX
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
        /// <see cref="ODESolver"/> with a <see cref="StepperMethod"/>.
        /// </summary>
        private ODESolver _odeSolver;

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
        /// <param name="stepper">The stepper method to be used. If not provided, the Forward Euler method will be used as default.</param>
        /// <exception cref="ArgumentException">Thrown when the number of rows in <paramref name="initialConditions"/> doesn't match <paramref name="numStates"/>.</exception>
        public void Initialize
        (
            int numStates,
            int numInputs,
            Matrix initialConditions,
            DerivativeFunction derivativeFunction = null,
            ODESolver stepper = null
        )
        {
            inputSize = numInputs;
            stateSize = numStates;
            _inputs = (numInputs > 0)? new Matrix(inputSize, 1, new float[inputSize]) : null;

            if (initialConditions.RowCount != stateSize || initialConditions.ColCount != 1)
            {
                throw new ArgumentException(@"
                    The initialConditions matrix must have the same number of rows 
                    as numStates and exactly one column.");
            }

            _states = initialConditions;
            DerivativeFcn = derivativeFunction;
            _odeSolver = stepper ?? new ForwardEuler();
        }


        /// <summary>
        /// The input vector.
        /// </summary>
        public Matrix inputs
        {
            get => _inputs;
            set
            {
                if (value.RowCount != inputSize)
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
        /// The current <see cref="ODESolver"/>.
        /// </summary>
        public ODESolver solver
        {
            get => _odeSolver;
            set => _odeSolver = value;
        }

        /// <summary>
        /// Delegate for the derivative function.
        /// </summary>
        public delegate Matrix DerivativeFunction(Matrix states, Matrix inputs);

        /// <summary>
        /// The derivative function used for numerical integration.
        /// </summary>
        public DerivativeFunction DerivativeFcn { get; set; }

        /// <summary>
        /// Derivative function for numerical integration.
        /// </summary>
        /// <param name="states">The state vector.</param>
        /// <param name="inputs">The input vector.</param>
        /// <returns>The Derivative vector.</returns>
        public Matrix Derivatives(Matrix states, Matrix inputs)
        {
            if (DerivativeFcn == null)
            {
                throw new InvalidOperationException("DerivativeFunction not set.");
            }

            return DerivativeFcn(states, inputs);
        }

        /// <summary>
        /// Perform numerical integration for the current timestep.
        /// </summary>
        public void Compute()
        {
            _odeSolver.Step(this);
        }

        /// <summary>
        /// Creates an <see cref="ODESolver"/> from the chosen <see cref="StepperMethod"/>.
        /// </summary>
        /// <param name="stepperMethod">The <see cref="StepperMethod"/> of choice.</param>
        /// <returns>The <see cref="ODESolver"/> that uses the desired <see cref="StepperMethod"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws when an unsupported method is passed.</exception>
        public static ODESolver CreateStepper(StepperMethod stepperMethod = StepperMethod.Euler)
        {
            return stepperMethod switch
            {
                StepperMethod.Euler => new ForwardEuler(),
                StepperMethod.Heun => new Heun(),
                StepperMethod.RK4 => new RK4(),
                _ => throw new ArgumentOutOfRangeException(nameof(stepperMethod),
                    $"Not expected stepper method: {stepperMethod}"),
            };
        }
    }
}