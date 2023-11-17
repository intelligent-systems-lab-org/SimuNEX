using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Implementation of a constant buoyant _force.
    /// </summary>
    [DisallowMultipleComponent]
    public class SimpleBuoyancy : PointForce
    {
        /// <summary>
        /// Density of the surrounding fluid.
        /// </summary>
        public float fluidDensity = 1000f;

        /// <summary>
        /// Gravitational _force that is applied to the object.
        /// </summary>
        public SimpleGravity simpleGravity;

        /// <summary>
        /// Calculates the buoyant _force for <see cref="RigidBodyF"/> objects.
        /// Has no effect on non-fluid based physics dynamics.
        /// </summary>
        protected override float force
            => rigidBody is RigidBodyF rbf ? fluidDensity
                * simpleGravity.acceleration *
                rbf._volume
                * rbf._displacedVolumeFactor : 0;

        protected override Direction direction => Direction.Up;
        protected override string centerName => "COB";

        new protected void OnValidate()
        {
            base.Awake();
            Initialize();
        }

        new protected void Awake()
        {
            base.Awake();
            Initialize();
        }

        protected void Initialize()
        {
            if (simpleGravity == null && !TryGetComponent(out simpleGravity))
            {
                simpleGravity = gameObject.AddComponent<SimpleGravity>();
            }
        }
    }
}
