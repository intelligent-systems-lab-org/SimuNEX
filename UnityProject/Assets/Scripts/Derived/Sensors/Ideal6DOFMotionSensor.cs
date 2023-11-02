namespace SimuNEX
{
    /// <summary>
    /// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
    /// </summary>
    public class Ideal6DOFMotionSensor : Sensor
    {
        /// <summary>
        /// Default output names for <see cref="Ideal6DOFMotionSensor"/>.
        /// </summary>
        private static readonly string[] OutputLabels = new string[]
        {
            "X Velocity",
            "Z Velocity",
            "Y Velocity",
            "X Angular Velocity",
            "Z Angular Velocity",
            "Y Angular Velocity",
            "W Angular Position",
            "X Angular Position",
            "Z Angular Position",
            "Y Angular Position",
            "X Position",
            "Z Position",
            "Y Position"
        };

        protected override void Initialize()
        {
            outputs = () => new float[]
            {
                rigidBody.velocity.linear.x,
                rigidBody.velocity.linear.z,
                rigidBody.velocity.linear.y,
                rigidBody.velocity.angular.x,
                rigidBody.velocity.angular.z,
                rigidBody.velocity.angular.y,
                rigidBody.angularPosition.w,
                rigidBody.angularPosition.x,
                rigidBody.angularPosition.z,
                rigidBody.angularPosition.y,
                rigidBody.position.x,
                rigidBody.position.z,
                rigidBody.position.y
            };

            outputNames = GenerateOutputNames();
        }

        /// <summary>
        /// Generates output names by prepending the name of the <see cref="RigidBody"/>
        /// to the default values.
        /// </summary>
        /// <returns>A string array containing output names.</returns>
        private string[] GenerateOutputNames()
        {
            string rigidBodyName = rigidBody.gameObject.name;
            string[] generatedNames = new string[OutputLabels.Length];

            for (int i = 0; i < OutputLabels.Length; i++)
            {
                generatedNames[i] = $"{rigidBodyName} {OutputLabels[i]}";
            }

            return generatedNames;
        }

        protected void OnValidate()
        {
            Initialize();
        }

        protected void Awake()
        {
            Initialize();
        }
    }
}
