using System.Linq;
using System.Reflection;
using UnityEditor;

namespace SimuNEX
{
    [CustomEditor(typeof(Actuator), true)]
    [CanEditMultipleObjects]
    public class ActuatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Parameters foldout
            FieldInfo[] parameterFields = serializedObject.targetObject.GetFieldsWithAttribute<ParameterAttribute>();
            serializedObject.DrawFoldout(parameterFields, "ParametersExpanded", "Parameters");

            // Inputs foldout
            FieldInfo[] inputFields = serializedObject.targetObject.GetFieldsWithAttribute<InputAttribute>();
            serializedObject.DrawFoldout(inputFields, "InputsExpanded", "Inputs");

            // Other properties
            string[] parameterNames = parameterFields.Select(f => f.Name).ToArray();
            string[] inputNames = inputFields.Select(f => f.Name).ToArray();
            string[] removedProperties = new string[] { "m_Script" };
            DrawPropertiesExcluding(serializedObject, parameterNames.Concat(inputNames).Concat(removedProperties).ToArray());

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
