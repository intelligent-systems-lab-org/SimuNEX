using SimuNEX.Loads;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace SimuNEX
{
    [CustomEditor(typeof(Load), true)]
    public class LoadEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Parameters foldout
            FieldInfo[] parameterFields = serializedObject.targetObject.GetFieldsWithAttribute<ParameterAttribute>();
            serializedObject.DrawFoldout(parameterFields, "ParametersExpanded", "Parameters");

            // Other properties
            string[] parameterNames = parameterFields.Select(f => f.Name).ToArray();
            DrawPropertiesExcluding(serializedObject, parameterNames.ToArray());

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
