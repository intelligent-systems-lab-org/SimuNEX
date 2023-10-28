using UnityEditor;
using UnityEngine;

/// <summary>
/// Implementation of a simple gravity force.
/// </summary>
public class SimpleGravity : Force
{
    /// <summary>
    /// The acceleration due to gravity.
    /// </summary>
    public float acceleration = 9.81f;

    /// <summary>
    /// The center of gravity.
    /// </summary>
    public Transform centerOfGravity;

    /// <summary>
    /// Apply the gravity force to the specified <see cref="RigidBody"/> object.
    /// </summary>
    public override void ApplyForce()
    {
        Vector3 gravityForce = rb.mass * acceleration * Vector3.down;
        rb.AddLinearForceAtPosition(gravityForce, centerOfGravity.position);
    }

    /// <summary>
    /// Calculate the weight of the specified <see cref="RigidBody"/> object.
    /// </summary>
    /// <returns>The weight of the dynamics object.</returns>
    public float weight => rb.mass * acceleration;
}

#if UNITY_EDITOR

[CustomEditor(typeof(SimpleGravity))]
public class SimpleGravityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleGravity gravity = (SimpleGravity)target;

        if (gravity.gameObject.GetComponents<SimpleGravity>().Length > 1)
        {
            EditorGUILayout.HelpBox("Only one SimpleGravity component can be added to the GameObject.", MessageType.Error);
            return;
        }

        DrawDefaultInspector();
    }
}
#endif