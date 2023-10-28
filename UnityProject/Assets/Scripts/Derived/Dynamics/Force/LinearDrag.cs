using UnityEditor;

/// <summary>
/// Implementation of linear drag.
/// </summary>
public class LinearDrag : Force
{
    /// <summary>
    /// Drag coefficients defined as six numbers, where each applies to a DOF.
    /// </summary>
    public Vector6DOF dragCoefficients;

    public override void ApplyForce()
    {
        rigidBody.AddForce(-1 * rigidBody.velocity * dragCoefficients);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(LinearDrag))]
public class LinearDragEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LinearDrag linDrag = (LinearDrag)target;

        if (linDrag.gameObject.GetComponents<LinearDrag>().Length > 1)
        {
            EditorGUILayout.HelpBox("Only one LinearDrag component can be added to the GameObject.", MessageType.Error);
            return;
        }

        DrawDefaultInspector();
    }
}
#endif