namespace SimuNEX
{
    /// <summary>
    /// Implements the identity function where output = input.
    /// </summary>
    public class IdentityActuator : Actuator
    {
        [Input]
        /// <summary>
        /// The input value.
        /// </summary>
        public float input;

        /// <summary>
        /// <see cref="Load"/> object that is attached to the actuator.
        /// </summary>
        public Load load;

        public override void SetInput(float[] value) => input = value[0];

        protected void OnValidate()
        {
            Initialize();
        }

        protected void Awake()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            if (TryGetComponent(out load))
            {
                load.rigidBody = rigidBody;
            }

            inputs = () => new float[1] { input };

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
                load.AttachActuator(() => inputs()[0]);
            }
        }

        protected void OnDisable()
        {
            if (load != null)
            {
                load.DetachActuator();
            }
        }
    }
}
