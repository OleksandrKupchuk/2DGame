using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Attribute))]
public class AttributeEditor : Editor {
    SerializedProperty _attributeTypeProperty;
    SerializedProperty _valueTypeProperty;
    SerializedProperty _valueProperty;
    SerializedProperty _dmageMinProperty;
    SerializedProperty _dmageMaxProperty;

    public override void OnInspectorGUI() {

        _attributeTypeProperty = serializedObject.FindProperty("type");
        _valueTypeProperty = serializedObject.FindProperty("valueType");
        _valueProperty = serializedObject.FindProperty("value");
        _dmageMinProperty = serializedObject.FindProperty("damageMin");
        _dmageMaxProperty = serializedObject.FindProperty("damageMax");

        serializedObject.Update();
        EditorGUILayout.PropertyField(_attributeTypeProperty);
        EditorGUILayout.PropertyField(_valueTypeProperty);

        if (_attributeTypeProperty.intValue == (int)AttributeType.Damage) {
            if (_valueTypeProperty.intValue == (int)ValueType.Integer) {
                EditorGUILayout.PropertyField(_dmageMinProperty);
                EditorGUILayout.PropertyField(_dmageMaxProperty);
            }
            else {
                EditorGUILayout.PropertyField(_valueProperty);
            }
        }
        else {
            EditorGUILayout.PropertyField(_valueProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}