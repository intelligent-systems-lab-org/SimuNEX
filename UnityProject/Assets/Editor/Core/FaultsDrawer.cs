using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
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
            Debug.Log("Adding fault of type: " + faultType.Name); // Debug statement

            Fault newFault = FaultFactory.CreateFault(faultType);
            FaultSystem faultSystem = serializedObject.targetObject as FaultSystem;
            if (faultSystem != null)
            {
                (faultSystem.faults ??= new List<Fault>()).Add(newFault);
                EditorUtility.SetDirty(faultSystem);

                Debug.Log("Fault added. Total faults: " + faultSystem.faults.Count); // Debug statement
            }
            else
            {
                Debug.LogError("FaultSystem is null");
            }
        }
    }
}
