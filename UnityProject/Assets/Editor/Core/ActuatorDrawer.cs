using System.Linq;
using UnityEditor;
using SimuNEX.Actuators;

namespace SimuNEX
{
    [CustomEditor(typeof(Actuator), true)]
    [CanEditMultipleObjects]
    public class ActuatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Foldouts
            string[] parameterNames = serializedObject.DrawFoldout<ParameterAttribute>("ParametersExpanded", "Parameters");
            string[] inputNames = serializedObject.DrawFoldout<InputAttribute>("InputsExpanded", "Inputs");
            string[] constraintNames = serializedObject.DrawFoldout<ConstraintAttribute>("ConstraintsExpanded", "Constraints");
            string[] solverNames = serializedObject.DrawFoldout<SolverAttribute>("SolverExpanded", "Solvers");

            // Other properties
            string[] removedProperties = new string[] { "m_Script", "faults" };
            DrawPropertiesExcluding(
                serializedObject,
                parameterNames.Concat(inputNames)
                    .Concat(removedProperties)
                    .Concat(constraintNames)
                    .Concat(solverNames)
                    .ToArray());

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
