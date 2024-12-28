using UnityEngine;

[CreateAssetMenu(fileName = "Dialogs", menuName = "Dialogs/NewDialogData")]
public class DialogData : ScriptableObject {
    public string title;
    [TextArea(2, 5)]
    public string[] paragraphs;
}
