using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogs", menuName = "Dialogs/NewDialog")]
public class Dialogs : ScriptableObject {
    public List<DialogData> dialogsData;
}

[Serializable]
public class DialogData {
    public string title;
    [TextArea(2, 5)]
    public string[] paragraphs;
}
