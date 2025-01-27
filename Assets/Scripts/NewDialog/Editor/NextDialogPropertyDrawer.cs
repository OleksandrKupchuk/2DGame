using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NextDialog))]
public class NextDialogPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 12f;

    private SerializedProperty _npcWordsProperty;
    private SerializedProperty _isNeedQuestProperty;

    private bool _isNpcWordsFoldout = false;

    private float _nextDialogFoldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
    private float _npcWordsListHight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsfoldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _isNeedQuestHeight = EditorGUIUtility.singleLineHeight;
    private float _questPropertyHeight = EditorGUIUtility.singleLineHeight;

    private float _foldoutPositionY;
    private float _playerWordsPositionY;
    private float _npcWordsListPositionY;
    private float _isNeedQuestPositionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");

        _foldoutPositionY = position.y;
        Rect _foldoutPosition = new Rect(position.x, _foldoutPositionY, position.size.x, _nextDialogFoldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        EditorGUI.indentLevel++;

        DrawPlayerWordsField(position, property);
        DrawNpcWordsField(position, property, label);
        DrawIsNeedQuestField(position, property);
        DrawQuestField(position, property);

        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _playerWordsProperty = property.FindPropertyRelative("_playerWords");
        _playerWordsPositionY = _foldoutPositionY + _nextDialogFoldoutHeight + EditorGUIUtility.standardVerticalSpacing;

        float _playerWordsLabelWidth = EditorGUIUtility.labelWidth;
        Rect _playerWordsLabelPosition = new Rect(position.x, _playerWordsPositionY, _playerWordsLabelWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(_playerWordsLabelPosition, "Player Words");

        Rect _playerWordsPosition = new Rect(position.x + _playerWordsLabelWidth, _playerWordsPositionY, position.width - _playerWordsLabelWidth, _playerWordsHeight);
        EditorGUI.BeginChangeCheck();
        string _input = EditorGUI.TextArea(_playerWordsPosition, _playerWordsProperty.stringValue);

        if (EditorGUI.EndChangeCheck()) {
            _playerWordsProperty.stringValue = _input;
        }
    }

    private void DrawNpcWordsField(Rect position, SerializedProperty property, GUIContent label) {
        _npcWordsProperty = property.FindPropertyRelative("_npcWords");
        _npcWordsListPositionY = _playerWordsPositionY + _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _npcWordsListPosition = new Rect(position.x + PADDING_LEFT, _npcWordsListPositionY, position.width - PADDING_LEFT, _npcWordsListHight);
        EditorGUI.PropertyField(_npcWordsListPosition, _npcWordsProperty);
    }

    private void DrawIsNeedQuestField(Rect position, SerializedProperty property) {
        SerializedProperty _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _isNeedQuestPositionY = _npcWordsListPositionY + EditorGUI.GetPropertyHeight(_npcWordsProperty) + EditorGUIUtility.standardVerticalSpacing;
        Rect _isNeedQuestPosition = new Rect(position.x, _isNeedQuestPositionY, position.size.x, _nextDialogFoldoutHeight);
        EditorGUI.PropertyField(_isNeedQuestPosition, _isNeedQuestProperty);
    }

    private void DrawQuestField(Rect position, SerializedProperty property) {
        SerializedProperty _questProperty = property.FindPropertyRelative("_quest");

        if (_isNeedQuestProperty.boolValue) {
            float _questPropertyPositionY = _isNeedQuestPositionY + _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _questPropertyPosition = new Rect(position.x, _questPropertyPositionY, position.size.x, _nextDialogFoldoutHeight);
            EditorGUI.PropertyField(_questPropertyPosition, _questProperty);
        }
        else {
            _questProperty.objectReferenceValue = null;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _npcWordsProperty = property.FindPropertyRelative("_npcWords");

        if (!property.isExpanded) {
            return _nextDialogFoldoutHeight;
        }

        float _height = 0;

        if(_isNpcWordsFoldout) {
            _height += _npcWordsfoldoutHeight;
        }

        if (_isNeedQuestProperty.boolValue) {
            _height += _nextDialogFoldoutHeight + _playerWordsHeight + EditorGUI.GetPropertyHeight(_npcWordsProperty) + _isNeedQuestHeight + _questPropertyHeight + EditorGUIUtility.standardVerticalSpacing;
        }
        else {
            _height += _nextDialogFoldoutHeight + _playerWordsHeight + EditorGUI.GetPropertyHeight(_npcWordsProperty) + _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        return _height + EditorGUIUtility.singleLineHeight;
    }
}
