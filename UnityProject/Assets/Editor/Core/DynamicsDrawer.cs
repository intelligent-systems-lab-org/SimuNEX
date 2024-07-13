using SimuNEX;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Core
{
    [CustomPropertyDrawer(typeof(Dynamics))]
    public class DynamicsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.BeginProperty(position, label, property);

            Dynamics dynamics = fieldInfo.GetValue(property.serializedObject.targetObject) as Dynamics;

            // Dropdown for solver types
            Type[] dynamicsTypes = Factory<Dynamics>.GetAvailableTypes();
            string[] dynamicsTypeNames = dynamicsTypes.Select(t => t.Name).ToArray();

            int currentDynamicsIndex = dynamics != null ?
                Array.IndexOf(dynamicsTypes, dynamics.GetType())
                : -1;

            // Default to NoDynamics if no current dynamics
            if (currentDynamicsIndex == -1)
            {
                Type NoType = typeof(NoDynamics);
                dynamics = Factory<Dynamics>.Create(NoType);
                fieldInfo.SetValue(property.serializedObject.targetObject, dynamics);
                currentDynamicsIndex = Array.IndexOf(dynamicsTypes, NoType);
            }

            int selectedSolverIndex = EditorGUI.Popup(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                "Dynamics",
                currentDynamicsIndex,
                dynamicsTypeNames
            );

            // Update dynamics type if changed
            if (currentDynamicsIndex != selectedSolverIndex)
            {
                dynamics = Factory<Dynamics>.Create(dynamicsTypes[selectedSolverIndex]);
                fieldInfo.SetValue(property.serializedObject.targetObject, dynamics);
            }

            EditorGUI.EndProperty();
        }
    }
}
