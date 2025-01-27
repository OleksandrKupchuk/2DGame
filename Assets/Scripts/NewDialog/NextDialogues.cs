using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new next")]
public class NextDialogues : ScriptableObject {
    [SerializeField]
    private List<NextDialog> _nextDialogues;
}
