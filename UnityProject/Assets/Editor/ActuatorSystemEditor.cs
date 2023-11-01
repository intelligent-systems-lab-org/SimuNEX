using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomEditor(typeof(ActuatorSystem))]
    public class ActuatorSystemEditor : Editor
    {
        SerializedProperty actuatorsProp;
        SerializedProperty inputsProp;
        bool showInputsFoldout = true;

        protected void OnEnable()
        {
            // Get the serialized properties
            actuatorsProp = serializedObject.FindProperty("actuators");
            inputsProp = serializedObject.FindProperty("inputs");
        }

        public override void OnInspectorGUI()
        {
            // Draw the default inspector but exclude the inputs array
            serializedObject.Update();
            EditorGUILayout.PropertyField(actuatorsProp);
            serializedObject.ApplyModifiedProperties();

            // Get the target object
            ActuatorSystem actuatorSystem = (ActuatorSystem)target;

            // Make sure actuators and inputs are initialized
            if (actuatorSystem.actuators != null && actuatorSystem.inputs != null)
            {
                // Foldout for inputs
                showInputsFoldout = EditorGUILayout.Foldout(showInputsFoldout, $"Inputs ({actuatorSystem.inputs?.Length ?? 0})", true);
                if (showInputsFoldout)
                {
                    int inputIndex = 0;
                    foreach (Actuator actuator in actuatorSystem.actuators)
                    {
                        if (actuator.inputNames != null)
                        {
                            for (int i = 0; i < actuator.inputNames.Length; i++)
                            {
                                if (inputIndex < actuatorSystem.inputs.Length)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    EditorGUILayout.LabelField(actuator.inputNames[i], GUILayout.ExpandWidth(true));
                                    actuatorSystem.inputs[inputIndex] = EditorGUILayout.FloatField
                                    (
                                        actuatorSystem.inputs[inputIndex],
                                        GUILayout.Width(50)
                                    );
                                    EditorGUILayout.EndHorizontal();
                                }

                                inputIndex++;
                            }
                        }
                    }
                }
            }
        }
    }
}
