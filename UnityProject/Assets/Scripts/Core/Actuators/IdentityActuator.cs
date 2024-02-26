using SimuNEX.Loads;

namespace SimuNEX.Actuators
{
    /// <summary>
    /// Implements the identity function where output = input.
    /// </summary>
    public class IdentityActuator : Actuator
    {
        /// <summary>
        /// Output value.
        /// </summary>
        [Faultable]
        protected float output;

        [Input]
        /// <summary>
        /// The input value.
        /// </summary>
        public float input;

        /// <summary>
        /// <see cref="Load"/> object that is attached to the actuator.
        /// </summary>
        public Load load;

        public override void SetInputs(float[] value) => input = value[0];

        protected void Awake()
        {
            InitializeVariables();
            Initialize();
        }

        protected override void Initialize()
        {
            if (TryGetComponent(out load))
            {
                load.rigidBody = rigidBody;
            }

            if (load != null)
            {
                inputNames = new string[] { $"{load.gameObject.name} Actuator Input" };
            }
            else
            {
                inputNames = new string[] { $"{gameObject.name} Actuator Input" };
            }
        }

        protected void OnEnable()
        {
            if (load != null)
            {
                load.AttachActuator(() =>
                {
                    Step();
                    return output;
                });
            }
        }

        protected void OnDisable()
        {
            if (load != null)
            {
                load.DetachActuator();
            }
        }

        protected override void ComputeStep()
        {
            output = inputs()[0];
        }

        protected override void ConstraintsStep()
        {
        }
    }
}
