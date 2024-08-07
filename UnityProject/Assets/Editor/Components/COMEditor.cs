using SimuNEX.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
        private int selectedInputIndex;
        private int selectedOutputIndex;
        private List<ModelOutput> selectedModelOutputs = new();

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

            _ = DrawDefaultInspector();

            showConfiguration = EditorGUILayout.Foldout(showConfiguration, "Port Configuration");

            if (showConfiguration)
            {
                _ = EditorGUILayout.BeginVertical("box");

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

                // Stream Configuration Menu
                EditorGUILayout.LabelField("Add Stream", EditorStyles.boldLabel);
                selectedProtocolIndex = EditorGUILayout.Popup("Protocol", selectedProtocolIndex, availableProtocols);

                // Dropdown for selecting COMInput
                string[] inputNames = com.dataInputs.Select(input => input.name).ToArray();

                if (inputNames.Length > 0)
                {
                    selectedInputIndex = EditorGUILayout.Popup("COMInput", selectedInputIndex, inputNames);
                }

                // Dropdown for selecting COMInput
                string[] outputNames = com.dataOutputs.Select(output => output.name).ToArray();

                if (outputNames.Length > 0)
                {
                    selectedOutputIndex = EditorGUILayout.Popup("COMOutput", selectedOutputIndex, outputNames);
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("ModelOutputs", GUILayout.Width(EditorGUIUtility.labelWidth));
                if (GUILayout.Button("Select", GUILayout.Width(EditorGUIUtility.fieldWidth)))
                {
                    ModelOutputSelectorWindow.ShowWindow(
                        com.modelPorts.Item2.ToList(),
                        selectedOutputs => selectedModelOutputs = selectedOutputs);
                }

                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("Add"))
                {
                    if (selectedProtocolIndex < 0
                        || selectedProtocolIndex >= availableProtocols.Length
                        || selectedInputIndex < 0
                        || selectedInputIndex >= com.dataInputs.Count)
                    {
                        Debug.LogWarning("Invalid selection.");
                        return;
                    }

                    Type protocolType = Factory<COMProtocol>.GetAvailableTypes()[selectedProtocolIndex];
                    COMProtocol protocolInstance = Factory<COMProtocol>.Create(protocolType);

                    DataStream dataStream = com.gameObject.AddComponent<DataStream>();
                    dataStream.Setup(
                        protocolInstance,
                        Streaming.S, // Default to Streaming.S
                        com.dataInputs[selectedInputIndex],
                        com.dataOutputs[selectedOutputIndex],
                        modelOutputs: selectedModelOutputs.ToArray()
                    );

                    com.streams.Add(dataStream);
                    EditorUtility.SetDirty(com); // Mark the object as "dirty" to ensure the changes are saved
                }

                EditorGUILayout.EndVertical();
            }

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
