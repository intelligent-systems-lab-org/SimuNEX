using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomPropertyDrawer(typeof(Matrix6DOF))]
    public class Matrix6DOFDrawer : PropertyDrawer
    {
        private bool isExpanded;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.GetValue(property.serializedObject.targetObject) is not Matrix6DOF matrix)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();

            isExpanded = EditorGUI.Foldout(
                new Rect(
                    position.x,
                    position.y,
                    position.width,
                    EditorGUIUtility.singleLineHeight),
                isExpanded,
                label);

            if (isExpanded)
            {
                EditorGUI.indentLevel++;
                float cellWidth = position.width / 6;
                float cellHeight = EditorGUIUtility.singleLineHeight;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Rect cellPosition = new(
                            position.x + (j * cellWidth),
                            position.y + ((i + 1) * cellHeight),
                            cellWidth,
                            cellHeight);
                        matrix[i, j] = EditorGUI.FloatField(cellPosition, matrix[i, j]);
                    }
                }

                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return isExpanded ? (6 + 1) * EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }
    }
}
