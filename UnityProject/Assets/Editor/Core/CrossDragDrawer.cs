using SimuNEX.Mechanical;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomEditor(typeof(CrossDrag))]
    public class CrossDragDrawer : Editor
    {
        private SerializedProperty dragCoefficientsProperty;
        private SerializedProperty rigidBodyProperty;
        private string[] labels = new string[] { "UV", "UW", "UP", "UQ", "UR", "VW", "VP", "VQ", "VR", "WP", "WQ", "WR", "PQ", "PR", "QR" };
        string[] foldoutLabels = { "Force X", "Force Y", "Force Z", "Torque X", "Torque Y", "Torque Z" };
        private bool[] foldouts = new bool[6];

        protected void OnEnable()
        {
            rigidBodyProperty = serializedObject.FindProperty("rigidBody");
            dragCoefficientsProperty = serializedObject.FindProperty("dragCoefficients._serializedData");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(rigidBodyProperty);

            if (dragCoefficientsProperty != null && dragCoefficientsProperty.isArray)
            {
                // Iterate over each foldout section (Force X, Force Y, Force Z, Torque X, Torque Y, Torque Z)
                for (int i = 0; i < 6; i++)
                {
                    foldouts[i] = EditorGUILayout.Foldout(foldouts[i], foldoutLabels[i]);
                    if (foldouts[i])
                    {
                        EditorGUI.indentLevel++;

                        // Iterate over the rows (3 rows)
                        for (int row = 0; row < 3; row++)
                        {
                            EditorGUILayout.BeginHorizontal();

                            // Iterate over the columns (5 columns per row)
                            for (int col = 0; col < 5; col++)
                            {
                                int index = i * 15 + row * 5 + col; // Calculate the flat index based on row and column
                                if (index < dragCoefficientsProperty.arraySize)
                                {
                                    // Draw the float field with the label above it
                                    EditorGUILayout.BeginVertical();
                                    EditorGUILayout.LabelField(labels[row * 5 + col], GUILayout.Width(50));
                                    SerializedProperty elementProperty = dragCoefficientsProperty.GetArrayElementAtIndex(index);
                                    EditorGUILayout.PropertyField(elementProperty, GUIContent.none, GUILayout.Width(50));
                                    EditorGUILayout.EndVertical();
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel--;
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
