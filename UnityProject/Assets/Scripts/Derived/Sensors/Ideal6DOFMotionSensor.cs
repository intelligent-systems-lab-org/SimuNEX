namespace SimuNEX
{
    /// <summary>
    /// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
    /// </summary>
    public class Ideal6DOFMotionSensor : Sensor
    {
        protected override void Initialize()
        {
            outputs = () => new float[]
            {
                rigidBody.velocity.linear.x, rigidBody.velocity.linear.z, rigidBody.velocity.linear.y,
                rigidBody.velocity.angular.x, rigidBody.velocity.angular.z, rigidBody.velocity.angular.y,
                rigidBody.angularPosition.w, rigidBody.angularPosition.x, rigidBody.angularPosition.z, rigidBody.angularPosition.y,
                rigidBody.position.x, rigidBody.position.z, rigidBody.position.y
            };
        }

        private void OnValidate()
        {
            Initialize();
        }

        private void Awake()
        {
            Initialize();
        }

    }
}