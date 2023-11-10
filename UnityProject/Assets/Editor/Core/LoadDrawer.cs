using SimuNEX.Loads;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace SimuNEX
{
    [CustomEditor(typeof(Load), true)]
    [CanEditMultipleObjects]
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
            string[] removedProperties = new string[] { "m_Script" };
            DrawPropertiesExcluding(serializedObject, parameterNames.Concat(removedProperties).ToArray());

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
