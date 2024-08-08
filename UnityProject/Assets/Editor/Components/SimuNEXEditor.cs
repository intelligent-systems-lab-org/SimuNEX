using UnityEngine;
using UnityEditor;

namespace SimuNEX.Editors
{
    [CustomEditor(typeof(SimuNEX))]
    public class SimuNEXEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SimuNEX simuNEX = (SimuNEX)target;
            if (GUILayout.Button("Init"))
            {
                simuNEX.Init();
            }
        }
    }
}
