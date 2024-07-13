using UnityEditor;
using UnityEngine;

namespace SimuNEX.Editor
{
    [CustomEditor(typeof(Model))]
    public class ModelEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Get the target object
            Model model = (Model)target;

            // Get the serialized object
            SerializedObject serializedObject = new SerializedObject(model);
            SerializedProperty dynamicsProperty = serializedObject.FindProperty("dynamics");

            // Start horizontal group
            EditorGUILayout.BeginHorizontal();

            // Custom Dynamics property drawer
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(dynamicsProperty, GUIContent.none, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            // Init button
            if (GUILayout.Button("Init", GUILayout.Width(50)))
            {
                model.Init();
                EditorUtility.SetDirty(model); // Ensure the model is marked as dirty
            }

            // End horizontal group
            EditorGUILayout.EndHorizontal();

            // Draw the rest of the default inspector
            DrawPropertiesExcluding(serializedObject, new string[] { "dynamics", "m_Script" });

            serializedObject.ApplyModifiedProperties();
        }
    }
}
