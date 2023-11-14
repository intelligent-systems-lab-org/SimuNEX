using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Applies a constant buoyant _force to all <see cref="RigidBody"/> objects within the field.
    /// Functions on <see cref="RigidBodyF"/> objects only.
    /// </summary>
    [DisallowMultipleComponent]
    public class SimpleBuoyancyField : ForceField
    {
        /// <summary>
        /// Density of the surrounding fluid.
        /// </summary>
        public float fluidDensity = 1000f;

        public override void Apply(RigidBody rigidBody)
        {
            SimpleBuoyancy simpleBuoyancy = rigidBody.gameObject.TryGetComponent(out SimpleBuoyancy existingBuoyancy)
                ? existingBuoyancy
                : rigidBody.gameObject.AddComponent<SimpleBuoyancy>();

            // Check for an existing SimpleBuoyancy component before adding
            simpleBuoyancy.fluidDensity = fluidDensity;
        }

        public override void Remove(RigidBody rigidBody)
        {
            // Try to find a SimpleBouyancy component attached to the Rigidbody's GameObject
            if (!rigidBody.gameObject.TryGetComponent(out SimpleBuoyancy existingBuoyancy))
            {
                return;
            }
            // If found, destroy it
            Destroy(existingBuoyancy);
        }
    }
}
