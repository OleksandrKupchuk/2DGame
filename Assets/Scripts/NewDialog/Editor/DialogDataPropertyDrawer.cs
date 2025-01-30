using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 15f;
    private SerializedProperty _isHaveConditionToUnlockDialogProperty;
    private SerializedProperty _conditionsProperty;
    private SerializedProperty _isNeedNpcWordsProperty;
    private SerializedProperty _isNeedQuestProperty;
    private SerializedProperty _isNeedDialogActionsProperty;
    private SerializedProperty _dialogActionsProperty;

    private Dictionary<string, ReorderableList> _npcWordsLists = new();
    private Dictionary<string, bool> _npcWordsFoldoutStates = new();
    private Dictionary<string, ReorderableList> _npcWordsAfterQuestDoneLists = new();
    private Dictionary<string, bool> _npcWordsAfterQuestDoneFoldoutStates = new();

    private float _paragraphHeigh = EditorGUIUtility.singleLineHeight * 3;
    private float _foldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _isHaveConditionsToUnlockDialogHeigh = EditorGUIUtility.singleLineHeight;
    private float _conditionsToUnlockDialogHeigh;
    private float _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
    private float _isNeedNpcWordsHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsHeight;
    private float _isNeedQuestHeight = EditorGUIUtility.singleLineHeight;
    private float _questHeight;
    private float _isNeedDialogActionsHeigh = EditorGUIUtility.singleLineHeight;

    private float _positionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        ReorderableList _npcWordsList = GetReorderableList(property, "_npcWords", _npcWordsLists);
        ReorderableList _npcWordsAfterQuestDoneList = GetReorderableList(property, "_npcWordsAfterQuestComplete", _npcWordsAfterQuestDoneLists);

        EditorGUI.BeginProperty(position, label, property);

        Rect _foldoutPosition = new Rect(PADDING_LEFT, 0, position.size.x, _foldoutHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        _isHaveConditionToUnlockDialogProperty = property.FindPropertyRelative("_isHaveConditionsToUnlockDialog");
        _conditionsProperty = property.FindPropertyRelative("_conditions");
        _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _isNeedDialogActionsProperty = property.FindPropertyRelative("_isNeedDialogActions");

        DrawIsHaveConditionToUnlockDialogField(position, property);
        DrawConditionsField(position, property);
        DrawPlayerWordsField(position, property);
        DrawIsNeedNpcWordsField(position);
        DrawNpcWordsField(position, property, _npcWordsList);
        DrawIsNeedQuestField(position, property, _npcWordsList.GetHeight());
        DrawQuestField(position, property);
        DrawPlayerWordsAfterQuestDoneField(position, property);
        DrawNpcWordsAfterQuestDoneField(position, property, _npcWordsAfterQuestDoneList);
        DrawIsNeedDialogActionsField(position, property, _npcWordsAfterQuestDoneList.GetHeight());
        DrawDialogActionsField(position, property);

        EditorGUI.EndProperty();
    }

    private ReorderableList GetReorderableList(SerializedProperty property, string propertyName, Dictionary<string, ReorderableList> lists) {
        string _propertyPath = property.propertyPath;

        if (lists.ContainsKey(_propertyPath)) {
            return lists[_propertyPath];
        }

        SerializedProperty _property = property.FindPropertyRelative(propertyName);

        ReorderableList _npcWordsList = new ReorderableList(_property.serializedObject, _property) {
            displayAdd = true,
            displayRemove = true,
            draggable = true,

            drawHeaderCallback = rect => EditorGUI.LabelField(rect, _property.displayName),

            drawElementCallback = (rect, index, focused, active) => {
                var _element = _property.GetArrayElementAtIndex(index);

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

        lists[_propertyPath] = _npcWordsList;
        return _npcWordsList;
    }

    private void DrawIsHaveConditionToUnlockDialogField(Rect position, SerializedProperty property) {
        _positionY = position.y + _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _isHaveConditionsToUnlockDialogHeigh);
        EditorGUI.PropertyField(_position, _isHaveConditionToUnlockDialogProperty);
    }

    private void DrawConditionsField(Rect position, SerializedProperty property) {
        if (_isHaveConditionToUnlockDialogProperty.boolValue) {
            _positionY += _isHaveConditionsToUnlockDialogHeigh + EditorGUIUtility.standardVerticalSpacing;
            _conditionsToUnlockDialogHeigh = EditorGUI.GetPropertyHeight(_conditionsProperty);
            Rect _position = new Rect(position.x + PADDING_LEFT, _positionY, position.width - PADDING_LEFT, _conditionsToUnlockDialogHeigh);
            EditorGUI.PropertyField(_position, _conditionsProperty);
        }
        else {
            _conditionsToUnlockDialogHeigh = EditorGUIUtility.singleLineHeight;
            _conditionsProperty.ClearArray();
        }
    }

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _property = property.FindPropertyRelative("_playerWords");

        _positionY += _conditionsToUnlockDialogHeigh + EditorGUIUtility.standardVerticalSpacing;

        float _labelWidth = EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;
        Rect _labelPosition = new Rect(position.x, _positionY, _labelWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(_labelPosition, "Player Words");

        Rect _playerWordsPosition = new Rect(position.x + _labelWidth, _positionY, position.width - _labelWidth, _playerWordsHeight);
        EditorGUI.BeginChangeCheck();
        string _input = EditorGUI.TextArea(_playerWordsPosition, _property.stringValue);

        if (EditorGUI.EndChangeCheck()) {
            _property.stringValue = _input;
        }
    }

    private void DrawIsNeedNpcWordsField(Rect position) {
        _positionY += _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _isNeedNpcWordsHeight);
        EditorGUI.PropertyField(_position, _isNeedNpcWordsProperty);
    }

    private void DrawNpcWordsField(Rect position, SerializedProperty property, ReorderableList reorderableList) {
        bool _foldoutState = GetFoldoutState(property, _npcWordsFoldoutStates);

        if (_isNeedNpcWordsProperty.boolValue) {
            _positionY += _isNeedNpcWordsHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _foldoutPosition = new Rect(position.x + PADDING_LEFT, _positionY, position.size.x - PADDING_LEFT, _foldoutHeight);
            _foldoutState = EditorGUI.Foldout(_foldoutPosition, _foldoutState, "Npc Words", true);
            SetFoldoutState(property, _npcWordsFoldoutStates, _foldoutState);

            if (_foldoutState) {
                _positionY += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
                _npcWordsHeight = reorderableList.GetHeight();
                Rect _position = new Rect(position.x, _positionY, position.width, _npcWordsHeight);
                reorderableList.DoList(_position);
            }
            else {
                _npcWordsHeight = EditorGUIUtility.singleLineHeight;
            }
        }
        else {
            _npcWordsHeight = EditorGUIUtility.singleLineHeight;
            reorderableList.serializedProperty.ClearArray();
        }
    }

    private void DrawIsNeedQuestField(Rect position, SerializedProperty property, float listHeight) {
        _positionY += _npcWordsHeight + EditorGUIUtility.standardVerticalSpacing;

        Rect _position = new Rect(position.x, _positionY, position.width, _isNeedQuestHeight);
        EditorGUI.PropertyField(_position, _isNeedQuestProperty);
    }

    private void DrawQuestField(Rect position, SerializedProperty property) {
        SerializedProperty _questProperty = property.FindPropertyRelative("_quest");

        if (_isNeedQuestProperty.boolValue) {
            _questHeight = EditorGUIUtility.singleLineHeight;
            _positionY += _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _questPosition = new Rect(position.x, _positionY, position.width, _questHeight);
            EditorGUI.PropertyField(_questPosition, _questProperty);
        }
        else {
            _questHeight = EditorGUIUtility.singleLineHeight;
            _questProperty.objectReferenceValue = null;
        }
    }

    private void DrawPlayerWordsAfterQuestDoneField(Rect position, SerializedProperty property) {
        SerializedProperty _property = property.FindPropertyRelative("_playerWordsAfterQuestComplete");

        if (_isNeedQuestProperty.boolValue) {
            _positionY += _questHeight + EditorGUIUtility.standardVerticalSpacing;

            float _labelWidth = EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;
            Rect _labelPosition = new Rect(position.x, _positionY, _labelWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_labelPosition, "Player Words After Quest Complete");

            Rect _playerWordsPosition = new Rect(position.x + _labelWidth, _positionY, position.width - _labelWidth, _playerWordsHeight);
            EditorGUI.BeginChangeCheck();
            string _input = EditorGUI.TextArea(_playerWordsPosition, _property.stringValue);

            if (EditorGUI.EndChangeCheck()) {
                _property.stringValue = _input;
            }
        }
        else {
            _property.stringValue = string.Empty;
        }
    }

    private void DrawNpcWordsAfterQuestDoneField(Rect position, SerializedProperty property, ReorderableList reorderableList) {
        bool _foldoutState = GetFoldoutState(property, _npcWordsAfterQuestDoneFoldoutStates);

        if (_isNeedQuestProperty.boolValue) {
            _positionY += _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _foldoutPosition = new Rect(position.x + 12, _positionY, position.size.x, _foldoutHeight);
            _foldoutState = EditorGUI.Foldout(_foldoutPosition, _foldoutState, "Npc Words After Quest Complete", true);
            SetFoldoutState(property, _npcWordsAfterQuestDoneFoldoutStates, _foldoutState);

            if (_foldoutState) {
                _positionY += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
                Rect _position = new Rect(position.x, _positionY, position.width, reorderableList.GetHeight());
                reorderableList.DoList(_position);
            }
        }
        else {
            reorderableList.serializedProperty.ClearArray();
        }
    }

    private void DrawIsNeedDialogActionsField(Rect position, SerializedProperty property, float listHeight) {
        bool _foldoutState = GetFoldoutState(property, _npcWordsAfterQuestDoneFoldoutStates);

        if (_isNeedQuestProperty.boolValue) {
            if (_foldoutState) {
                _positionY += listHeight + EditorGUIUtility.standardVerticalSpacing;
            }
            else {
                _positionY += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }
        else {
            _positionY += _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        Rect _position = new Rect(position.x, _positionY, position.width, _isNeedDialogActionsHeigh);
        EditorGUI.PropertyField(_position, _isNeedDialogActionsProperty);
    }

    private void DrawDialogActionsField(Rect position, SerializedProperty property) {
        _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        if (_isNeedDialogActionsProperty.boolValue) {
            _positionY += _isNeedDialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;
            Rect _position = new Rect(position.x + PADDING_LEFT, _positionY, position.width - PADDING_LEFT, EditorGUI.GetPropertyHeight(_dialogActionsProperty));
            EditorGUI.PropertyField(_position, _dialogActionsProperty);
        }
        else {
            _dialogActionsProperty.ClearArray();
        }
    }

    private bool GetFoldoutState(SerializedProperty property, Dictionary<string, bool> foldoutStates) {
        if (!foldoutStates.ContainsKey(property.propertyPath)) {
            foldoutStates[property.propertyPath] = false;
        }

        return foldoutStates[property.propertyPath];
    }

    private void SetFoldoutState(SerializedProperty property, Dictionary<string, bool> foldoutStates, bool state) {
        string _propertyPath = property.propertyPath;
        foldoutStates[_propertyPath] = state;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        _isHaveConditionToUnlockDialogProperty = property.FindPropertyRelative("_isHaveConditionsToUnlockDialog");
        _conditionsProperty = property.FindPropertyRelative("_conditions");
        _isNeedNpcWordsProperty = property.FindPropertyRelative("_isNeedNpcWords");
        _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _isNeedDialogActionsProperty = property.FindPropertyRelative("_isNeedDialogActions");
        _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        float _height = 0;

        if (!property.isExpanded) {
            return _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _foldoutHeight + _isHaveConditionsToUnlockDialogHeigh + (EditorGUIUtility.standardVerticalSpacing * 2);

        if (_isHaveConditionToUnlockDialogProperty.boolValue) {
            _height += EditorGUI.GetPropertyHeight(_conditionsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _playerWordsHeight + _isNeedNpcWordsHeight + (EditorGUIUtility.standardVerticalSpacing * 2);

        if (_isNeedNpcWordsProperty.boolValue) {
            bool _foldout = GetFoldoutState(property, _npcWordsFoldoutStates);

            _height += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;

            if (_foldout) {
                _height += GetReorderableList(property, "_npcWords", _npcWordsLists).GetHeight() + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        _height += _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;

        if (_isNeedQuestProperty.boolValue) {
            _height += _questHeight + _playerWordsHeight + _foldoutHeight + (EditorGUIUtility.standardVerticalSpacing * 3);

            bool _foldoutNpcWordsAfterQuestDone = GetFoldoutState(property, _npcWordsAfterQuestDoneFoldoutStates);

            if (_foldoutNpcWordsAfterQuestDone) {
                _height += GetReorderableList(property, "_npcWordsAfterQuestComplete", _npcWordsAfterQuestDoneLists).GetHeight() + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        _height += _isNeedDialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;

        if (_isNeedDialogActionsProperty.boolValue) {
            _height += EditorGUI.GetPropertyHeight(_dialogActionsProperty) + EditorGUIUtility.standardVerticalSpacing;
        }

        return _height + EditorGUIUtility.standardVerticalSpacing;
    }
}
