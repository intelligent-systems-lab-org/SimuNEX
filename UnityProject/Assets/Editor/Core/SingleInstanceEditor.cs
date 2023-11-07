using System;
using UnityEditor;
using UnityEngine;

namespace SimuNEX
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class SingleInstanceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MonoBehaviour component = (MonoBehaviour)target;
            Type componentType = component.GetType();

            // Check if this component type has the SingleInstanceAttribute
            SingleInstanceAttribute singleInstanceAttribute = (SingleInstanceAttribute)Attribute.GetCustomAttribute(
                componentType,
                typeof(SingleInstanceAttribute));
            if (singleInstanceAttribute != null)
            {
                // Enforce that only one instance of the component type can exist on the GameObject
                Component[] components = component.gameObject.GetComponents(componentType);
                if (components.Length > 1)
                {
                    EditorGUILayout.HelpBox($"Only one instance of {componentType.Name} is allowed per GameObject.", MessageType.Error);
                    return;
                }
            }

            // Draw the default inspector
            _ = DrawDefaultInspector();
        }
    }
}
