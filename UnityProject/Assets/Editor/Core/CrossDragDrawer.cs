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
                for (int i = 0; i < 6; i++) // 6 rows for Forces and Torques
                {
                    foldouts[i] = EditorGUILayout.Foldout(foldouts[i], $"Force/Torque {((i < 3) ? $"X{(i % 3)}" : $"K{(i % 3)}")}");
                    if (foldouts[i])
                    {
                        EditorGUI.indentLevel++;

                        for (int j = 0; j < 15; j++) // 15 columns for pairwise coefficients
                        {
                            int index = i * 15 + j;
                            if (index < dragCoefficientsProperty.arraySize)
                            {
                                EditorGUILayout.PropertyField(dragCoefficientsProperty.GetArrayElementAtIndex(index), new GUIContent(labels[j]));
                            }
                        }

                        EditorGUI.indentLevel--;
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
