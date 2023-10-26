/// <summary>
/// Implements an ideal sensor that measures velocities and positions of a <see cref="RigidBody"/>.
/// </summary>
public class Ideal6DOFMotionSensor : Sensor
{
    protected override void Initialize()
    {
        outputs = () => new float[]
        {
            rb.velocity.linear.x, rb.velocity.linear.z, rb.velocity.linear.y,
            rb.velocity.angular.x, rb.velocity.angular.z, rb.velocity.angular.y,
            rb.angularPosition.w, rb.angularPosition.x, rb.angularPosition.z, rb.angularPosition.y,
            rb.position.x, rb.position.z, rb.position.y
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
