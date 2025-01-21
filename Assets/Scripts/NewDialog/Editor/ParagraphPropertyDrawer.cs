using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Paragraph))]
public class ParagraphPropertyDrawer : PropertyDrawer {
    private int _amountLines = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty _paragraphProperty = property.FindPropertyRelative("_paragraph");
        float _paragraphHeight = EditorGUIUtility.singleLineHeight * _amountLines;
        Rect _paragraphPosition = new Rect(position.x, position.y, position.width, _paragraphHeight);

        EditorGUI.BeginChangeCheck();
        string input = EditorGUI.TextArea(_paragraphPosition, _paragraphProperty.stringValue);

        if (EditorGUI.EndChangeCheck()) {
            _paragraphProperty.stringValue = input;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUIUtility.singleLineHeight * _amountLines;
    }
}
