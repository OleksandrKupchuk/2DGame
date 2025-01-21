using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private float _titleHeight;
    private float _paragraphsHeight;
    private float _isHaveConditionToUnlockHeight;
    private float _isNeedDialogActionsHeight;
    private float _isNeedDialogActionsPositionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        SerializedProperty _titleProperty = property.FindPropertyRelative("_title");
        SerializedProperty _paragraphsProperty = property.FindPropertyRelative("_paragraphs");
        SerializedProperty _isHaveConditionToUnlockProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        SerializedProperty _conditionsProperty = property.FindPropertyRelative("_conditions");
        SerializedProperty _isNeedDialogActionProperty = property.FindPropertyRelative("_isNeedDialogActions");
        SerializedProperty _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        _titleHeight = EditorGUIUtility.singleLineHeight * 2;
        Rect _titlePosition = new Rect(position.x, position.y, position.width, _titleHeight);
        EditorGUI.PropertyField(_titlePosition, _titleProperty);

        _paragraphsHeight = EditorGUIUtility.singleLineHeight;
        float _paragraphsPositionY = position.y + _titleHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _paragraphsPosition = new Rect(position.x, _paragraphsPositionY, position.width, _paragraphsHeight);
        EditorGUI.PropertyField(_paragraphsPosition, _paragraphsProperty);

        _isHaveConditionToUnlockHeight = EditorGUIUtility.singleLineHeight;
        float _isHaveConditionToUnlockPositionY = _paragraphsPositionY + EditorGUI.GetPropertyHeight(_paragraphsProperty) + EditorGUIUtility.standardVerticalSpacing;
        Rect _isHaveConditionToUnlockPosition = new Rect(position.x, _isHaveConditionToUnlockPositionY, position.width, _isHaveConditionToUnlockHeight);
        EditorGUI.PropertyField(_isHaveConditionToUnlockPosition, _isHaveConditionToUnlockProperty);

        if (_isHaveConditionToUnlockProperty.boolValue) {
            float _conditionsHeight = EditorGUIUtility.singleLineHeight;
            float _conditionsPositionY = _isHaveConditionToUnlockPositionY + _isHaveConditionToUnlockHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _conditionsPosition = new Rect(position.x, _conditionsPositionY, position.width, _conditionsHeight);
            EditorGUI.PropertyField(_conditionsPosition, _conditionsProperty);

            _isNeedDialogActionsPositionY = _isHaveConditionToUnlockPositionY + _isHaveConditionToUnlockHeight + EditorGUI.GetPropertyHeight(_conditionsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }
        else {
            _isNeedDialogActionsPositionY = _isHaveConditionToUnlockPositionY + _isHaveConditionToUnlockHeight + EditorGUIUtility.standardVerticalSpacing;
        }
        
        _isNeedDialogActionsHeight = EditorGUIUtility.singleLineHeight;
        Rect _isNeedDialogActionsPosition = new Rect(position.x, _isNeedDialogActionsPositionY, position.width, _isNeedDialogActionsHeight);
        EditorGUI.PropertyField(_isNeedDialogActionsPosition, _isNeedDialogActionProperty);

        if (_isNeedDialogActionProperty.boolValue) {
            float _dialogActionPositionY = _isNeedDialogActionsPositionY + _isNeedDialogActionsHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _dialogActionPosition = new Rect(position.x, _dialogActionPositionY, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(_dialogActionPosition, _dialogActionsProperty);
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        SerializedProperty _paragraphsProperty = property.FindPropertyRelative("_paragraphs");
        SerializedProperty _isHaveConditionToUnlockProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        SerializedProperty _conditionsProperty = property.FindPropertyRelative("_conditions");
        SerializedProperty _isNeedDialogActionProperty = property.FindPropertyRelative("_isNeedDialogActions");
        SerializedProperty _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        float height = _titleHeight + _paragraphsHeight + _isHaveConditionToUnlockHeight + _isNeedDialogActionsHeight;

        if (_paragraphsProperty.isExpanded) {
            height += EditorGUI.GetPropertyHeight(_paragraphsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }

        if (_isHaveConditionToUnlockProperty.boolValue) {
            height += EditorGUI.GetPropertyHeight(_conditionsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }

        if (_isNeedDialogActionProperty.boolValue) {
            height += EditorGUI.GetPropertyHeight(_dialogActionsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height + EditorGUIUtility.singleLineHeight;
    }
}