using UnityEngine;
using UnityEditor;

namespace SimuNEX.Editors
{
    [CustomEditor(typeof(Runner))]
    public class SimuNEXEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Runner simuNEX = (Runner)target;
            if (GUILayout.Button("Init"))
            {
                simuNEX.Init();
            }
        }
    }
}
