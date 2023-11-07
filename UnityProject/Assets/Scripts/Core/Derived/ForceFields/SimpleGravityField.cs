using SimuNEX.Mechanical.Forces;

namespace SimuNEX.Mechanical.ForceFields
{
    /// <summary>
    /// Applies a constant gravitational acceleration to all <see cref="RigidBody"/> objects within the field.
    /// </summary>
    public class SimpleGravityField : ForceField
    {
        public float acceleration = 9.81f;

        public override void Apply(RigidBody rigidBody)
        {
            // Check for an existing SimpleGravity component before adding
            if (rigidBody.gameObject.TryGetComponent(out SimpleGravity existingGravity))
            {
                // If there's an existing SimpleGravity, remove it first
                Destroy(existingGravity);
            }

            SimpleGravity simpleGravity = rigidBody.gameObject.AddComponent<SimpleGravity>();
            simpleGravity.acceleration = acceleration;
        }

        public override void Remove(RigidBody rigidBody)
        {
            // Try to find a SimpleGravity component attached to the Rigidbody's GameObject
            if (!rigidBody.gameObject.TryGetComponent(out SimpleGravity existingGravity))
            {
                return;
            }
            // If found, destroy it
            Destroy(existingGravity);
        }
    }
}
