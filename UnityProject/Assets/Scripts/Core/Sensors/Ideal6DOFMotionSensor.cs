using UnityEngine;

namespace SimuNEX.Sensors
{
    /// <summary>
    /// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
    /// </summary>
    public class Ideal6DOFMotionSensor : Sensor
    {
        /// <summary>
        /// Measured velocity value.
        /// </summary>
        [Output]
        [Faultable]
        [SerializeField]
        protected Vector3 velocity;

        /// <summary>
        /// Measured angular velocity.
        /// </summary>
        [Output]
        [Faultable]
        [SerializeField]
        protected Vector3 angularVelocity;

        /// <summary>
        /// Measured angular position.
        /// </summary>
        [Output]
        [Faultable]
        [SerializeField]
        protected Quaternion angularPosition;

        /// <summary>
        /// Measured position.
        /// </summary>
        [Output]
        [Faultable]
        [SerializeField]
        protected Vector3 position;

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

        public override void Initialize()
        {
            OutputNames = GenerateOutputNames();
        }

        /// <summary>
        /// Generates output names by prepending the name of the <see cref="RigidBody"/>
        /// to the default values.
        /// </summary>
        /// <returns>A string array containing output names.</returns>
        private string[] GenerateOutputNames()
        {
            if (rigidBody == null)
            {
                return OutputLabels;
            }

            string rigidBodyName = rigidBody.gameObject.name;
            string[] generatedNames = new string[OutputLabels.Length];

            for (int i = 0; i < OutputLabels.Length; i++)
            {
                generatedNames[i] = $"{rigidBodyName} {OutputLabels[i]}";
            }

            return generatedNames;
        }

        protected override void ComputeStep()
        {
            velocity = rigidBody.velocity.linear;
            angularVelocity = rigidBody.velocity.angular;
            angularPosition = rigidBody.angularPosition;
            position = rigidBody.position;
        }
    }
}
