using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 15f;

    private ReorderableList _npcWordsList;

    private float _foldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
    private float _isNeedNpcWordsHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsFoldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsListHeight;

    private float _paragraphHeigh = EditorGUIUtility.singleLineHeight * 3;

    private bool _isNpcWordsFoldout = false;

    private float _playerWordsPositionY;
    private float _isNeedNpcWordsPositionY;
    private float _isNpcWordsPositionY;

    private Dictionary<string, ReorderableList> _reorderableLists = new Dictionary<string, ReorderableList>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        CreateCustomNpcWordsList(property);

        EditorGUI.BeginProperty(position, label, property);

        Rect _foldoutPosition = new Rect(PADDING_LEFT, 0, position.size.x, _foldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        SerializedProperty _npcWordsProperty = property.FindPropertyRelative("_npcWords");
        SerializedProperty _isNeedQuestsProperty = property.FindPropertyRelative("_isNeedQuests");
        SerializedProperty _questProperty = property.FindPropertyRelative("_quest");
        SerializedProperty _nextDialogsProperty = property.FindPropertyRelative("_nextDialogs");
        SerializedProperty _isHaveConditionToUnlockProperty = property.FindPropertyRelative("_isHaveConditionToUnlockDialog");
        SerializedProperty _conditionsProperty = property.FindPropertyRelative("_conditions");
        SerializedProperty _isNeedDialogActionProperty = property.FindPropertyRelative("_isNeedDialogActions");
        SerializedProperty _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        DrawPlayerWordsField(position, property);
        DrawIsNeedNpcWordsField(position, property);
        DrawNpcWordsField(position, property);

        EditorGUI.EndProperty();
    }

    private void CreateCustomNpcWordsList(SerializedProperty property) {
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

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _playerWordsProperty = property.FindPropertyRelative("_playerWords");

        _playerWordsPositionY = position.y + _foldoutHeight;

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

    private void DrawIsNeedNpcWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");

        _isNeedNpcWordsPositionY = _playerWordsPositionY + _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _isNeedNpcWordsPositionY, position.width, _isNeedNpcWordsHeight);
        EditorGUI.PropertyField(_position, _isNeedNpcWordsProperty);
    }

    private void DrawNpcWordsField(Rect position, SerializedProperty property) {
        float _foldoutPositionY = _isNeedNpcWordsPositionY + _isNeedNpcWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _foldoutPosition = new Rect(position.x + PADDING_LEFT, _foldoutPositionY, position.width - PADDING_LEFT, _npcWordsFoldoutHeight);
        _isNpcWordsFoldout = EditorGUI.Foldout(_foldoutPosition, _isNpcWordsFoldout, "Npc Words", true);

        if (_isNpcWordsFoldout) {
            _npcWordsListHeight = _npcWordsList.GetHeight();
            _isNpcWordsPositionY = _foldoutPositionY + _npcWordsFoldoutHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _listPosition = new Rect(position.x, _isNpcWordsPositionY, position.width, _npcWordsList.GetHeight());
            _npcWordsList.DoList(_listPosition);
        }
        else {
            _npcWordsListHeight = _npcWordsFoldoutHeight;
            _isNpcWordsPositionY = _foldoutPositionY + EditorGUIUtility.standardVerticalSpacing;
        }
    }

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    //    SerializedProperty _npcWordsProperty = property.FindPropertyRelative("_npcWords");

    //    float _height = 0;

    //    if (property.isExpanded) {
    //        _height += _foldoutHeight + _playerWordsHeight + _isNeedNpcWordsHeight + _npcWordsListHeight;
    //    }
    //    else {
    //        _height = _foldoutHeight;
    //    }

    //    return _height + EditorGUIUtility.standardVerticalSpacing + 100;
    //}
}
