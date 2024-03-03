using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Attribute))]
public class AttributeEditor : Editor {
    SerializedProperty _attributeTypeProperty;
    SerializedProperty _valueTypeProperty;
    SerializedProperty _valueProperty;
    SerializedProperty _dmageMinProperty;
    SerializedProperty _dmageMaxProperty;
    SerializedProperty _durationProperty;
    SerializedProperty _iconProperty;

    public override void OnInspectorGUI() {
        _attributeTypeProperty = serializedObject.FindProperty("type");
        _valueTypeProperty = serializedObject.FindProperty("valueType");
        _valueProperty = serializedObject.FindProperty("value");
        _dmageMinProperty = serializedObject.FindProperty("damageMin");
        _dmageMaxProperty = serializedObject.FindProperty("damageMax");
        _durationProperty = serializedObject.FindProperty("duration");
        _iconProperty = serializedObject.FindProperty("icon");

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

        EditorGUILayout.PropertyField(_durationProperty);

        string _path = ResourcesPath.FolderTooltip + _attributeTypeProperty.enumNames[_attributeTypeProperty.intValue];
        Sprite _icon = Resources.Load<Sprite>(_path);
        if (_icon != null) {
            _iconProperty.objectReferenceValue = _icon;
            EditorGUILayout.PropertyField(_iconProperty);
        }
        else {
            Debug.Log($"can't load sprite? please check the path '{ResourcesPath.FolderTooltip + _attributeTypeProperty.enumNames[_attributeTypeProperty.intValue]}'");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
