using UnityEditor;

namespace SimuNEX
{
    [CustomEditor(typeof(Sensor), true)]
    [CanEditMultipleObjects]
    public class SensorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            string[] removedProperties = new string[] { "m_Script", "faults" };

            DrawPropertiesExcluding(serializedObject, removedProperties);

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
