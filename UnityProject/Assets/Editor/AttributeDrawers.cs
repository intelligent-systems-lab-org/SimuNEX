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

            // Parameters foldout
            FieldInfo[] parameterFields = serializedObject.targetObject.GetFieldsWithAttribute<ParameterAttribute>();
            DrawFoldout(parameterFields, "ParametersExpanded", "Parameters");

            // Inputs foldout
            FieldInfo[] inputFields = serializedObject.targetObject.GetFieldsWithAttribute<InputAttribute>();
            DrawFoldout(inputFields, "InputsExpanded", "Inputs");

            // Draw other properties
            string[] parameterNames = parameterFields.Select(f => f.Name).ToArray();
            string[] inputNames = inputFields.Select(f => f.Name).ToArray();
            DrawPropertiesExcluding(serializedObject, parameterNames.Concat(inputNames).ToArray());

            _ = serializedObject.ApplyModifiedProperties();
        }

        private void DrawFoldout(FieldInfo[] fields, string editorPrefsKey, string foldoutLabelPrefix)
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
