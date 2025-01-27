using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(TestA))]
public class TestAPropertyDrawer : PropertyDrawer {
    private ReorderableList reorderableList;
    private float _height = EditorGUIUtility.singleLineHeight * 3;
    private Dictionary<string, ReorderableList> reorderableLists = new();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        //if (reorderableList == null) {
        //    SerializedProperty namesProperty = property.FindPropertyRelative("names");

        //    reorderableList = new ReorderableList(namesProperty.serializedObject, namesProperty) {
        //        displayAdd = true,
        //        displayRemove = true,
        //        draggable = true,

        //        drawHeaderCallback = rect => EditorGUI.LabelField(rect, namesProperty.displayName),

        //        drawElementCallback = (rect, index, focused, active) => {
        //            var element = namesProperty.GetArrayElementAtIndex(index);

        //            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, _height), element);
        //        },

        //        onAddCallback = list => {
        //            var arrayProperty = list.serializedProperty;
        //            arrayProperty.arraySize++;
        //            var newElement = arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1);

        //            newElement.managedReferenceValue = new TestA();
        //            arrayProperty.serializedObject.ApplyModifiedProperties();
        //        },

        //        elementHeightCallback = index => {
        //            var height = _height;

        //            return height;
        //        },
        //    };
        //}

        //EditorGUI.BeginProperty(position, label, property);

        //reorderableList.DoList(new Rect(position.x, position.y, position.width, reorderableList.GetHeight()));

        //EditorGUI.EndProperty();

        ReorderableList reorderableList = GetReorderableList(property);

        EditorGUI.BeginProperty(position, label, property);
        reorderableList.DoList(new Rect(position.x, position.y, position.width, reorderableList.GetHeight()));
        EditorGUI.EndProperty();
    }

    private ReorderableList GetReorderableList(SerializedProperty property) {
        string propertyPath = property.propertyPath;

        if (reorderableLists.ContainsKey(propertyPath)) {
            return reorderableLists[propertyPath];
        }

        SerializedProperty namesProperty = property.FindPropertyRelative("names");

        ReorderableList newList = new ReorderableList(property.serializedObject, namesProperty, true, true, true, true);

        newList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, namesProperty.displayName);
        };

        newList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            SerializedProperty element = namesProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, _height), element, GUIContent.none);
        };

        newList.elementHeightCallback = (int index) => {
            return _height;
        };

        reorderableLists[propertyPath] = newList;
        return newList;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        //if (reorderableList == null) {
        //    return base.GetPropertyHeight(property, label);
        //}
        //return reorderableList.GetHeight();
        return GetReorderableList(property).GetHeight();
    }
}
