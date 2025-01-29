using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogData))]
public class DialogDataPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 15f;
    private SerializedProperty _isNeedNpcWordsProperty;
    private SerializedProperty _isNeedQuestProperty;
    private SerializedProperty _isHaveConditionToUnlockDialogProperty;
    private SerializedProperty _isNeedDialogActionsProperty;

    private Dictionary<string, ReorderableList> _npcWordsLists = new();
    private Dictionary<string, bool> _npcWordsListsFoldoutStates = new();
    private Dictionary<string, ReorderableList> _npcWordsAfterQuestDoneLists = new();
    private Dictionary<string, bool> _npcWordsAfterQuestDoneFoldoutStates = new();

    private float _foldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
    private float _isNeedNpcWordsHeight = EditorGUIUtility.singleLineHeight;
    private float _npcWordsHeight;
    private float _npcWordsAfterQuestDoneHeight;
    private float _isNeedQuestHeight = EditorGUIUtility.singleLineHeight;
    private float _questHeight;
    private float _paragraphHeigh = EditorGUIUtility.singleLineHeight * 3;
    private float _isHaveConditionToUnlockDialogHeigh = EditorGUIUtility.singleLineHeight;
    private float _conditionsHeigh;
    private float _isNeedDialogActionsHeigh = EditorGUIUtility.singleLineHeight;
    private float _dialogActionsHeigh;

    private float _positionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        ReorderableList _npcWordsList = GetNpcWordsList(property);
        //ReorderableList _npcWordsList = GetReorderableList(property, _npcWordsLists);
        ReorderableList _npcWordsAfterQuestDoneList = GetNpcWordsAfterQuestDoneList(property);
        //ReorderableList _npcWordsAfterQuestDoneList = GetReorderableList(property, _npcWordsAfterQuestDoneLists);

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
        DrawIsNeedNpcWordsField(position);
        DrawNpcWordsField(position, property, _npcWordsList);
        DrawIsNeedQuestField(position);
        DrawQuestField(position, property);
        DrawPlayerWordsAfterQuestDoneField(position, property);
        DrawNpcWordsAfterQuestDoneField(position, property, _npcWordsAfterQuestDoneList);
        DrawIsNeedDialogActionsField(position);
        DrawDialogActionsField(position, property);

        EditorGUI.EndProperty();
    }

    private ReorderableList GetReorderableList(SerializedProperty property, Dictionary<string, ReorderableList> lists) {
        string _propertyPath = property.propertyPath;

        if (lists.ContainsKey(_propertyPath)) {
            return lists[_propertyPath];
        }

        SerializedProperty _property = property.FindPropertyRelative("_npcWords");

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

    private ReorderableList GetNpcWordsList(SerializedProperty property) {
        string _propertyPath = property.propertyPath;

        if (_npcWordsLists.ContainsKey(_propertyPath)) {
            return _npcWordsLists[_propertyPath];
        }

        SerializedProperty _property = property.FindPropertyRelative("_npcWords");

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

        _npcWordsLists[_propertyPath] = _npcWordsList;
        return _npcWordsList;
    }

    private ReorderableList GetNpcWordsAfterQuestDoneList(SerializedProperty property) {
        string _propertyPath = property.propertyPath;

        if (_npcWordsAfterQuestDoneLists.ContainsKey(_propertyPath)) {
            return _npcWordsAfterQuestDoneLists[_propertyPath];
        }

        SerializedProperty _property = property.FindPropertyRelative("_npcWordsAfterQuestDone");

        ReorderableList _npcWordsAfterQuestDoneList = new ReorderableList(_property.serializedObject, _property) {
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

        _npcWordsAfterQuestDoneLists[_propertyPath] = _npcWordsAfterQuestDoneList;
        return _npcWordsAfterQuestDoneList;
    }

    private void DrawIsHaveConditionToUnlockDialogField(Rect position, SerializedProperty property) {
        _positionY = position.y + _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _isHaveConditionToUnlockDialogHeigh);
        EditorGUI.PropertyField(_position, _isHaveConditionToUnlockDialogProperty);
    }

    private void DrawConditionsField(Rect position, SerializedProperty property) {
        SerializedProperty _conditionsProperty = property.FindPropertyRelative("_conditions");

        if (_isHaveConditionToUnlockDialogProperty.boolValue) {
            _positionY += _isHaveConditionToUnlockDialogHeigh + EditorGUIUtility.standardVerticalSpacing;
            _conditionsHeigh = EditorGUI.GetPropertyHeight(_conditionsProperty);
            Rect _position = new Rect(position.x, _positionY, position.width, _conditionsHeigh);
            EditorGUI.PropertyField(_position, _conditionsProperty);
        }
        else {
            _conditionsHeigh = EditorGUIUtility.singleLineHeight;
            _conditionsProperty.ClearArray();
        }
    }

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _property = property.FindPropertyRelative("_playerWords");

        _positionY += _conditionsHeigh + EditorGUIUtility.standardVerticalSpacing;

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
        if (_isNeedNpcWordsProperty.boolValue) {
            bool _foldoutState = GetFoldoutSatet(property, _npcWordsListsFoldoutStates);

            _positionY += _isNeedNpcWordsHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _foldoutPosition = new Rect(position.x + 12, _positionY, position.size.x, _foldoutHeight);
            _foldoutState = EditorGUI.Foldout(_foldoutPosition, _foldoutState, "Npc Words", true);
            SetFoldoutSatet(property, _npcWordsListsFoldoutStates, _foldoutState);

            if (_foldoutState) {
                _npcWordsHeight = reorderableList.GetHeight();
                _positionY += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
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

    private bool GetFoldoutSatet(SerializedProperty property, Dictionary<string, bool> foldoutStates) {
        if (!foldoutStates.ContainsKey(property.propertyPath)) {
            foldoutStates[property.propertyPath] = false;
        }

        return foldoutStates[property.propertyPath];
    }

    private void SetFoldoutSatet(SerializedProperty property, Dictionary<string, bool> foldoutStates, bool state) {
        string _propertyPath = property.propertyPath;
        foldoutStates[_propertyPath] = state;
    }

    private void DrawIsNeedQuestField(Rect position) {
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
        SerializedProperty _property = property.FindPropertyRelative("_playerWordsAfterQuestDone");

        _positionY += _questHeight + EditorGUIUtility.standardVerticalSpacing;

        float _labelWidth = EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;
        Rect _labelPosition = new Rect(position.x, _positionY, _labelWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(_labelPosition, "Player Words After Quest Done");

        Rect _playerWordsPosition = new Rect(position.x + _labelWidth, _positionY, position.width - _labelWidth, _playerWordsHeight);
        EditorGUI.BeginChangeCheck();
        string _input = EditorGUI.TextArea(_playerWordsPosition, _property.stringValue);

        if (EditorGUI.EndChangeCheck()) {
            _property.stringValue = _input;
        }
    }

    private void DrawNpcWordsAfterQuestDoneField(Rect position, SerializedProperty property, ReorderableList reorderableList) {
        bool _foldoutState = GetFoldoutSatet(property, _npcWordsAfterQuestDoneFoldoutStates);

        _positionY += _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _foldoutPosition = new Rect(position.x + 12, _positionY, position.size.x, _foldoutHeight);
        _foldoutState = EditorGUI.Foldout(_foldoutPosition, _foldoutState, "Npc Words After Done Quest", true);
        SetFoldoutSatet(property, _npcWordsAfterQuestDoneFoldoutStates, _foldoutState);

        if (_foldoutState) {
            _npcWordsAfterQuestDoneHeight = reorderableList.GetHeight();
            _positionY += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _position = new Rect(position.x, _positionY, position.width, _npcWordsHeight);
            reorderableList.DoList(_position);
        }
        else {
            _npcWordsAfterQuestDoneHeight = EditorGUIUtility.singleLineHeight;
        }
    }

    private void DrawIsNeedDialogActionsField(Rect position) {
        _positionY += _npcWordsAfterQuestDoneHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _isNeedDialogActionsHeigh);
        EditorGUI.PropertyField(_position, _isNeedDialogActionsProperty);
    }

    private void DrawDialogActionsField(Rect position, SerializedProperty property) {
        SerializedProperty _dialogActionsProperty = property.FindPropertyRelative("_dialogActions");

        if (_isNeedDialogActionsProperty.boolValue) {
            _dialogActionsHeigh = EditorGUI.GetPropertyHeight(_dialogActionsProperty);
            _positionY += _isNeedDialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;
            Rect _position = new Rect(position.x, _positionY, position.width, _conditionsHeigh);
            EditorGUI.PropertyField(_position, _dialogActionsProperty);
        }
        else {
            _dialogActionsHeigh = EditorGUIUtility.singleLineHeight;
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

        _height += _foldoutHeight + _isHaveConditionToUnlockDialogHeigh + (EditorGUIUtility.standardVerticalSpacing * 2);

        if (_isHaveConditionToUnlockDialogProperty.boolValue) {
            _height += _conditionsHeigh + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _playerWordsHeight + _isNeedNpcWordsHeight + (EditorGUIUtility.standardVerticalSpacing * 2);

        if (_isNeedNpcWordsProperty.boolValue) {
            bool _foldout = GetFoldoutSatet(property, _npcWordsListsFoldoutStates);

            _height += _foldoutHeight + EditorGUIUtility.standardVerticalSpacing;

            if (_foldout) {
                _height += _npcWordsHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        _height += _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;

        if (_isNeedQuestProperty.boolValue) {
            _height += _questHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _playerWordsHeight + _foldoutHeight + (EditorGUIUtility.standardVerticalSpacing * 2);

        bool _foldoutNpcWordsAfterQuestDone = GetFoldoutSatet(property, _npcWordsAfterQuestDoneFoldoutStates);

        if (_foldoutNpcWordsAfterQuestDone) {
            _height += _npcWordsAfterQuestDoneHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        _height += _isNeedDialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;

        if (_isNeedDialogActionsProperty.boolValue) {
            _height += _dialogActionsHeigh + EditorGUIUtility.standardVerticalSpacing;
        }

        Debug.Log("" + property.propertyPath);
        Debug.Log("Height = " + _height + EditorGUIUtility.standardVerticalSpacing);
        return _height + EditorGUIUtility.standardVerticalSpacing;
    }
}
