using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    public static class SerializedObjectExtensions
    {
        /// <summary>
        /// Static dictionary to store foldout states.
        /// </summary>
        private static readonly Dictionary<string, bool> foldoutStates = new();

        public static void DrawFoldout(
            this SerializedObject serializedObject,
            FieldInfo[] fields,
            string editorPrefsKey,
            string foldoutLabelPrefix)
        {
            int fieldCount = fields.Length;
            if (fieldCount == 0)
            {
                return;
            }

            // Use the serializedObject's hashCode or another unique identifier.
            string uniqueKey = serializedObject.GetHashCode() + editorPrefsKey;

            // Get the foldout state from the dictionary.
            if (!foldoutStates.ContainsKey(uniqueKey))
            {
                foldoutStates[uniqueKey] = EditorPrefs.GetBool(editorPrefsKey, false);
            }

            string foldoutLabel = $"{foldoutLabelPrefix} ({fieldCount})";
            foldoutStates[uniqueKey] = EditorGUILayout.Foldout(foldoutStates[uniqueKey], foldoutLabel);

            if (foldoutStates[uniqueKey])
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
