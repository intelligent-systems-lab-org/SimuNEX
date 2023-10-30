using UnityEditor;
using UnityEngine;

/// <summary>
/// Implementation of quadratic drag where the force depends on the square of the velocity of the <see cref="RigidBody"/>.
/// </summary>
public class QuadraticDrag : Force
{
    /// <summary>
    /// Drag coefficients defined as a <see cref="Matrix6DOF"/>, 
    /// where each row refers to a force applied to a DOF and each column refers to a velocity DOF.
    /// </summary>
    public Matrix6DOF dragCoefficients = new();

    public override void ApplyForce()
    {
        rigidBody.AddForce(dragCoefficients * rigidBody.velocity.Apply(v => Mathf.Abs(v) * v) * -1);
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