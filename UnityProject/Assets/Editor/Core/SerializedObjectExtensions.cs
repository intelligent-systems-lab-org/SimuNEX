using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Draws a foldout that contains all properties of type <see cref="PropertyAttribute"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="PropertyAttribute"/> to query.</typeparam>
        /// <param name="serializedObject">Serialized object containing properties.</param>
        /// <param name="editorPrefsKey">Key for storing foldout state.</param>
        /// <param name="foldoutLabelPrefix">Label for the foldout.</param>
        /// <returns>List of names of <see cref="PropertyAttribute"/> that were drawn in the foldout.</returns>
        public static string[] DrawFoldout<T>(
            this SerializedObject serializedObject,
            string editorPrefsKey,
            string foldoutLabelPrefix) where T : PropertyAttribute
        {
            FieldInfo[] fields = serializedObject.targetObject.GetFieldsWithAttribute<T>();

            int fieldCount = fields.Length;
            if (fieldCount == 0)
            {
                return fields.Select(f => f.Name).ToArray();
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
                serializedObject.DrawFields(fields);

                EditorGUI.indentLevel--;
            }

            return fields.Select(f => f.Name).ToArray();
        }

        /// <summary>
        /// Draws an array of fields in list format.
        /// </summary>
        /// <param name="serializedObject">Serialized object containing property fields.</param>
        /// <param name="fields">List of property fields.</param>
        private static void DrawFields(this SerializedObject serializedObject, FieldInfo[] fields)
        {
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
        }
    }
}
