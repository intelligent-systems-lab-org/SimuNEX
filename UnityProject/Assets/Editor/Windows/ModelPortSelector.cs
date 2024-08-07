using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimuNEX.Editors
{
    public abstract class ModelPortSelectorWindow<T> : EditorWindow where T : ModelPort
    {
        private List<T> modelPorts;
        private List<bool> selected;
        private Action<List<T>> onSelectionComplete;
        private Vector2 scrollPosition;

        protected abstract string WindowTitle { get; }
        protected abstract string SelectButtonText { get; }

        public static void ShowWindow<U>(List<T> modelPorts, Action<List<T>> onSelectionComplete) where U : ModelPortSelectorWindow<T>
        {
            U window = GetWindow<U>(false, "Select Ports", true);
            window.titleContent = new GUIContent(window.WindowTitle);
            window.modelPorts = modelPorts;
            window.selected = new List<bool>(new bool[modelPorts.Count]);
            window.onSelectionComplete = onSelectionComplete;
        }

        protected void OnGUI()
        {
            EditorGUILayout.LabelField(SelectButtonText, EditorStyles.boldLabel);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            int columns = Mathf.Max(1, (int)(position.width / 200));
            int rows = Mathf.CeilToInt(modelPorts.Count / (float)columns);

            for (int row = 0; row < rows; row++)
            {
                _ = EditorGUILayout.BeginHorizontal();
                for (int col = 0; col < columns; col++)
                {
                    int index = (row * columns) + col;
                    if (index < modelPorts.Count)
                    {
                        selected[index] = EditorGUILayout.Toggle(modelPorts[index].name, selected[index]);
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("OK"))
            {
                List<T> selectedPorts = new();
                for (int i = 0; i < modelPorts.Count; i++)
                {
                    if (selected[i])
                    {
                        selectedPorts.Add(modelPorts[i]);
                    }
                }

                onSelectionComplete?.Invoke(selectedPorts);
                Close();
            }
        }
    }

    public class ModelOutputSelectorWindow : ModelPortSelectorWindow<ModelOutput>
    {
        protected override string WindowTitle => "Select ModelOutputs";
        protected override string SelectButtonText => "Select ModelOutputs";

        public static void ShowWindow(List<ModelOutput> modelOutputs, Action<List<ModelOutput>> onSelectionComplete)
        {
            ShowWindow<ModelOutputSelectorWindow>(modelOutputs, onSelectionComplete);
        }
    }

    public class ModelInputSelectorWindow : ModelPortSelectorWindow<ModelInput>
    {
        protected override string WindowTitle => "Select ModelInputs";
        protected override string SelectButtonText => "Select ModelInputs";

        public static void ShowWindow(List<ModelInput> modelInputs, Action<List<ModelInput>> onSelectionComplete)
        {
            ShowWindow<ModelInputSelectorWindow>(modelInputs, onSelectionComplete);
        }
    }
}
