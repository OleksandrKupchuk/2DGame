using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogs", menuName = "Dialogues/NewDialogs")]
public class Dialogues : ScriptableObject {
    [field: SerializeField]
    public List<DialogData> DialoguesData { get; private set; }

    public void OnEnable() {
        if(DialoguesData == null) {
            return;
        }

        foreach (var dialog in DialoguesData) {
            dialog.IsDialogExpired = false;
        }
    }
}
