using UnityEditor;
using UnityEngine;

/// <summary>
/// Implementation of a constant bouyant force.
/// </summary>
public class SimpleBuoyancy : Force
{
    /// <summary>
    /// The bouyant force.
    /// </summary>
    public float buoyantForce = 1f;

    /// <summary>
    /// The center of buoyancy.
    /// </summary>
    public Vector3 centerOfBuoyancy = Vector3.zero;

    /// <summary>
    /// Apply the bouyant force to the specified <see cref="RigidBody"/> object.
    /// </summary>
    public override void ApplyForce()
    {
        Vector3 bouyantForce = buoyantForce * new Vector3(0, 1, 0);
        rb.AddLinearForceAtPosition(bouyantForce, centerOfBuoyancy, CoordinateFrame.ICF);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SimpleBuoyancy))]
public class SimpleBuoyancyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleBuoyancy buoyancy = (SimpleBuoyancy)target;

        if (buoyancy.gameObject.GetComponents<SimpleBuoyancy>().Length > 1)
        {
            EditorGUILayout.HelpBox("Only one SimpleBuoyancy component can be added to the GameObject.", MessageType.Error);
            return;
        }

        DrawDefaultInspector();
    }
}
#endif