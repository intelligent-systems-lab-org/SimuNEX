using UnityEditor;
using UnityEngine;
using SimuNEX.Communication;
using System.Linq;
using System;

namespace SimuNEX.Editors
{
    [CustomEditor(typeof(COM))]
    public class COMEditor : Editor
    {
        private bool showConfiguration;
        private string portName = "";
        private bool isInputPort = true;
        private int portSize = 1;
        private int portCopies = 1;
        private int selectedProtocolIndex;
        private string[] availableProtocols;

        protected void OnEnable()
        {
            // Load available protocols
            Type[] protocolTypes = Factory<COMProtocol>.GetAvailableTypes();
            availableProtocols = protocolTypes.Select(t => t.Name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            COM com = (COM)target;

            DrawDefaultInspector();

            showConfiguration = EditorGUILayout.Foldout(showConfiguration, "Port Configuration");

            if (showConfiguration)
            {
                EditorGUILayout.BeginVertical("box");

                // Port Configuration Menu
                EditorGUILayout.LabelField("Add Port", EditorStyles.boldLabel);
                portName = EditorGUILayout.TextField("Name", portName);
                isInputPort = EditorGUILayout.Popup("Type", isInputPort ? 0 : 1, new[] { "Input", "Output" }) == 0;
                portSize = EditorGUILayout.IntField("Size", portSize);
                portCopies = EditorGUILayout.IntField("Copies", portCopies);

                if (GUILayout.Button("Add"))
                {
                    com.AddPort(portName, isInputPort, portSize, portCopies);
                    EditorUtility.SetDirty(com); // Mark the object as "dirty" to ensure the changes are saved
                }

                EditorGUILayout.LabelField("Add Stream", EditorStyles.boldLabel);
                selectedProtocolIndex = EditorGUILayout.Popup("Protocol", selectedProtocolIndex, availableProtocols);

                if (GUILayout.Button("Add"))
                {

                }

                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
