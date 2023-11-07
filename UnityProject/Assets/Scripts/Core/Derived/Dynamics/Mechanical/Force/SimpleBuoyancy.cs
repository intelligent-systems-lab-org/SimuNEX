using UnityEngine;

namespace SimuNEX.Mechanical.Forces
{
    /// <summary>
    /// Implementation of a constant buoyant force.
    /// </summary>
    [SingleInstance]
    public class SimpleBuoyancy : Force
    {
        /// <summary>
        /// The buoyant force.
        /// </summary>
        [SerializeField]
        private float buoyantForce = 1f;

        /// <summary>
        /// Density of the surrounding fluid.
        /// </summary>
        public float fluidDensity = 1000f;

        /// <summary>
        /// The center of buoyancy.
        /// </summary>
        public Transform centerOfBuoyancy;

        /// <summary>
        /// Gravitational force that is applied to the object.
        /// </summary>
        private SimpleGravity simpleGravity;

        protected void OnValidate()
        {
            Initialize();
        }

        protected void Awake()
        {
            Initialize();
        }

        protected void Initialize()
        {
            if (!TryGetComponent(out simpleGravity))
            {
                simpleGravity = gameObject.AddComponent<SimpleGravity>();
            }

            FindCOB();
        }

        /// <summary>
        /// Attempts to find a child with the name "COB" and assigns it as the COB.
        /// </summary>
        private void FindCOB()
        {
            if (centerOfBuoyancy == null)
            {
                Transform potentialCOB = transform.Find("COB");
                if (potentialCOB != null)
                {
                    centerOfBuoyancy = potentialCOB;
                }
            }
        }

        /// <summary>
        /// Apply the buoyant force to the specified <see cref="RigidBodyF"/> object.
        /// Has no effect on non-fluid based physics dynamics.
        /// </summary>
        public override void ApplyForce()
        {
            if (rigidBody is RigidBodyF rbf)
            {
                buoyantForce = fluidDensity * simpleGravity.acceleration * rbf._volume * rbf._displacedVolumeFactor;
                rigidBody.AddLinearForceAtPosition(Vector3.up * buoyantForce, centerOfBuoyancy.position);
            }
        }
    }
}
