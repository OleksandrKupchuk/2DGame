using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 15f;
    private SerializedProperty _isNeedNpcWordsProperty;
    private SerializedProperty _isNeedQuestProperty;
    private SerializedProperty _isHaveConditionToUnlockDialogProperty;
    private SerializedProperty _isNeedDialogActionsProperty;

    private ReorderableList _npcWordsList;

    private float _foldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
    private float _isNeedNpcWordsHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsHeight;
    private float _isNeedQuestHeight = EditorGUIUtility.singleLineHeight;
    private float _questHeight;
    private float _paragraphHeigh = EditorGUIUtility.singleLineHeight * 3;
    private float _isHaveConditionToUnlockDialogHeigh = EditorGUIUtility.singleLineHeight;
    private float _conditionsHeigh;
    private float _isNeedDialogActionsHeigh = EditorGUIUtility.singleLineHeight;
    private float _dialogActionsHeigh;

    private float _isHaveConditionToUnlockDialogPositionY;
    private float _conditionsPositionY;
    private float _playerWordsPositionY;
    private float _isNeedNpcWordsPositionY;
    private float _isNpcWordsPositionY;
    private float _isNeedQuestPositionY;
    private float _questPositionY;
    private float _isNeedDialogActionsPositionY;
    private float _dialogActionsPositionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        CreateNpcWordsList(property);

        EditorGUI.BeginProperty(position, label, property);

        Rect _foldoutPosition = new Rect(PADDING_LEFT, 0, position.size.x, _foldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _isHaveConditionToUnlockDialogProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        _isNeedDialogActionsProperty = property.FindPropertyRelative("_isNeedDialogActions");

        DrawIsHaveConditionToUnlockDialogField(position, property);
        DrawConditionsField(position, property);
        DrawPlayerWordsField(position, property);
        DrawIsNeedNpcWordsField(position, property);
        DrawNpcWordsField(position, property);
        DrawIsNeedQuestField(position, property);
        DrawQuestField(position, property);
        DrawIsNeedDialogActionsField(position, property);
        DrawDialogActionsField(position, property);

        EditorGUI.EndProperty();
    }

    private void CreateNpcWordsList(SerializedProperty property) {
        if (_npcWordsList != null) {
            return;
        }

        SerializedProperty _npcWordsProperty = property.FindPropertyRelative("_npcWords");

        _npcWordsList = new ReorderableList(_npcWordsProperty.serializedObject, _npcWordsProperty) {
            displayAdd = true,
            displayRemove = true,
            draggable = true,

            drawHeaderCallback = rect => EditorGUI.LabelField(rect, _npcWordsProperty.displayName),

            drawElementCallback = (rect, index, focused, active) => {
                var _element = _npcWordsProperty.GetArrayElementAtIndex(index);

                float _labelWidth = EditorGUIUtility.labelWidth;
                Rect _labelPosition = new Rect(rect.x, rect.y, _labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(_labelPosition, $"Sentence {index + 1}");

                EditorGUI.BeginChangeCheck();
                string _input = EditorGUI.TextArea(new Rect(rect.x + _labelWidth, rect.y, rect.width - _labelWidth, _paragraphHeigh), _element.stringValue);

                if (EditorGUI.EndChangeCheck()) {
                    _element.stringValue = _input;
                }
            },

            elementHeightCallback = index => {
                return _paragraphHeigh;
            },
        };
    }

    private void DrawIsHaveConditionToUnlockDialogField(Rect position, SerializedProperty property) {
        _isHaveConditionToUnlockDialogPositionY = position.y + _foldoutHeight;
        Rect _position = new Rect(position.x, _isHaveConditionToUnlockDialogPositionY, position.width, _isHaveConditionToUnlockDialogHeigh);
        EditorGUI.PropertyField(_position, _isHaveConditionToUnlockDialogProperty);
    }

    private void DrawConditionsField(Rect position, SerializedProperty property) {
        SerializedProperty _conditionsProperty = property.FindPropertyRelative("_conditions");

        if (_isHaveConditionToUnlockDialogProperty.boolValue) {
            _conditionsHeigh = EditorGUI.GetPropertyHeight(_conditionsProperty);
            _conditionsPositionY = _isHaveConditionToUnlockDialogPositionY + _isHaveConditionToUnlockDialogHeigh + EditorGUIUtility.standardVerticalSpacing;
            Rect _position = new Rect(position.x, _conditionsPositionY, position.width, _conditionsHeigh);
            EditorGUI.PropertyField(_position, _conditionsProperty);
        }
        else {
            _conditionsHeigh = EditorGUIUtility.singleLineHeight;
            _conditionsPositionY = _isHaveConditionToUnlockDialogPositionY + EditorGUIUtility.standardVerticalSpacing;
            _conditionsProperty.ClearArray();
        }
    }

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _playerWordsProperty = property.FindPropertyRelative("_playerWords");

        _playerWordsPositionY = _conditionsPositionY + _conditionsHeigh + EditorGUIUtility.standardVerticalSpacing;

        float _playerWordsLabelWidth = EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;
        Rect _playerWordsLabelPosition = new Rect(position.x, _playerWordsPositionY, _playerWordsLabelWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(_playerWordsLabelPosition, "Player Words");

        Rect _playerWordsPosition = new Rect(position.x + _playerWordsLabelWidth, _playerWordsPositionY, position.width - _playerWordsLabelWidth, _playerWordsHeight);
        EditorGUI.BeginChangeCheck();
        string _input = EditorGUI.TextArea(_playerWordsPosition, _playerWordsProperty.stringValue);

        if (EditorGUI.EndChangeCheck()) {
            _playerWordsProperty.stringValue = _input;
        }
    }

    private void DrawIsNeedNpcWordsField(Rect position, SerializedProperty property) {
        _isNeedNpcWordsPositionY = _playerWordsPositionY + _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _isNeedNpcWordsPositionY, position.width, _isNeedNpcWordsHeight);
        EditorGUI.PropertyField(_position, _isNeedNpcWordsProperty);
    }

    private void DrawNpcWordsField(Rect position, SerializedProperty property) {
        if (_isNeedNpcWordsProperty.boolValue) {
            _npcWordsHeight = _npcWordsList.GetHeight();
            _isNpcWordsPositionY = _isNeedNpcWordsPositionY + _isNeedNpcWordsHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _position = new Rect(position.x, _isNpcWordsPositionY, position.width, _npcWordsHeight);
            _npcWordsList.DoList(_position);
        }
        else {
            //_npcWordsHeight = 0;
            _npcWordsHeight = EditorGUIUtility.singleLineHeight;
            _isNpcWordsPositionY = _isNeedNpcWordsPositionY;
            _npcWordsList.serializedProperty.ClearArray();
        }
    }

    private void DrawIsNeedQuestField(Rect position, SerializedProperty property) {
        //if (_isNeedNpcWordsProperty.boolValue) {
        //    _isNeedQuestPositionY = _isNpcWordsPositionY + _npcWordsHeight;
        //}
        //else {
        //    _isNeedQuestPositionY = _isNpcWordsPositionY + _isNeedNpcWordsHeight;
        //}

        _isNeedQuestPositionY = _isNpcWordsPositionY + _npcWordsHeight;
        Rect _position = new Rect(position.x, _isNeedQuestPositionY, position.width, _isNeedQuestHeight);
        EditorGUI.PropertyField(_position, _isNeedQuestProperty);
    }

    private void DrawQuestField(Rect position, SerializedProperty property) {
        SerializedProperty _questProperty = property.FindPropertyRelative("_quest");

        if (_isNeedQuestProperty.boolValue) {
            _questHeight = EditorGUIUtility.singleLineHeight;
            _questPositionY = _isNeedQuestPositionY + _isNeedQuestHeight;
            Rect _questPosition = new Rect(position.x, _questPositionY, position.width, _questHeight);
            EditorGUI.PropertyField(_questPosition, _questProperty);
        }
        else {
            //_questHeight = 0;
            _questHeight = EditorGUIUtility.singleLineHeight;
            _questPositionY = _isNeedQuestPositionY;
            _questProperty.objectReferenceValue = null;
        }
    }

    private void DrawIsNeedDialogActionsField(Rect position, SerializedProperty property) {
        _isNeedDialogActionsPositionY = _questPositionY + _questHeight;
        Rect _position = new Rect(position.x, _isNeedDialogActionsPositionY, position.width, _isNeedDialogActionsHeigh);
        EditorGUI.PropertyField(_position, _isNeedDialogActionsProperty);
    }

    private void DrawDialogActionsField(Rect position, SerializedProperty property) {
        SerializedProperty _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        if (_isNeedDialogActionsProperty.boolValue) {
            _dialogActionsHeigh = EditorGUI.GetPropertyHeight(_dialogActionsProperty);
            _dialogActionsPositionY = _isNeedDialogActionsPositionY + _isNeedDialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;
            Rect _position = new Rect(position.x, _dialogActionsPositionY, position.width, _conditionsHeigh);
            EditorGUI.PropertyField(_position, _dialogActionsProperty);
        }
        else {
            _dialogActionsHeigh = EditorGUIUtility.singleLineHeight;
            _dialogActionsPositionY = _isNeedDialogActionsPositionY + EditorGUIUtility.standardVerticalSpacing;
            _dialogActionsProperty.ClearArray();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _isHaveConditionToUnlockDialogProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        _isNeedDialogActionsProperty = property.FindPropertyRelative("_isNeedDialogActions");

        float _height = 0;

        if (!property.isExpanded) {
            return _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _foldoutHeight;

        if (_isHaveConditionToUnlockDialogProperty.boolValue) {
            _height += _isHaveConditionToUnlockDialogHeigh + _conditionsHeigh + (EditorGUIUtility.standardVerticalSpacing * 2);
        }
        else {
            _height += _isHaveConditionToUnlockDialogHeigh + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _playerWordsHeight;

        if (_isNeedNpcWordsProperty.boolValue) {
            _height += _isNeedNpcWordsHeight + _npcWordsHeight + (EditorGUIUtility.standardVerticalSpacing * 2);
        }
        else {
            _height += _isNeedNpcWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        if (_isNeedQuestProperty.boolValue) {
            _height += _isNeedQuestHeight + _questHeight + (EditorGUIUtility.standardVerticalSpacing * 2);
        }
        else {
            _height += _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        if (_isNeedDialogActionsProperty.boolValue) {
            _height += _isNeedDialogActionsHeigh + _dialogActionsHeigh + (EditorGUIUtility.standardVerticalSpacing * 2);
        }
        else {
            _height += _isNeedDialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;
        }

        return _height + EditorGUIUtility.standardVerticalSpacing;
    }
}
