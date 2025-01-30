using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData {
    [SerializeField]
    public bool _isHaveConditionsToUnlockDialog;
    [SerializeField]
    private List<ConditionUsage> _conditions;
    [SerializeField]
    private string _playerWords;
    [SerializeField]
    private bool _isNeedNpcWords;
    [SerializeField]
    private List<string> _npcWords;
    [SerializeField]
    private bool _isNeedQuest;
    [SerializeField]
    private Quest _quest;
    [SerializeField]
    private string _playerWordsAfterQuestComplete;
    [SerializeField]
    private List<string> _npcWordsAfterQuestComplete;
    [SerializeField]
    private bool _isNeedDialogActions;
    [SerializeField]
    private List<DialogAction> _dialogActions;

    public bool IsHaveConditionToUnlockDialog => _isHaveConditionsToUnlockDialog;
    public string PlayerWords => _playerWords;
    public bool IsNeedNpcWords => _isNeedNpcWords;
    public List<string> NpcWords => _npcWords;
    public bool IsHaveToSaySomething => _isNeedNpcWords;
    public bool IsNeedDialogActions => _isNeedDialogActions;
    public List<DialogAction> DialogActions => _dialogActions;

    public string GetParagraph(int index) => _npcWords[index];
}
