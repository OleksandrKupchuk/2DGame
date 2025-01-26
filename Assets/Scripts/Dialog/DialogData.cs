using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData {
    [SerializeField]
    private string _playerWords;
    [SerializeField]
    private bool _isNeedNpcWords;
    [SerializeField]
    private List<string> _npcWords;
    [SerializeField]
    private bool _isNeedQuests;
    [SerializeField]
    private Quest _quest;
    [SerializeField]
    private List<NextDialog> _nextDialogs;
    [SerializeField]
    public bool _isHaveConditionToUnlockDialog;
    [SerializeField]
    private List<ConditionUsage> _conditions;
    [SerializeField]
    private bool _isNeedDialogActions;
    [SerializeField]
    private List<DialogAction> _dialogActions;

    //public string Title => _playerWords;
    //public bool IsNeedParagraphs => _isNeedNpcWords;
    //public List<string> Paragraphs => _npcWords;
    //public bool IsHaveToSaySomething => _isNeedNpcWords;
    //public bool IsHaveConditionToUnlockDialog => _isHaveConditionToUnlockDialog;
    //public bool IsNeedDialogActions => _isNeedDialogActions;
    //public List<DialogAction> DialogActions => _dialogActions;

    //public string GetParagraph(int index) => _npcWords[index];
}
