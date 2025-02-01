using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData {
    [SerializeField]
    public bool _isDialogExpired;
    [SerializeField]
    public bool _isHaveConditionsToUnlockDialog;
    [SerializeField]
    private List<Condition> _conditions;
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

    public bool IsDialogExpired { get => _isDialogExpired; set => _isDialogExpired = value; }
    public bool IsHaveConditionToUnlockDialog => _isHaveConditionsToUnlockDialog;
    public string PlayerWords => _playerWords;
    public bool IsNeedNpcWords => _isNeedNpcWords;
    public List<string> NpcWords => _npcWords;
    public bool IsHaveToSaySomething => _isNeedNpcWords;
    public bool IsNeedQuest => _isNeedQuest;
    public Quest Quest => _quest;
    public string PlayerWordsAfterQuestComplete => _playerWordsAfterQuestComplete;
    public List<string> NpcWordsAfterQuestComplete => _npcWordsAfterQuestComplete;
    public bool IsNeedDialogActions => _isNeedDialogActions;
    public List<DialogAction> DialogActions => _dialogActions;

    public bool AllConditionsIsTrue() {
        if(_conditions.Count == 0) {
            return false;
        }

        foreach (var condition in _conditions) {
            if (!condition.IsTrue) {
                return false;
            }
        }

        return true;
    }
}
