using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    // List to keep track of all RigidBodies inside the collider.
    private List<RigidBody> rigidBodiesWithinBounds = new();

    /// <summary>
    /// Bounding box of the environment.
    /// </summary>
    private BoxCollider boxBounds;

    private void Start()
    {
        // Get the BoxCollider attached to this GameObject.
        
        if(!TryGetComponent(out boxBounds))
        {
            Debug.LogError("No BoxCollider found on the GameObject. Please attach one.");
            return;
        }
        boxBounds.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
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
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform parentTransform = other.transform;

        // Traverse up the hierarchy until you find a GameObject with the RigidBody (or another) component
        while (parentTransform != null && parentTransform.GetComponent<RigidBody>() == null)
        {
            parentTransform = parentTransform.parent;
        }

        if (parentTransform != null)
        {
            if (parentTransform.TryGetComponent<RigidBody>(out var rb))
            {
                rigidBodiesWithinBounds.Remove(rb);
            }
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