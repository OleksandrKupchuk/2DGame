using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    SerializedProperty _titleProperty;
    SerializedProperty _isNeedParagraphsProperty;
    SerializedProperty _paragraphsProperty;
    SerializedProperty _isHaveConditionToUnlockProperty;
    SerializedProperty _conditionsProperty;
    SerializedProperty _isNeedDialogActionProperty;
    SerializedProperty _dialogActionsProperty;

    private float _foldoutHeight;
    private float _titleHeight;
    private float _isNeedParagraphsHeight;
    private float _paragraphsHeight;
    private float _isHaveConditionToUnlockHeight;
    private float _isNeedDialogActionsHeight;

    private float _paragraphsPositionY;
    private float _isHaveConditionToUnlockPositionY;
    private float _isNeedDialogActionsPositionY;

    private const float PADDING_LEFT = 15f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        _foldoutHeight = EditorGUIUtility.singleLineHeight;
        Rect _foldoutPosition = new Rect(PADDING_LEFT, 0, position.size.x, _foldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        EditorGUI.indentLevel++;
        EditorGUILayout.BeginVertical();

        _titleProperty = property.FindPropertyRelative("_title");
        _isNeedParagraphsProperty = property.FindPropertyRelative("_isNeedParagraphs");
        _paragraphsProperty = property.FindPropertyRelative("_paragraphs");
        _isHaveConditionToUnlockProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        _conditionsProperty = property.FindPropertyRelative("_conditions");
        _isNeedDialogActionProperty = property.FindPropertyRelative("_isNeedDialogActions");
        _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        _titleHeight = EditorGUIUtility.singleLineHeight * 2;
        float _titlePositionY = position.y + _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _titlePosition = new Rect(position.x, _titlePositionY, position.width, _titleHeight);
        EditorGUI.PropertyField(_titlePosition, _titleProperty);

        _isNeedParagraphsHeight = EditorGUIUtility.singleLineHeight;
        float _isNeedParagraphsPositionY = _titlePositionY + _titleHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _isNeedParagraphsPosition = new Rect(position.x, _isNeedParagraphsPositionY, position.width, _isNeedParagraphsHeight);
        EditorGUI.PropertyField(_isNeedParagraphsPosition, _isNeedParagraphsProperty);

        if(_isNeedParagraphsProperty.boolValue) {
            _paragraphsHeight = EditorGUIUtility.singleLineHeight;
            _paragraphsPositionY = _isNeedParagraphsPositionY + _isNeedParagraphsHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _paragraphsPosition = new Rect(position.x + PADDING_LEFT, _paragraphsPositionY, position.width - PADDING_LEFT, _paragraphsHeight);
            EditorGUI.PropertyField(_paragraphsPosition, _paragraphsProperty);

            _isHaveConditionToUnlockPositionY = _paragraphsPositionY + EditorGUI.GetPropertyHeight(_paragraphsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }
        else {
            _isHaveConditionToUnlockPositionY = _isNeedParagraphsPositionY + _isNeedParagraphsHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        _isHaveConditionToUnlockHeight = EditorGUIUtility.singleLineHeight;
        Rect _isHaveConditionToUnlockPosition = new Rect(position.x, _isHaveConditionToUnlockPositionY, position.width, _isHaveConditionToUnlockHeight);
        EditorGUI.PropertyField(_isHaveConditionToUnlockPosition, _isHaveConditionToUnlockProperty);

        if (_isHaveConditionToUnlockProperty.boolValue) {
            float _conditionsHeight = EditorGUIUtility.singleLineHeight;
            float _conditionsPositionY = _isHaveConditionToUnlockPositionY + _isHaveConditionToUnlockHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _conditionsPosition = new Rect(position.x + PADDING_LEFT, _conditionsPositionY, position.width - PADDING_LEFT, _conditionsHeight);
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
            Rect _dialogActionPosition = new Rect(position.x + PADDING_LEFT, _dialogActionPositionY, position.width - PADDING_LEFT, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(_dialogActionPosition, _dialogActionsProperty);
        }


        EditorGUILayout.EndVertical();
        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        if (!property.isExpanded) {
            return _foldoutHeight;
        }

        _isNeedParagraphsProperty = property.FindPropertyRelative("_isNeedParagraphs");
        _paragraphsProperty = property.FindPropertyRelative("_paragraphs");
        _isHaveConditionToUnlockProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        _conditionsProperty = property.FindPropertyRelative("_conditions");
        _isNeedDialogActionProperty = property.FindPropertyRelative("_isNeedDialogActions");
        _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        float height = _foldoutHeight + _titleHeight + _isNeedParagraphsHeight + _isHaveConditionToUnlockHeight + _isNeedDialogActionsHeight;

        if (_isNeedParagraphsProperty.boolValue) {
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