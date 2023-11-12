using SimuNEX.Actuators;
using System.Linq;
using UnityEditor;

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
            string[] omittedNames = serializedObject.targetObject
                .GetFieldsWithAttribute<OmittableAttribute>(includePrivate: true, applyOmitLogic: false)
                .Select(f => f.Name)
                .ToArray();

            // Other properties
            string[] removedProperties = new string[] { "m_Script", "faults" };
            DrawPropertiesExcluding(
                serializedObject,
                parameterNames.Concat(inputNames)
                    .Concat(removedProperties)
                    .Concat(constraintNames)
                    .Concat(solverNames)
                    .Concat(omittedNames)
                    .ToArray());

            //// Draw fault addition UI
            serializedObject.DrawFaultAddition();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
