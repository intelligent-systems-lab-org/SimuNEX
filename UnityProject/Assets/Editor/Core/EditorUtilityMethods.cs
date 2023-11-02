using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    public static class EditorUtilityMethods
    {
        public static void DrawFoldout(
            SerializedObject serializedObject,
            FieldInfo[] fields,
            string editorPrefsKey,
            string foldoutLabelPrefix)
        {
            int fieldCount = fields.Length;

            if (fieldCount == 0)
            {
                return;
            }

            string foldoutLabel = $"{foldoutLabelPrefix} ({fieldCount})";

            bool areFieldsExpanded = EditorPrefs.GetBool(editorPrefsKey, false);
            areFieldsExpanded = EditorGUILayout.Foldout(areFieldsExpanded, foldoutLabel);
            EditorPrefs.SetBool(editorPrefsKey, areFieldsExpanded);

            if (areFieldsExpanded)
            {
                EditorGUI.indentLevel++;
                foreach (FieldInfo field in fields)
                {
                    SerializedProperty prop = serializedObject.FindProperty(field.Name);
                    if (prop != null && prop.propertyType == SerializedPropertyType.Float)
                    {
                        _ = EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(field.Name), GUILayout.ExpandWidth(true));
                        prop.floatValue = EditorGUILayout.FloatField(prop.floatValue, GUILayout.Width(100));
                        EditorGUILayout.EndHorizontal();
                    }
                    else if (prop != null)
                    {
                        _ = EditorGUILayout.PropertyField(prop, new GUIContent(ObjectNames.NicifyVariableName(field.Name)));
                    }
                }

                EditorGUI.indentLevel--;
            }
        }
    }
}
