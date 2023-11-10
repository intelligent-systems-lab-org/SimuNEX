using System.Collections.Generic;
using UnityEngine;
using SimuNEX.Mechanical;

namespace SimuNEX
{
    /// <summary>
    /// Handles environment-based interactions with SimuNEX entities.
    /// </summary>
    public class Environment : MonoBehaviour
    {
        /// <summary>
        /// Center of bounding box.
        /// </summary>
        public Vector3 center = Vector3.zero;

        /// <summary>
        /// Bounding box size.
        /// </summary>
        public Vector3 boxSize = Vector3.one;

        /// <summary>
        /// List to keep track of all RigidBodies inside the collider.
        /// </summary>
        private readonly List<RigidBody> rigidBodiesWithinBounds = new();

        /// <summary>
        /// Bounding box of the environment.
        /// </summary>
        private BoxCollider boxBounds;

        /// <summary>
        /// List of attached forcefields.
        /// </summary>
        public List<ForceField> forceFields = new();

        protected void OnValidate()
        {
            Initialize();
        }

        protected void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes bounding box and <see cref="ForceField"/> objects.
        /// </summary>
        private void Initialize()
        {
            InitializeBoundingBox();
            forceFields = new List<ForceField>(GetComponents<ForceField>());
        }

        /// <summary>
        /// Sets up the bounding box.
        /// </summary>
        private void InitializeBoundingBox()
        {
            BoxCollider[] colliders = GetComponents<BoxCollider>();

            // Destroy all BoxColliders except one
            for (int i = 1; i < colliders.Length; i++)
            {
                Destroy(colliders[i]);
            }

            // If no BoxCollider is found or only one exists, handle it
            if (colliders.Length == 0)
            {
                // No BoxCollider found, attach one
                boxBounds = gameObject.AddComponent<BoxCollider>();
            }
            else
            {
                // One BoxCollider found, use it
                boxBounds = colliders[0];
            }

            // Overwrite the properties of the BoxCollider
            boxBounds.center = center;
            boxBounds.size = boxSize;
            boxBounds.isTrigger = true;
        }

        protected void OnTriggerEnter(Collider other)
        {
            Transform parentTransform = other.transform;

            // Traverse up the hierarchy until you find a GameObject with the RigidBody (or another) component
            while (parentTransform != null && parentTransform.GetComponent<RigidBody>() == null)
            {
                parentTransform = parentTransform.parent;
            }

            if (parentTransform != null && parentTransform.gameObject != null)
            {
                RigidBody rb = parentTransform.GetComponent<RigidBody>();
                if (rb != null && !rigidBodiesWithinBounds.Contains(rb))
                {
                    rigidBodiesWithinBounds.Add(rb);

                    // Apply all the _force fields when a RigidBody enters the environment
                    foreach (ForceField forceField in forceFields)
                    {
                        forceField.Apply(rb);
                    }
                }
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            Transform parentTransform = other.transform;

            // Traverse up the hierarchy until you find a GameObject with the RigidBody (or another) component
            while (parentTransform != null && parentTransform.GetComponent<RigidBody>() == null)
            {
                parentTransform = parentTransform.parent;
            }

            if (parentTransform == null || !parentTransform.TryGetComponent(out RigidBody rb))
            {
                return;
            }

            _ = rigidBodiesWithinBounds.Remove(rb);

            // Remove all the _force fields when a RigidBody exits the environment
            foreach (ForceField forceField in forceFields)
            {
                forceField.Remove(rb);
            }
        }

        /// <summary>
        /// Obtains the list of detected <see cref="RigidBody"/> objects inside the environment.
        /// </summary>
        /// <returns>List of detected <see cref="RigidBody"/> objects.</returns>
        public IEnumerable<RigidBody> GetRigidBodiesWithinBounds()
        {
            return rigidBodiesWithinBounds;
        }
    }
}
