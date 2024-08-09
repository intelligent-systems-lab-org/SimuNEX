using SimuNEX.Communication;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimuNEX.Editors
{
    [CustomEditor(typeof(DataStream))]
    public class DataStreamEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DataStream dataStream = (DataStream)target;

            // Draw the default inspector
            DrawDefaultInspector();
        }
    }
}
