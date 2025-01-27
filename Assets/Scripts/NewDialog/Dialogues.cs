using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogs", menuName = "Dialogues/NewDialogs")]
public class Dialogues : ScriptableObject {
    [field: SerializeField]
    public List<DialogData> DialoguesData { get; private set; }
}
