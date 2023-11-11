using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
    /// </summary>
    public class Ideal6DOFMotionSensor : Sensor
    {
        /// <summary>
        /// Velocity property.
        /// </summary>
        public Vector3 velocity => _velocity;

        /// <summary>
        /// Angular velocity property.
        /// </summary>
        public Vector3 angularVelocity => _angularVelocity;

        /// <summary>
        /// Angular position property.
        /// </summary>
        public Quaternion angularPosition => _angularPosition;

        /// <summary>
        /// Position property.
        /// </summary>
        public Vector3 position => _position;

        /// <summary>
        /// Measured velocity value.
        /// </summary>
        protected Vector3 _velocity;

        /// <summary>
        /// Measured angular velocity.
        /// </summary>
        protected Vector3 _angularVelocity;

        /// <summary>
        /// Measured angular position.
        /// </summary>
        protected Quaternion _angularPosition;

        /// <summary>
        /// Measured position.
        /// </summary>
        protected Vector3 _position;

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
            outputs = () =>
            {
                _velocity = rigidBody.velocity.linear;
                _angularVelocity = rigidBody.velocity.angular;
                _angularPosition = rigidBody.angularPosition;
                _position = rigidBody.position;

                ApplyFaults("velocity", ref _velocity);
                ApplyFaults("angularVelocity", ref _angularVelocity);
                ApplyFaults("angularPosition", ref _angularPosition);
                ApplyFaults("position", ref _position);

                return new float[]
                {
                    velocity.x,
                    velocity.z,
                    velocity.y,
                    angularVelocity.x,
                    angularVelocity.z,
                    angularVelocity.y,
                    angularPosition.w,
                    angularPosition.x,
                    angularPosition.z,
                    angularPosition.y,
                    position.x,
                    position.z,
                    position.y
                };
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
