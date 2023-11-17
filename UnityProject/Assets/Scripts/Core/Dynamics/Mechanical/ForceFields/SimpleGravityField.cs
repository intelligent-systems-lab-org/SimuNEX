using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Applies a constant gravitational acceleration to all <see cref="RigidBody"/> objects within the field.
    /// </summary>
    [DisallowMultipleComponent]
    public class SimpleGravityField : ForceField
    {
        public float acceleration = 9.81f;

        public override void Apply(RigidBody rigidBody)
        {
            SimpleGravity simpleGravity = rigidBody.gameObject.TryGetComponent(out SimpleGravity existingGravity)
                ? existingGravity
                : rigidBody.gameObject.AddComponent<SimpleGravity>();

            // Check for an existing SimpleGravity component before adding
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
