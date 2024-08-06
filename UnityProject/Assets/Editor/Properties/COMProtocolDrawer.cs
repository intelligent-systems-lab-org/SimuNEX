using SimuNEX.Communication;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimuNEX.Editors
{
    [CustomPropertyDrawer(typeof(COMProtocol), true)]
    public class COMProtocolDrawer : PropertyDrawer
    {
        private string[] availableProtocols;
        private Type[] protocolTypes;
        private int selectedProtocolIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (availableProtocols == null)
            {
                protocolTypes = Factory<COMProtocol>.GetAvailableTypes();
                availableProtocols = protocolTypes.Select(t => t.Name).ToArray();

                // Find the current protocol type index
                string currentType = property.managedReferenceFullTypename.Split(' ').Last();
                selectedProtocolIndex = Array.FindIndex(protocolTypes, t => t.FullName == currentType);
            }

            _ = EditorGUI.BeginProperty(position, label, property);

            // Draw the protocol dropdown
            Rect dropdownRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            int newSelectedProtocolIndex = EditorGUI.Popup(dropdownRect, "Settings", selectedProtocolIndex, availableProtocols);

            // Check if the selected protocol has changed
            if (newSelectedProtocolIndex != selectedProtocolIndex)
            {
                selectedProtocolIndex = newSelectedProtocolIndex;
                Type newType = protocolTypes[selectedProtocolIndex];
                property.managedReferenceValue = Factory<COMProtocol>.Create(newType);
                _ = property.serializedObject.ApplyModifiedProperties();
                GUIUtility.ExitGUI();
            }

            // Adjust position for the default property fields
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            position.height -= EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Draw the filtered properties of the selected protocol
            if (property.managedReferenceValue != null)
            {
                Streaming streaming = GetStreamingMode(property);
                Type targetType = property.managedReferenceValue.GetType();
                foreach (FieldInfo field in targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    COMTypeAttribute attr = (COMTypeAttribute)field.GetCustomAttribute(typeof(COMTypeAttribute));
                    if (attr == null || ShouldShowField(attr.StreamingMode, streaming))
                    {
                        SerializedProperty fieldProp = property.FindPropertyRelative(field.Name);
                        if (fieldProp != null)
                        {
                            float fieldHeight = EditorGUI.GetPropertyHeight(fieldProp, true);
                            Rect fieldRect = new(position.x, position.y, position.width, fieldHeight);
                            _ = EditorGUI.PropertyField(fieldRect, fieldProp, true);
                            position.y += fieldHeight + EditorGUIUtility.standardVerticalSpacing;
                        }
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        private Streaming GetStreamingMode(SerializedProperty property)
        {
            DataStream parent = property.serializedObject.targetObject as DataStream;
            if (parent != null)
            {
                return parent.streaming;
            }

            return Streaming.SR; // Default to SR if not found
        }

        private bool ShouldShowField(Streaming fieldStreaming, Streaming streamMode)
        {
            return streamMode == Streaming.SR ||
                (streamMode == Streaming.S && (fieldStreaming == Streaming.S || fieldStreaming == Streaming.SR)) ||
                (streamMode == Streaming.R && (fieldStreaming == Streaming.R || fieldStreaming == Streaming.SR));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue == null)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            Type targetType = property.managedReferenceValue.GetType();
            foreach (FieldInfo field in targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                COMTypeAttribute attr = (COMTypeAttribute)field.GetCustomAttribute(typeof(COMTypeAttribute));
                if (attr == null || ShouldShowField(attr.StreamingMode, GetStreamingMode(property)))
                {
                    SerializedProperty fieldProp = property.FindPropertyRelative(field.Name);
                    if (fieldProp != null)
                    {
                        height += EditorGUI.GetPropertyHeight(fieldProp, true) + EditorGUIUtility.standardVerticalSpacing;
                    }
                }
            }

            return height;
        }
    }
}
