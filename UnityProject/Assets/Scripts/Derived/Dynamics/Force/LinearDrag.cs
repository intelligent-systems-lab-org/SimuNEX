using UnityEditor;

namespace SimuNEX
{
    /// <summary>
    /// Implementation of linear drag.
    /// </summary>
    public class LinearDrag : Force
    {
        /// <summary>
        /// Drag coefficients defined as a <see cref="Matrix6DOF"/>, 
        /// where each row refers to a force applied to a DOF and each column refers to a velocity DOF.
        /// </summary>
        public Matrix6DOF dragCoefficients;

        public override void ApplyForce()
        {
            rigidBody.AddForce(dragCoefficients * rigidBody.velocity * -1);
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
}