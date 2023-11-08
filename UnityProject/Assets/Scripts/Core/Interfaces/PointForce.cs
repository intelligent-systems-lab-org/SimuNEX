using UnityEngine;

namespace SimuNEX.Mechanical.Forces
{
    /// <summary>
    /// Implements a constant force that acts at a point on the <see cref="RigidBody"/>.
    /// </summary>
    public abstract class PointForce : Force
    {
        /// <summary>
        /// The value of the force to apply.
        /// </summary>
        protected abstract float force { get; }

        /// <summary>
        /// Direction where the force acts.
        /// </summary>
        protected abstract Direction direction { get; }

        /// <summary>
        /// Point where the force acts.
        /// </summary>
        public Transform point;

        /// <summary>
        /// Name of the point where the force acts.
        /// </summary>
        protected abstract string centerName { get; }

        protected void OnValidate()
        {
            FindCenter();
        }

        protected void Awake()
        {
            FindCenter();
        }

        /// <summary>
        /// Attempts to find a child with the name <see cref="centerName"/> and assigns it as the <see cref="point"/>.
        /// </summary>
        protected void FindCenter()
        {
            if (point == null)
            {
                Transform potentialCenter = transform.Find(centerName);
                if (potentialCenter != null)
                {
                    point = potentialCenter;
                }
                else
                {
                    GameObject centerObject = new(centerName);
                    centerObject.transform.SetParent(transform);
                    centerObject.transform.localPosition = Vector3.zero;
                    point = centerObject.transform;
                }
            }
        }

        public override void ApplyForce()
        {
            rigidBody.AddLinearForceAtPosition(direction.ToVector() * force, point.position);
        }
    }
}
