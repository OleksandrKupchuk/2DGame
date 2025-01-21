using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogs", menuName = "Dialogs/NewDialogs")]
public class Dialogs : ScriptableObject {
    [SerializeField]
    private List<DialogData> dialogs;

    [Header("---------------------")]
    [SerializeField]
    private DialogData dialog;
}
