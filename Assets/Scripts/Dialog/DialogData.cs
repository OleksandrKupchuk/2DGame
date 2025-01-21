using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Dialogs", menuName = "Dialogs/NewDialogData")]
[System.Serializable]
public class DialogData {
    [SerializeField]
    private string _title;
    [SerializeField]
    private List<Paragraph> _paragraphs;
    [SerializeField]
    public bool _isHaveConditionToUnlockDialog;
    [SerializeField]
    private List<ConditionUsage> _conditions;
    [SerializeField]
    private bool _isNeedDialogActions;
    [SerializeField]
    private List<DialogAction> _dialogActions;

    public string Title => _title;
    public List<Paragraph> Paragraphs => _paragraphs;
    public bool IsHaveToSaySomething => Paragraphs.Count > 0;
    public bool IsHaveConditionToUnlockDialog => _isHaveConditionToUnlockDialog;
    public bool IsNeedDialogActions => _isNeedDialogActions;
    public List<DialogAction> DialogActions => _dialogActions;
}
