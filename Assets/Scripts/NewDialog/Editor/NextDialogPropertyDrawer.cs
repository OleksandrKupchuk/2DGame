using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(NextDialog))]
public class NextDialogPropertyDrawer : PropertyDrawer {
    private const float PADDING_LEFT = 12f;

    private SerializedProperty _isNeedQuestProperty;

    private ReorderableList _npcWordsList;
    private float _npcWordsHeigh = EditorGUIUtility.singleLineHeight * 3;
    private bool _isNpcWordsFoldout = false;

    private float _nextDialogFoldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _playerWordsHeight;
    private float _npcWordsListHight;
    private float _npcWordsfoldoutHeight = EditorGUIUtility.singleLineHeight;
    private float _isNeedQuestHeight;
    private float _questPropertyHeight;

    private float _foldoutPositionY;
    private float _playerWordsPositionY;
    private float _npcWordsListPositionY;
    private float _isNeedQuestPositionY;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        //зараз в клас≥ Dialogues кожен елемент €кий створюЇтьс€ посилаЇтьс€ на один ≥ той самий, тому дл€ класу Dialogues потр≥бно дописати в списку
        //onAddCallback = list => {
        //    mAListProperty.arraySize++;
        //    var newElement = mAListProperty.GetArrayElementAtIndex(mAListProperty.arraySize - 1);

        //    // ≤н≥ц≥ал≥зац≥€ нового екземпл€ра
        //    newElement.managedReferenceValue = new TestA();
        //    serializedObject.ApplyModifiedProperties();
        //}
        CreateCustomNpcWordsList(property);

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
        DrawNpcWordsList(position, property, label);
        DrawIsNeedQuestField(position, property);
        DrawQuestField(position, property);

        EditorGUI.indentLevel--;
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
                Rect labelPosition = new Rect(rect.x, rect.y, _labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(labelPosition, $"Sentence {index + 1}");

                EditorGUI.BeginChangeCheck();
                string _input = EditorGUI.TextArea(new Rect(rect.x + _labelWidth, rect.y, rect.width - _labelWidth, _npcWordsHeigh), _element.stringValue);

                if (EditorGUI.EndChangeCheck()) {
                    _element.stringValue = _input;
                }
            },

            elementHeightCallback = index => {
                return _npcWordsHeigh;
            },
        };
    }

    private void DrawPlayerWordsField(Rect position, SerializedProperty property) {
        SerializedProperty _playerWordsProperty = property.FindPropertyRelative("_playerWords");
        _playerWordsPositionY = _foldoutPositionY + _nextDialogFoldoutHeight + EditorGUIUtility.standardVerticalSpacing;

        float _playerWordsLabelWidth = EditorGUIUtility.labelWidth;
        Rect _playerWordsLabelPosition = new Rect(position.x, _playerWordsPositionY, _playerWordsLabelWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(_playerWordsLabelPosition, "Player Words");

        _playerWordsHeight = EditorGUIUtility.singleLineHeight * 2;
        Rect _playerWordsPosition = new Rect(position.x + _playerWordsLabelWidth, _playerWordsPositionY, position.width - _playerWordsLabelWidth, _playerWordsHeight);
        EditorGUI.BeginChangeCheck();
        string _input = EditorGUI.TextArea(_playerWordsPosition, _playerWordsProperty.stringValue);

        if (EditorGUI.EndChangeCheck()) {
            _playerWordsProperty.stringValue = _input;
        }
    }

    private void DrawNpcWordsList(Rect position, SerializedProperty property, GUIContent label) {
        float _npcWordsfoldoutPositionY = _playerWordsPositionY + _playerWordsHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _npcWordsfoldoutPosition = new Rect(position.x + PADDING_LEFT, _npcWordsfoldoutPositionY, position.width - PADDING_LEFT, _npcWordsfoldoutHeight);
        _isNpcWordsFoldout = EditorGUI.Foldout(_npcWordsfoldoutPosition, _isNpcWordsFoldout, "Npc Words", true);

        if (_isNpcWordsFoldout) {
            _npcWordsListHight = _npcWordsList.GetHeight();
            _npcWordsListPositionY = _npcWordsfoldoutPositionY + _npcWordsfoldoutHeight + EditorGUIUtility.standardVerticalSpacing;
            Rect _npcWordsListPosition = new Rect(position.x + PADDING_LEFT, _npcWordsListPositionY, position.width - PADDING_LEFT, _npcWordsListHight);
            _npcWordsList.DoList(_npcWordsListPosition);
        }
        else {
            _npcWordsListPositionY = _npcWordsfoldoutPositionY;
            _npcWordsListHight = _npcWordsfoldoutHeight;
        }
    }

    private void DrawIsNeedQuestField(Rect position, SerializedProperty property) {
        SerializedProperty _isNeedQuestProperty = property.FindPropertyRelative("_isNeedQuest");
        _isNeedQuestHeight = EditorGUIUtility.singleLineHeight;
        _isNeedQuestPositionY = _npcWordsListPositionY + _npcWordsListHight + EditorGUIUtility.standardVerticalSpacing;
        Rect _isNeedQuestPosition = new Rect(position.x, _isNeedQuestPositionY, position.size.x, _nextDialogFoldoutHeight);
        EditorGUI.PropertyField(_isNeedQuestPosition, _isNeedQuestProperty);
    }

    private void DrawQuestField(Rect position, SerializedProperty property) {
        SerializedProperty _questProperty = property.FindPropertyRelative("_quest");

        if (_isNeedQuestProperty.boolValue) {
            _questPropertyHeight = EditorGUIUtility.singleLineHeight;
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

        if (!property.isExpanded) {
            return _nextDialogFoldoutHeight;
        }

        float _height = 0;

        if(_isNpcWordsFoldout) {
            _height += _npcWordsfoldoutHeight;
        }

        if (_isNeedQuestProperty.boolValue) {
            _height += _nextDialogFoldoutHeight + _playerWordsHeight + _npcWordsListHight + _isNeedQuestHeight + _questPropertyHeight + EditorGUIUtility.standardVerticalSpacing;
        }
        else {
            _height += _nextDialogFoldoutHeight + _playerWordsHeight + _npcWordsListHight + _isNeedQuestHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        return _height + EditorGUIUtility.singleLineHeight;
    }
}
