using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomPropertyDrawer(typeof(Fault), true)]
    public class FaultDrawer : PropertyDrawer
    {
        private static readonly Regex _regex = new("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.BeginProperty(position, label, property);

            string faultTypeName = property.managedReferenceFullTypename.Split(' ').Last().Split('.').Last();
            string formattedFaultName = _regex.Replace(faultTypeName, " $1").Replace("Fault", "");

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, formattedFaultName);

            _ = EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }

    public static partial class SerializedObjectExtensions
    {
        private static int selectedFaultableIndex;
        private static int selectedFaultIndex;

        public static void DrawFaultAddition(this SerializedObject serializedObject)
        {
            FaultSystem faultSystem = serializedObject.targetObject as FaultSystem;

            const string editorPrefsKey = "FaultsExpanded";

            // Use the serializedObject's hashCode or another unique identifier.
            string uniqueKey = serializedObject.GetHashCode() + editorPrefsKey;

            // Get the foldout state from the dictionary.
            if (!foldoutStates.ContainsKey(uniqueKey))
            {
                foldoutStates[uniqueKey] = EditorPrefs.GetBool(editorPrefsKey, false);
            }

            // Draw the "Fault Menu" foldout
            foldoutStates[uniqueKey] = EditorGUILayout.Foldout(foldoutStates[uniqueKey], "Fault Menu");

            if (foldoutStates[uniqueKey])
            {
                // Get list of Faultable properties
                string[] faultables = faultSystem.GetPropertiesWithAttribute<Faultable>().Select(p => p.Name).ToArray();

                // Get the list of available Faults
                Type[] faultTypes = FaultFactory.GetAvailableFaultTypes();
                string[] faultTypeNames = faultTypes.Select(t => t.Name).ToArray();

                selectedFaultableIndex = EditorGUILayout.Popup("Faultable Properties", selectedFaultableIndex, faultables);
                selectedFaultIndex = EditorGUILayout.Popup("Available Faults", selectedFaultIndex, faultTypeNames);

                if (GUILayout.Button("Apply Fault"))
                {
                    // Retrieve the selected Faultable property and Fault type
                    string selectedFaultableProperty = faultables[selectedFaultableIndex];
                    Type selectedFaultType = faultTypes[selectedFaultIndex];

                    AddFault(selectedFaultableProperty, selectedFaultType, serializedObject);
                }
            }
        }

        private static void AddFault(string property, Type faultType, SerializedObject serializedObject)
        {
            Debug.Log("Adding fault of type: " + faultType.Name);

            Fault newFault = FaultFactory.CreateFault(faultType);
            FaultSystem faultSystem = serializedObject.targetObject as FaultSystem;

            if (faultSystem != null)
            {
                faultSystem.AddFault(property, newFault);
                EditorUtility.SetDirty(faultSystem);

                Debug.Log("Fault added to property: " + property + ". Total faults: " + faultSystem.faults.Count);
            }
            else
            {
                Debug.LogError("FaultSystem is null");
            }
        }
    }
}
