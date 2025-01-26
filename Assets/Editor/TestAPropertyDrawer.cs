using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(TestA))]
public class TestAPropertyDrawer : PropertyDrawer {
    private ReorderableList reorderableList;
    private float _height = EditorGUIUtility.singleLineHeight * 3;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        if (reorderableList == null) {
            SerializedProperty namesProperty = property.FindPropertyRelative("_names");

            reorderableList = new ReorderableList(namesProperty.serializedObject, namesProperty) {
                displayAdd = true,
                displayRemove = true,
                draggable = true,

                drawHeaderCallback = rect => EditorGUI.LabelField(rect, namesProperty.displayName),

                drawElementCallback = (rect, index, focused, active) => {
                    var element = namesProperty.GetArrayElementAtIndex(index);

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, _height), element);
                },

                elementHeightCallback = index => {
                    var height = _height;

                    return height;
                },
            };
        }

        EditorGUI.BeginProperty(position, label, property);

        reorderableList.DoLayoutList();

        EditorGUI.EndProperty();
    }

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    //    if (reorderableList == null) {
    //        return base.GetPropertyHeight(property, label);
    //    }
    //    return reorderableList.GetHeight();
    //}
}
