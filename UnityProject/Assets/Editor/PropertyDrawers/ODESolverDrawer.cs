using SimuNEX.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomPropertyDrawer(typeof(ODESolver))]
    public class ODESolverDrawer : PropertyDrawer
    {
        private static readonly Dictionary<string, bool> foldoutStates = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.BeginProperty(position, label, property);

            string propertyPath = property.propertyPath;
            bool foldoutState = foldoutStates.ContainsKey(propertyPath) && foldoutStates[propertyPath];

            foldoutState = EditorGUI.Foldout(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                foldoutState,
                label,
                true);

            foldoutStates[propertyPath] = foldoutState;

            if (foldoutState)
            {
                // Increase indentation
                EditorGUI.indentLevel++;

                // Adjust the position for the contents of the foldout
                position.y += EditorGUIUtility.singleLineHeight;
                position.height = EditorGUIUtility.singleLineHeight;

                ODESolver solver = fieldInfo.GetValue(property.serializedObject.targetObject) as ODESolver;

                // Dropdown for solver types
                Type[] solverTypes = Factory<ODESolver>.GetAvailableTypes();
                string[] solverTypeNames = solverTypes.Select(t => t.Name).ToArray();
                int currentSolverIndex = Array.IndexOf(solverTypes, solver?.GetType());

                // Default to ForwardEuler if no current solver
                if (currentSolverIndex == -1)
                {
                    Type forwardEulerType = typeof(ForwardEuler);
                    solver = Factory<ODESolver>.Create(forwardEulerType);
                    fieldInfo.SetValue(property.serializedObject.targetObject, solver);
                    currentSolverIndex = Array.IndexOf(solverTypes, forwardEulerType);
                }

                int selectedSolverIndex = EditorGUI.Popup(
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                    "Solver Type",
                    currentSolverIndex,
                    solverTypeNames
                );

                // Update solver type if changed
                if (currentSolverIndex != selectedSolverIndex)
                {
                    solver = Factory<ODESolver>.Create(solverTypes[selectedSolverIndex]);
                    fieldInfo.SetValue(property.serializedObject.targetObject, solver);
                }

                // Field for step size
                position.y += EditorGUIUtility.singleLineHeight;
                float stepSize = -1f;
                stepSize = EditorGUI.FloatField(
                    new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                    "Step Size",
                    stepSize
                );
                if (solver != null)
                {
                    solver.stepSize = stepSize;
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            string propertyPath = property.propertyPath;
            bool isFoldedOut = foldoutStates.ContainsKey(propertyPath) && foldoutStates[propertyPath];
            if (isFoldedOut)
            {
                // Height when foldout is open
                return (EditorGUIUtility.singleLineHeight * 3) + EditorGUIUtility.standardVerticalSpacing;
            }
            // Height when foldout is closed
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
