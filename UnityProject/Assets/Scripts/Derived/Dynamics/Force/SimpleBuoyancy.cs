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
    public Transform centerOfBuoyancy;

    private void OnValidate()
    {
        FindCOB();
    }

    private void Awake() {
        FindCOB();
    }

    /// <summary>
    /// Attempts to find a child with the name "COB" and assigns it as the COB.
    /// </summary>
    private void FindCOB() {
        if(centerOfBuoyancy == null)
            {
                
                Transform potentialCOB = transform.Find("COB");
                if (potentialCOB != null)
                {
                    centerOfBuoyancy = potentialCOB;
                }
            }
    }

    /// <summary>
    /// Apply the bouyant force to the specified <see cref="RigidBody"/> object.
    /// </summary>
    public override void ApplyForce() 
        => rb.AddLinearForceAtPosition(Vector3.up * buoyantForce, centerOfBuoyancy.position);
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