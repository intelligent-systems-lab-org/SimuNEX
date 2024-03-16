using UnityEditor;
using UnityEngine;
using SimuNEX.Sensors;

namespace SimuNEX
{
    [CustomEditor(typeof(SensorSystem))]
    public class SensorSystemEditor : Editor
    {
        SerializedProperty sensorsProp;
        bool showOutputsFoldout = true;

        protected void OnEnable()
        {
            sensorsProp = serializedObject.FindProperty("sensors");
        }

        public override void OnInspectorGUI()
        {
            // Draw the default inspector but exclude the outputs array
            serializedObject.Update();
            EditorGUILayout.PropertyField(sensorsProp);
            serializedObject.ApplyModifiedProperties();

            // Get the target object
            SensorSystem sensorSystem = (SensorSystem)target;

            // Make sure sensors and inputs are initialized
            if (sensorSystem.sensors != null && sensorSystem.outputs != null)
            {
                // Foldout for inputs
                showOutputsFoldout = EditorGUILayout.Foldout(showOutputsFoldout, $"Outputs ({sensorSystem.outputs?.Length ?? 0})", true);
                if (showOutputsFoldout)
                {
                    int outputIndex = 0;
                    foreach (Sensor sensor in sensorSystem.sensors)
                    {
                        if (sensor.OutputNames != null)
                        {
                            for (int i = 0; i < sensor.OutputNames.Length; i++)
                            {
                                if (outputIndex < sensorSystem.outputs.Length)
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    EditorGUILayout.LabelField(sensor.OutputNames[i], GUILayout.ExpandWidth(true));
                                    sensorSystem.outputs[outputIndex] = EditorGUILayout.FloatField
                                    (
                                        sensorSystem.outputs[outputIndex],
                                        GUILayout.Width(50)
                                    );
                                    EditorGUILayout.EndHorizontal();
                                }

                                outputIndex++;
                            }
                        }
                    }
                }
            }
        }
    }
}
