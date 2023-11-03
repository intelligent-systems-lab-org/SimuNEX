using UnityEditor;

namespace SimuNEX
{
    [CustomEditor(typeof(Sensor), true)]
    public class SensorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _ = DrawDefaultInspector();

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
