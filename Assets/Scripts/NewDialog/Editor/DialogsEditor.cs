using UnityEditor;

//[CustomEditor(typeof(Dialogs))]
public class DialogsEditor : Editor {
    SerializedProperty dialogsProperty;

    public override void OnInspectorGUI() {
        dialogsProperty = serializedObject.FindProperty("dialogs");

        serializedObject.Update();
        EditorGUILayout.PropertyField(dialogsProperty, true);
        serializedObject.ApplyModifiedProperties();
    }
}
