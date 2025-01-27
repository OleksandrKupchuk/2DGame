using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 15f;
    private SerializedProperty _isNeedNpcWordsProperty;
    private SerializedProperty _isNeedQuestProperty;

    private float _foldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
    private float _isNeedNpcWordsHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsHeight;
    private float _isNeedQuestHeight = EditorGUIUtility.singleLineHeight;
    private float _questHeight;

    private float _playerWordsPositionY;
    private float _isNeedNpcWordsPositionY;
    private float _isNpcWordsPositionY;
    private float _isNeedQuestPositionY;
    private float _questPositionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        Rect _foldoutPosition = new Rect(PADDING_LEFT, 0, position.size.x, _foldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");

        DrawPlayerWordsField(position, property);
        DrawIsNeedNpcWordsField(position, property);
        DrawNpcWordsField(position, property);
        DrawIsNeedQuestField(position, property);
        DrawQuestField(position, property);

        EditorGUI.EndProperty();
    }

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _playerWordsProperty = property.FindPropertyRelative("_playerWords");

        _playerWordsPositionY = position.y + _foldoutHeight;

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
        SerializedProperty _npcWordsProperty = property.FindPropertyRelative("_npcWords");

        if (_isNeedNpcWordsProperty.boolValue) {
            _isNpcWordsPositionY = _isNeedNpcWordsPositionY + _isNeedNpcWordsHeight;

            float _npcWordsLabelWidth = EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;
            Rect _npcWordsLabelPosition = new Rect(position.x, _isNpcWordsPositionY, _npcWordsLabelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_npcWordsLabelPosition, "Npc Words");

            _npcWordsHeight = EditorGUIUtility.singleLineHeight * 4;
            Rect _npcWordsPosition = new Rect(position.x + _npcWordsLabelWidth, _isNpcWordsPositionY, position.width - _npcWordsLabelWidth, _npcWordsHeight);
            EditorGUI.BeginChangeCheck();
            string _input = EditorGUI.TextArea(_npcWordsPosition, _npcWordsProperty.stringValue);

            if (EditorGUI.EndChangeCheck()) {
                _npcWordsProperty.stringValue = _input;
            }
        }
        else {
            _npcWordsHeight = 0;
            _isNpcWordsPositionY = _isNeedNpcWordsPositionY;
            _npcWordsProperty.stringValue = string.Empty;
        }
    }

    private void DrawIsNeedQuestField(Rect position, SerializedProperty property) {
        if (_isNeedNpcWordsProperty.boolValue) {
            _isNeedQuestPositionY = _isNpcWordsPositionY + _npcWordsHeight;
        }
        else {
            _isNeedQuestPositionY = _isNpcWordsPositionY + _isNeedNpcWordsHeight;
        }

        Rect _isNeedQuestPosition = new Rect(position.x, _isNeedQuestPositionY, position.width, _isNeedQuestHeight);
        EditorGUI.PropertyField(_isNeedQuestPosition, _isNeedQuestProperty);
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
            _questHeight = 0;
            _questPositionY = _isNeedQuestPositionY;
            _questProperty.objectReferenceValue = null;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");

        float _height = 0;

        if (property.isExpanded) {
            _height += _foldoutHeight + _playerWordsHeight + _isNeedNpcWordsHeight + _npcWordsHeight + _isNeedQuestHeight + _questHeight;
        }
        else {
            _height = _foldoutHeight;
        }

        return _height + EditorGUIUtility.standardVerticalSpacing;
    }
}
