using UnityEngine;
using UnityEditor;
using SimuNEX.Examples;

namespace SimuNEX.Editors
{
    [CustomEditor(typeof(SimuNEXDemo), true)]  // 'true' tells Unity to use this editor for derived classes as well
    [CanEditMultipleObjects]
    public class SimuNEXDemoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the properties of the actual subclass
            DrawDefaultInspector();

            SimuNEXDemo demo = (SimuNEXDemo)target;

            // Add the "Init" button
            if (GUILayout.Button("Init"))
            {
                demo.Init();
            }
        }
    }
}
