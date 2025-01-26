using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 15f;

    private ReorderableList _paragraphsList;
    private float _paragraphHeigh = EditorGUIUtility.singleLineHeight * 3;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        CreateCustomParagraphList(property);

        EditorGUI.BeginProperty(position, label, property);

        float _foldoutHeight = EditorGUIUtility.singleLineHeight;
        Rect _foldoutPosition = new Rect(PADDING_LEFT, 0, position.size.x, _foldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        SerializedProperty _titleProperty = property.FindPropertyRelative("_title");
        SerializedProperty _isNeedParagraphsProperty = property.FindPropertyRelative("_isNeedParagraphs");
        SerializedProperty _paragraphsProperty = property.FindPropertyRelative("_paragraphs");
        SerializedProperty _isHaveConditionToUnlockProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        SerializedProperty _conditionsProperty = property.FindPropertyRelative("_conditions");
        SerializedProperty _isNeedDialogActionProperty = property.FindPropertyRelative("_isNeedDialogActions");
        SerializedProperty _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        EditorGUI.indentLevel++;

        float _titleHeight = EditorGUIUtility.singleLineHeight * 2;
        EditorGUILayout.PropertyField(_titleProperty, new GUIContent("Title"), GUILayout.Height(_titleHeight));
        EditorGUILayout.PropertyField(_isNeedParagraphsProperty, new GUIContent("Is Need Paragraphs"));

        if (_isNeedParagraphsProperty.boolValue) {
            //EditorGUILayout.PropertyField(_paragraphsProperty, new GUIContent("Paragraphs"));
            GUILayout.BeginHorizontal();
            _paragraphsList.DoLayoutList();
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.PropertyField(_isHaveConditionToUnlockProperty, new GUIContent("Is Have Condition To Unlock"));

        if (_isHaveConditionToUnlockProperty.boolValue) {
            EditorGUILayout.PropertyField(_conditionsProperty, new GUIContent("Conditions"));
        }
        else {
            _conditionsProperty.ClearArray();
        }

        EditorGUILayout.PropertyField(_isNeedDialogActionProperty, new GUIContent("Is Need DialogController Action"));

        if (_isNeedDialogActionProperty.boolValue) {
            EditorGUILayout.PropertyField(_dialogActionsProperty, new GUIContent("DialogController Actions"));
        }

        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    private void CreateCustomParagraphList(SerializedProperty property) {
        if (_paragraphsList != null) {
            return;
        }

        SerializedProperty _paragraphsProperty = property.FindPropertyRelative("_paragraphs");

        _paragraphsList = new ReorderableList(_paragraphsProperty.serializedObject, _paragraphsProperty) {
            displayAdd = true,
            displayRemove = true,
            draggable = true,

            drawHeaderCallback = rect => EditorGUI.LabelField(rect, _paragraphsProperty.displayName),

            drawElementCallback = (rect, index, focused, active) => {
                var element = _paragraphsProperty.GetArrayElementAtIndex(index);

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, _paragraphHeigh), element);
            },

            elementHeightCallback = index => {
                var height = _paragraphHeigh;

                return height;
            },
        };
    }
}
