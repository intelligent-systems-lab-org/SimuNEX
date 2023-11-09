namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Applies a constant buoyant force to all <see cref="RigidBody"/> objects within the field.
    /// Functions on <see cref="RigidBodyF"/> objects only.
    /// </summary>
    public class SimpleBuoyancyField : ForceField
    {
        /// <summary>
        /// Density of the surrounding fluid.
        /// </summary>
        public float fluidDensity = 1000f;

        public override void Apply(RigidBody rigidBody)
        {
            // Check for an existing SimpleBuoyancy component before adding
            if (rigidBody.gameObject.TryGetComponent(out SimpleBuoyancy existingBuoyancy))
            {
                // If there's an existing SimpleBuoyancy, remove it first
                Destroy(existingBuoyancy);
            }

            SimpleBuoyancy simpleBuoyancy = rigidBody.gameObject.AddComponent<SimpleBuoyancy>();
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
