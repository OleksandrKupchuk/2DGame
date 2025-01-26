using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NextDialog {
    [SerializeField]
    private string _playerWords;
    [SerializeField]
    private List<string> _npcWords;
    [SerializeField]
    private bool _isNeedQuest;
    [SerializeField]
    private Quest _quest;
}
