using System.Linq;
using System.Reflection;
using UnityEditor;
using static SimuNEX.EditorUtilityMethods;

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
            DrawFoldout(serializedObject, parameterFields, "ParametersExpanded", "Parameters");

            // Inputs foldout
            FieldInfo[] inputFields = serializedObject.targetObject.GetFieldsWithAttribute<InputAttribute>();
            DrawFoldout(serializedObject, inputFields, "InputsExpanded", "Inputs");

            // Other properties
            string[] parameterNames = parameterFields.Select(f => f.Name).ToArray();
            string[] inputNames = inputFields.Select(f => f.Name).ToArray();
            DrawPropertiesExcluding(serializedObject, parameterNames.Concat(inputNames).ToArray());

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
