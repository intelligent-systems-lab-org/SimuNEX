using SimuNEX.Loads;
using System.Linq;
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
            string[] parameterNames = serializedObject.DrawFoldout<ParameterAttribute>("ParametersExpanded", "Parameters");

            // SFX foldout
            string[] sfxNames = serializedObject.DrawFoldout<SFXAttribute>("SFXExpanded", "SFX");

            // Other properties
            string[] removedProperties = new string[] { "m_Script" };
            DrawPropertiesExcluding(serializedObject, parameterNames.Concat(sfxNames).Concat(removedProperties).ToArray());

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
