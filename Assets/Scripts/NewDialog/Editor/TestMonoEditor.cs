using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//[CustomEditor(typeof(TestList))]
public class TestMonoEditor : Editor {
    //private ReorderableList mainList;

    //private void OnEnable() {
    //    SerializedProperty nestedListProperty = serializedObject.FindProperty("_nestedList");

    //    mainList = new ReorderableList(serializedObject, nestedListProperty) {
    //        displayAdd = true,
    //        displayRemove = true,
    //        draggable = true,

    //        drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Main List"),

    //        drawElementCallback = (rect, index, focused, active) => {
    //            SerializedProperty element = nestedListProperty.GetArrayElementAtIndex(index);

    //            //SerializedProperty nameProperty = element.FindPropertyRelative("name");
    //            //Rect nameRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
    //            //EditorGUI.PropertyField(nameRect, nameProperty);

    //            SerializedProperty subItemsProperty = element.FindPropertyRelative("subItems");
    //            Rect subListRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width, EditorGUIUtility.singleLineHeight * (subItemsProperty.arraySize + 2));
    //            DrawSubList(subListRect, subItemsProperty);
    //        },

    //        elementHeightCallback = index => {
    //            SerializedProperty element = nestedListProperty.GetArrayElementAtIndex(index);
    //            SerializedProperty subItemsProperty = element.FindPropertyRelative("subItems");

    //            return EditorGUIUtility.singleLineHeight + 2 + EditorGUIUtility.singleLineHeight * (subItemsProperty.arraySize + 2);
    //        }
    //    };
    //}

    //public override void OnInspectorGUI() {
    //    serializedObject.Update();
    //    mainList.DoLayoutList();
    //    serializedObject.ApplyModifiedProperties();
    //}

    //private void DrawSubList(Rect rect, SerializedProperty subItemsProperty) {
    //    ReorderableList subList = new ReorderableList(subItemsProperty.serializedObject, subItemsProperty) {
    //        displayAdd = true,
    //        displayRemove = true,
    //        draggable = true,

    //        drawHeaderCallback = subRect => EditorGUI.LabelField(subRect, "Sub List"),

    //        drawElementCallback = (subRect, subIndex, focused, active) => {
    //            SerializedProperty subElement = subItemsProperty.GetArrayElementAtIndex(subIndex);
    //            EditorGUI.PropertyField(new Rect(subRect.x, subRect.y, subRect.width, EditorGUIUtility.singleLineHeight), subElement, GUIContent.none);
    //        },

    //        elementHeightCallback = subIndex => EditorGUIUtility.singleLineHeight
    //    };

    //    subList.DoList(rect);
    //}
}
