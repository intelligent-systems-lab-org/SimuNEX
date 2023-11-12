using UnityEditor;
using SimuNEX.Sensors;
using System.Linq;

namespace SimuNEX
{
    [CustomEditor(typeof(Sensor), true)]
    [CanEditMultipleObjects]
    public class SensorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Foldouts
            string[] parameterNames = serializedObject.DrawFoldout<ParameterAttribute>("ParametersExpanded", "Parameters");
            string[] outputNames = serializedObject.DrawFoldout<OutputAttribute>("InputsExpanded", "Outputs");
            string[] solverNames = serializedObject.DrawFoldout<SolverAttribute>("SolverExpanded", "Solvers");

            string[] removedProperties = new string[] { "m_Script", "faults" };

            DrawPropertiesExcluding(
                serializedObject,
                parameterNames.Concat(outputNames)
                    .Concat(removedProperties)
                    .Concat(solverNames)
                    .ToArray());

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
