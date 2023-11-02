using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomEditor(typeof(Actuator), true)] // true makes it work for derived classes as well
    public class ActuatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Use reflection to gather all fields with the [Parameters] attribute
            FieldInfo[] parameterFields = serializedObject.targetObject.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => Attribute.IsDefined(f, typeof(ParameterAttribute)))
                .ToArray();

            int paramCount = parameterFields.Length;
            string foldoutLabel = $"Parameters ({paramCount})";

            bool areParametersExpanded = EditorPrefs.GetBool("ParametersExpanded", false); // Use EditorPrefs to remember foldout state
            areParametersExpanded = EditorGUILayout.Foldout(areParametersExpanded, foldoutLabel);
            EditorPrefs.SetBool("ParametersExpanded", areParametersExpanded);

            if (areParametersExpanded)
            {
                EditorGUI.indentLevel++;
                foreach (FieldInfo field in parameterFields)
                {
                    SerializedProperty prop = serializedObject.FindProperty(field.Name);
                    if (prop != null)
                    {
                        _ = EditorGUILayout.PropertyField(prop, new GUIContent(ObjectNames.NicifyVariableName(field.Name)));
                    }
                }

                EditorGUI.indentLevel--;
            }

            // Draw other properties
            string[] parameterNames = parameterFields.Select(f => f.Name).ToArray();
            DrawPropertiesExcluding(serializedObject, parameterNames);

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
