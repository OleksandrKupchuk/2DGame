using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(TestA))]
public class TestAPropertyDrawer : PropertyDrawer {
    private float _height = EditorGUIUtility.singleLineHeight * 3;
    private Dictionary<string, ReorderableList> reorderableLists = new();
    private Dictionary<string, bool> _foldoutStates = new Dictionary<string, bool>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        bool _foldout = GetFoldoutState(property);

        _foldout = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), _foldout, label);
        SetFoldoutState(property, _foldout);

        if (_foldout) {
            ReorderableList _reorderableList = GetReorderableList(property);

            EditorGUI.BeginProperty(position, label, property);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            _reorderableList.DoList(position);

            EditorGUI.EndProperty();
        }
    }

    private ReorderableList GetReorderableList(SerializedProperty property) {
        string propertyPath = property.propertyPath;

        if (reorderableLists.ContainsKey(propertyPath)) {
            return reorderableLists[propertyPath];
        }

        SerializedProperty namesProperty = property.FindPropertyRelative("names");

        ReorderableList newList = new ReorderableList(property.serializedObject, namesProperty, true, true, true, true);

        newList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, namesProperty.displayName);
        };

        newList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            SerializedProperty element = namesProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, _height), element, GUIContent.none);
        };

        newList.elementHeightCallback = (int index) => {
            return _height;
        };

        reorderableLists[propertyPath] = newList;
        return newList;
    }

    private bool GetFoldoutState(SerializedProperty property) {
        string propertyPath = property.propertyPath;

        if (!_foldoutStates.ContainsKey(propertyPath)) {
            _foldoutStates[propertyPath] = true;
        }

        return _foldoutStates[propertyPath];
    }

    private void SetFoldoutState(SerializedProperty property, bool state) {
        string propertyPath = property.propertyPath;
        _foldoutStates[propertyPath] = state;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        bool foldout = GetFoldoutState(property);

        if (foldout) {
            return GetReorderableList(property).GetHeight() + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
        else {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
