using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Vector6DOF))]
public class Vector6DOFDrawer : PropertyDrawer
{
    private const float Spacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect labelRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(labelRect, label, EditorStyles.boldLabel);

        Rect linearLabelRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(linearLabelRect, "Linear");

        Rect linearRect = new(position.x, position.y + 2 * EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
        SerializedProperty linearProperty = property.FindPropertyRelative("linear");
        linearProperty.vector3Value = EditorGUI.Vector3Field(linearRect, GUIContent.none, linearProperty.vector3Value);

        Rect angularLabelRect = new(position.x, position.y + 3 * EditorGUIUtility.singleLineHeight + Spacing, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(angularLabelRect, "Angular");

        Rect angularRect = new(position.x, position.y + 4 * EditorGUIUtility.singleLineHeight + Spacing, position.width, EditorGUIUtility.singleLineHeight);
        SerializedProperty angularProperty = property.FindPropertyRelative("angular");
        angularProperty.vector3Value = EditorGUI.Vector3Field(angularRect, GUIContent.none, angularProperty.vector3Value);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 5 * (EditorGUIUtility.singleLineHeight + Spacing);
    }
}