using SimuNEX.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomPropertyDrawer(typeof(Fault), true)]
    public class FaultDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _ = EditorGUI.BeginProperty(position, label, property);

            string faultName = property.managedReferenceFullTypename.Split(' ').Last().Split('.').Last();

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, faultName);

            _ = EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }

    public static class FaultableExtensions
    {
        /// <summary>
        /// Static dictionary to store foldout states.
        /// </summary>
        private static readonly Dictionary<string, bool> foldoutStates = new();
        private static int selectedFaultableIndex;
        private static int selectedFaultIndex;

        public static void DrawFaultAddition(this SerializedObject serializedObject)
        {
            FaultEntity faultSystem = serializedObject.targetObject as FaultEntity;

            const string editorPrefsKey = "FaultsExpanded";
            string uniqueKey = serializedObject.GetHashCode() + editorPrefsKey;

            // Get list of Faultable properties
            string[] faultables = faultSystem.GetFieldsWithAttribute<FaultableAttribute>(includePrivate: true)
                .Select(p => p.Name)
                .ToArray();

            if (faultables.Length > 0)
            {
                if (!foldoutStates.ContainsKey(uniqueKey))
                {
                    foldoutStates[uniqueKey] = EditorPrefs.GetBool(editorPrefsKey, false);
                }

                foldoutStates[uniqueKey] = EditorGUILayout.Foldout(foldoutStates[uniqueKey], "Fault Menu");

                if (foldoutStates[uniqueKey])
                {
                    // Draw the faults list using the default property drawer
                    SerializedProperty faultsProperty = serializedObject.FindProperty("faults");
                    EditorGUILayout.PropertyField(faultsProperty, new GUIContent("Faults"), true);

                    // Determine the FaultableAttribute of the selected property
                    FieldInfo selectedField = faultSystem.GetType().GetField(
                        faultables[selectedFaultableIndex],
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    FaultableAttribute faultableAttr = selectedField.GetCustomAttribute<FaultableAttribute>();

                    // Initialize with all available fault types
                    IEnumerable<Type> filteredFaultTypes = Factory<Fault>.GetAvailableTypes();

                    // Get existing faults for the selected property and filter if not empty
                    List<Fault> existingFaults = faultSystem.GetFaults(faultables[selectedFaultableIndex]);

                    if (existingFaults != null && existingFaults.Count > 0)
                    {
                        HashSet<Type> existingFaultTypes = new(existingFaults.Select(f => f.GetType()));
                        filteredFaultTypes = filteredFaultTypes.Where(faultType => !existingFaultTypes.Contains(faultType));
                    }

                    if (faultableAttr.UnsupportedFaults.Length > 0)
                    {
                        filteredFaultTypes = filteredFaultTypes.Except(faultableAttr.UnsupportedFaults);
                    }
                    else if (faultableAttr.SupportedFaults.Length > 0)
                    {
                        filteredFaultTypes = filteredFaultTypes.Intersect(faultableAttr.SupportedFaults);
                    }

                    string[] faultTypeNames = filteredFaultTypes.Select(t => t.Name).ToArray();

                    selectedFaultableIndex = EditorGUILayout.Popup("Faultable Properties", selectedFaultableIndex, faultables);

                    // Do not show available faults if empty
                    if (faultTypeNames.Length > 0)
                    {
                        selectedFaultIndex = EditorGUILayout.Popup("Available Faults", selectedFaultIndex, faultTypeNames);
                    }

                    if (GUILayout.Button("Apply Fault"))
                    {
                        string selectedFaultableProperty = faultables[selectedFaultableIndex];
                        Type selectedFaultType = filteredFaultTypes.ElementAt(selectedFaultIndex);

                        AddFault(selectedFaultableProperty, selectedFaultType, serializedObject);
                    }
                }
            }
        }

        private static void AddFault(string property, Type faultType, SerializedObject serializedObject)
        {
            Debug.Log("Adding fault of type: " + faultType.Name);

            Fault newFault = Factory<Fault>.Create(faultType);
            FaultEntity faultSystem = serializedObject.targetObject as FaultEntity;

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
