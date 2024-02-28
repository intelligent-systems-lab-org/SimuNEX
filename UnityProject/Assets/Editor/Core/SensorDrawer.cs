using SimuNEX.Sensors;
using System.Linq;
using UnityEditor;

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
            string[] outputNames = serializedObject.DrawFoldout<OutputAttribute>("OutputsExpanded", "Outputs");
            string[] solverNames = serializedObject.DrawFoldout<SolverAttribute>("SolverExpanded", "Solvers");
            string[] omittedNames = serializedObject.targetObject
                .GetFieldsWithAttribute<OmittableAttribute>(includePrivate: true, applyOmitLogic: false)
                .Select(f => f.Name)
                .ToArray();

            string[] removedProperties = new string[] { "m_Script", "faults" };

            DrawPropertiesExcluding(
                serializedObject,
                parameterNames.Concat(outputNames)
                    .Concat(removedProperties)
                    .Concat(solverNames)
                    .Concat(omittedNames)
                    .ToArray());

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
