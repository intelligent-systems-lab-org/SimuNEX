using UnityEditor;
using UnityEngine;

/// <summary>
/// Implementation of quadratic drag where the force depends on the square of the velocity of the <see cref="RigidBody"/>.
/// </summary>
public class QuadraticDrag : Force
{
    /// <summary>
    /// Drag coefficients defined as six numbers, where each applies to a DOF.
    /// </summary>
    public Vector6DOF dragCoefficients;

    public override void ApplyForce()
    {
        rigidBody.AddForce(-1 * rigidBody.velocity.Apply(v => Mathf.Abs(v) * v) * dragCoefficients);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(QuadraticDrag))]
public class QuadraticDragEditor : Editor
{
    public override void OnInspectorGUI()
    {
        QuadraticDrag quadDrag = (QuadraticDrag)target;

        if (quadDrag.gameObject.GetComponents<QuadraticDrag>().Length > 1)
        {
            EditorGUILayout.HelpBox("Only one QuadraticDrag component can be added to the GameObject.", MessageType.Error);
            return;
        }

        DrawDefaultInspector();
    }
}
#endif