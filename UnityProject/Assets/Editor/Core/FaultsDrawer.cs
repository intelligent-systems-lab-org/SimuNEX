using System;
using System.Collections.Generic;
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
            EditorGUI.BeginProperty(position, label, property);

            string faultTypeName = property.managedReferenceFullTypename.Split(' ').Last().Split('.').Last();
            string formattedFaultName = _regex.Replace(faultTypeName, " $1").Replace("Fault", "");

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, formattedFaultName);

            EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }

    public static partial class SerializedObjectExtensions
    {
        public static void DrawFaultAddition(this SerializedObject serializedObject)
        {
            if (GUILayout.Button("Add Fault"))
            {
                Type[] faultTypes = FaultFactory.GetAvailableFaultTypes();
                GenericMenu menu = new();

                foreach (Type type in faultTypes)
                {
                    menu.AddItem(new GUIContent(type.Name), false, () => AddFault(type, serializedObject));
                }

                menu.ShowAsContext();
            }
        }

        private static void AddFault(Type faultType, SerializedObject serializedObject)
        {
            Debug.Log("Adding fault of type: " + faultType.Name);

            Fault newFault = FaultFactory.CreateFault(faultType);
            FaultSystem faultSystem = serializedObject.targetObject as FaultSystem;
            if (faultSystem != null)
            {
                (faultSystem.faults ??= new List<Fault>()).Add(newFault);
                EditorUtility.SetDirty(faultSystem);

                Debug.Log("Fault added. Total faults: " + faultSystem.faults.Count);
            }
            else
            {
                Debug.LogError("FaultSystem is null");
            }
        }
    }
}
