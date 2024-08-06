//using System.Collections.Generic;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;

//namespace SimuNEX.Editors
//{
//    [CustomEditor(typeof(ModelSystem))]
//    public class ModelSystemEditor : Editor
//    {
//        private ModelSystem modelSystem;

//        protected void OnEnable()
//        {
//            modelSystem = (ModelSystem)target;
//        }

//        public override void OnInspectorGUI()
//        {
//            DrawDefaultInspector();

//            EditorGUILayout.Space();
//            EditorGUILayout.LabelField("Model System Ports", EditorStyles.boldLabel);

//            DrawPorts();

//            if (GUILayout.Button("Sort Models Topologically"))
//            {
//                modelSystem.models = modelSystem.TopologicalSort();
//            }
//        }

//        private void DrawPorts()
//        {
//            EditorGUILayout.LabelField("Inputs", EditorStyles.boldLabel);
//            foreach (ModelInput input in modelSystem.inports)
//            {
//                DrawInputPort(input);
//            }

//            EditorGUILayout.Space();
//            EditorGUILayout.LabelField("Outputs", EditorStyles.boldLabel);
//            foreach (ModelOutput output in modelSystem.outports)
//            {
//                DrawOutputPort(output);
//            }

//            EditorGUILayout.Space();
//            EditorGUILayout.LabelField("Internal Mappings", EditorStyles.boldLabel);
//            foreach (Model model in modelSystem.models)
//            {
//                foreach (ModelOutput output in model.outports)
//                {
//                    DrawInternalMapping(output);
//                }
//            }
//        }

//        private void DrawInputPort(ModelInput input)
//        {
//            EditorGUILayout.BeginHorizontal();

//            EditorGUILayout.LabelField(input.name);

//            List<ModelOutput> compatibleOutputs = GetCompatibleOutputs(input);
//            string[] outputNames = compatibleOutputs.Select(o => o.name).ToArray();
//            //int selectedIndex = compatibleOutputs.IndexOf(modelSystem.inputMappings.FirstOrDefault(kvp => kvp.Value.Contains(input)).Key);

//            //int newIndex = EditorGUILayout.Popup(selectedIndex, outputNames);

//            //if (newIndex != selectedIndex)
//            //{
//            //    if (selectedIndex != -1)
//            //    {
//            //        modelSystem.UnmapInput(input);
//            //    }

//            //    if (newIndex != -1)
//            //    {
//            //        modelSystem.MapInput(compatibleOutputs[newIndex], input);
//            //    }
//            //}

//            EditorGUILayout.EndHorizontal();
//        }

//        private void DrawOutputPort(ModelOutput output)
//        {
//            EditorGUILayout.BeginHorizontal();

//            EditorGUILayout.LabelField(output.name);

//            List<ModelInput> compatibleInputs = GetCompatibleInputs(output);
//            string[] inputNames = compatibleInputs.Select(i => i.name).ToArray();
//            //int selectedIndex = compatibleInputs.IndexOf(modelSystem.outputMappings.FirstOrDefault(kvp => kvp.Value.Contains(output)).Key);

//            //int newIndex = EditorGUILayout.Popup(selectedIndex, inputNames);

//            //if (newIndex != selectedIndex)
//            //{
//            //    if (selectedIndex != -1)
//            //    {
//            //        modelSystem.UnmapOutput(output);
//            //    }

//            //    if (newIndex != -1)
//            //    {
//            //        modelSystem.MapOutput(output, compatibleInputs[newIndex]);
//            //    }
//            //}

//            EditorGUILayout.EndHorizontal();
//        }

//        private void DrawInternalMapping(ModelOutput output)
//        {
//            EditorGUILayout.BeginHorizontal();

//            EditorGUILayout.LabelField(output.name);

//            List<ModelInput> compatibleInputs = GetCompatibleInputs(output);
//            string[] inputNames = compatibleInputs.Select(i => i.name).ToArray();
//            var currentMappings = modelSystem.internalMappings.ContainsKey(output) ? 
//                modelSystem.internalMappings[output] : new List<ModelInput>();
//            int selectedIndex = compatibleInputs.IndexOf(currentMappings.FirstOrDefault());

//            int newIndex = EditorGUILayout.Popup(selectedIndex, inputNames);

//            if (newIndex != selectedIndex)
//            {
//                if (selectedIndex != -1)
//                {
//                    modelSystem.internalMappings[output].Remove(compatibleInputs[selectedIndex]);
//                }

//                if (newIndex != -1)
//                {
//                    modelSystem.MapInternal(output, compatibleInputs[newIndex]);
//                }
//            }

//            EditorGUILayout.EndHorizontal();
//        }

//        private List<ModelOutput> GetCompatibleOutputs(ModelInput input)
//        {
//            return modelSystem.models
//                .SelectMany(m => m.outports)
//                .Where(o => o.size == input.size && !modelSystem.inputMappings.Any(kvp => kvp.Value.Contains(input)))
//                .ToList();
//        }

//        private List<ModelInput> GetCompatibleInputs(ModelOutput output)
//        {
//            return modelSystem.models
//                .SelectMany(m => m.inports)
//                .Where(i => i.size == output.size && !modelSystem.outputMappings.Any(kvp => kvp.Value.Contains(output)))
//                .ToList();
//        }
//    }
//}
