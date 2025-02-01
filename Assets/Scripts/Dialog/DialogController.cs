using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogController")]
public class DialogController : ScriptableObject {
    private Dialogues _dialogues;
    private DialogData _dialogData;
    private int _paragraphCounter;
    private List<string> _paragraphs;

    [SerializeField]
    private QuestSystem _questSystem;

    public event Action<string, List<DialogData>> OnDialoguesOpened;
    public event Action OnDialoguesClosed;
    public event Action<DialogData> OnDialogStarted;
    public event Action<string, bool> OnParagraphShowed;
    public event Action OnNextParagraph;

    public void OpenDialogues(string speakerName, Dialogues dialogues) {
        _dialogues = dialogues;
        OnDialoguesOpened?.Invoke(speakerName, GetUpdatedDialogues());
    }

    public List<DialogData> GetUpdatedDialogues() {
        List<DialogData> _sortedDialogues = new();

        foreach (var dialog in _dialogues.DialoguesData) {
            if (dialog.IsDialogExpired) {
                continue;
            }
            else if (!dialog.IsHaveConditionToUnlockDialog) {
                _sortedDialogues.Add(dialog);
            }
            else {
                if (dialog.AllConditionsIsTrue()) {
                    _sortedDialogues.Add(dialog);
                }
            }
        }

        return _sortedDialogues;
    }


    public void CloseDialogues() {
        OnDialoguesClosed?.Invoke();
    }

    public void StartDialog(DialogData dialogData) {
        _dialogData = dialogData;
        _paragraphCounter = 0;

        if (!_dialogData.IsHaveToSaySomething) {
            TryExecuteDialogActions(_dialogData);
            return;
        }

        CheckQuestComplete(dialogData);
    }

    private void TryExecuteDialogActions(DialogData dialogData) {
        if (!dialogData.IsNeedDialogActions) {
            return;
        }

        foreach (var dialogAction in dialogData.DialogActions) {
            dialogAction.Execute();
        }
    }

    private void CheckQuestComplete(DialogData dialogData) {
        if (dialogData.IsNeedQuest && _dialogData.Quest.IsComplete()) {
            _paragraphs = dialogData.NpcWordsAfterQuestComplete;
            dialogData.IsDialogExpired = true;
        }
        else {
            _paragraphs = dialogData.NpcWords;
        }

        CheckLastParagraph();
    }

    public void CheckLastParagraph() {
        if (_paragraphs.Count > _paragraphCounter + 1) {
            OnParagraphShowed?.Invoke(_paragraphs[_paragraphCounter], false);
            _paragraphCounter += 1;
        }
        else {
            OnParagraphShowed?.Invoke(_paragraphs[_paragraphCounter], true);
            TryAddQuest(_dialogData);
            TryExecuteDialogActions(_dialogData);
        }
    }

    private void TryAddQuest(DialogData dialogData) {
        if (!dialogData.IsNeedQuest) {
            return;
        }

        if (dialogData.Quest != null) {
            _questSystem.AddQuest(dialogData.Quest);
        }
    }
}
