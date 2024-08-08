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
        private bool showInputFoldout;
        private bool showOutputFoldout;
        private string portName = "";
        private bool isInputPort = true;
        private int portSize = 1;
        private int portCopies = 1;
        private int selectedProtocolIndex;
        private string[] availableProtocols;
        private int selectedInputIndex;
        private int selectedOutputIndex;
        private List<ModelOutput> selectedModelOutputs = new();
        private List<ModelInput> selectedModelInputs = new();

        protected void OnEnable()
        {
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

                EditorGUILayout.LabelField("Add Port", EditorStyles.boldLabel);
                portName = EditorGUILayout.TextField("Name", portName);
                isInputPort = EditorGUILayout.Popup("Type", isInputPort ? 0 : 1, new[] { "Input", "Output" }) == 0;
                portSize = EditorGUILayout.IntField("Size", portSize);
                portCopies = EditorGUILayout.IntField("Copies", portCopies);

                if (GUILayout.Button("Add"))
                {
                    com.AddPort(portName, isInputPort, portSize, portCopies);
                    EditorUtility.SetDirty(com);
                }

                EditorGUILayout.EndVertical();

                if (com.dataInputs.Count > 0 || com.dataOutputs.Count > 0)
                {
                    _ = EditorGUILayout.BeginVertical("box");

                    EditorGUILayout.LabelField("Add Stream", EditorStyles.boldLabel);
                    selectedProtocolIndex = EditorGUILayout.Popup("Protocol", selectedProtocolIndex, availableProtocols);

                    if (com.dataInputs.Count > 0)
                    {
                        EditorGUI.indentLevel++;
                        showInputFoldout = EditorGUILayout.Foldout(showInputFoldout, "Select Inputs");
                        if (showInputFoldout)
                        {
                            string[] inputNames = com.dataInputs.Select(input => input.name).ToArray();
                            selectedInputIndex = EditorGUILayout.Popup("COMInput", selectedInputIndex, inputNames);

                            _ = EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("ModelOutputs", GUILayout.Width(EditorGUIUtility.labelWidth));
                            if (GUILayout.Button(
                                "Select",
                                GUILayout.Width(EditorGUIUtility.fieldWidth)))
                            {
                                ModelOutputSelectorWindow.ShowWindow(
                                    com.modelOutputs.ToList(),
                                    selectedOutputs => selectedModelOutputs = selectedOutputs);
                            }

                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUI.indentLevel--;
                    }

                    if (com.dataOutputs.Count > 0)
                    {
                        EditorGUI.indentLevel++;
                        showOutputFoldout = EditorGUILayout.Foldout(showOutputFoldout, "Select Outputs");
                        if (showOutputFoldout)
                        {
                            string[] outputNames = com.dataOutputs.Select(output => output.name).ToArray();
                            selectedOutputIndex = EditorGUILayout.Popup("COMOutput", selectedOutputIndex, outputNames);

                            _ = EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("ModelInputs", GUILayout.Width(EditorGUIUtility.labelWidth));
                            if (GUILayout.Button(
                                "Select",
                                GUILayout.Width(EditorGUIUtility.fieldWidth)))
                            {
                                ModelInputSelectorWindow.ShowWindow(
                                    com.modelInputs.ToList(),
                                    selectedInputs => selectedModelInputs = selectedInputs);
                            }

                            EditorGUILayout.EndHorizontal();
                        }

                        EditorGUI.indentLevel--;
                    }

                    if ((selectedModelOutputs != null && selectedModelOutputs.Count != 0)
                        || (selectedModelInputs != null && selectedModelInputs.Count != 0))
                    {
                        if (GUILayout.Button("Add"))
                        {
                            if (selectedProtocolIndex < 0 || selectedProtocolIndex >= availableProtocols.Length)
                            {
                                Debug.LogWarning("Invalid selection.");
                                return;
                            }

                            Type protocolType = Factory<COMProtocol>.GetAvailableTypes()[selectedProtocolIndex];
                            COMProtocol protocolInstance = Factory<COMProtocol>.Create(protocolType);

                            COMInput comInput = (selectedModelOutputs == null || selectedModelOutputs.Count == 0) ?
                                null : com.dataInputs[selectedInputIndex];

                            COMOutput comOutput = (selectedModelInputs == null || selectedModelInputs.Count == 0) ?
                                null : com.dataOutputs[selectedOutputIndex];

                            Streaming streaming = Streaming.S;

                            if (comInput != null && comOutput != null)
                            {
                                streaming = Streaming.SR;
                            }
                            else if (comInput != null && comOutput == null)
                            {
                                streaming = Streaming.R;
                            }

                            DataStream dataStream = com.gameObject.AddComponent<DataStream>();
                            dataStream.Setup(
                                protocolInstance,
                                streaming,
                                comInput,
                                comOutput,
                                selectedModelOutputs.ToArray(),
                                selectedModelInputs.ToArray()
                            );

                            com.streams.Add(dataStream);
                            EditorUtility.SetDirty(com);
                        }
                    }

                    EditorGUILayout.EndVertical();
                }
            }

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
