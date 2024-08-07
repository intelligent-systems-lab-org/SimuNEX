using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimuNEX.Editors
{
    public class ModelOutputSelectorWindow : EditorWindow
    {
        private List<ModelOutput> modelOutputs;
        private List<bool> selected;
        private Action<List<ModelOutput>> onSelectionComplete;

        private Vector2 scrollPosition;

        public static void ShowWindow(List<ModelOutput> modelOutputs, Action<List<ModelOutput>> onSelectionComplete)
        {
            ModelOutputSelectorWindow window = GetWindow<ModelOutputSelectorWindow>("Select ModelOutputs");
            window.modelOutputs = modelOutputs;
            window.selected = new List<bool>(new bool[modelOutputs.Count]);
            window.onSelectionComplete = onSelectionComplete;
        }

        protected void OnGUI()
        {
            EditorGUILayout.LabelField("Select ModelOutputs", EditorStyles.boldLabel);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            int columns = Mathf.Max(1, (int)(position.width / 200));
            int rows = Mathf.CeilToInt(modelOutputs.Count / (float)columns);

            for (int row = 0; row < rows; row++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int col = 0; col < columns; col++)
                {
                    int index = (row * columns) + col;
                    if (index < modelOutputs.Count)
                    {
                        selected[index] = EditorGUILayout.Toggle(modelOutputs[index].name, selected[index]);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("OK"))
            {
                List<ModelOutput> selectedOutputs = new List<ModelOutput>();
                for (int i = 0; i < modelOutputs.Count; i++)
                {
                    if (selected[i])
                    {
                        selectedOutputs.Add(modelOutputs[i]);
                    }
                }
                onSelectionComplete?.Invoke(selectedOutputs);
                Close();
            }
        }
    }
}
