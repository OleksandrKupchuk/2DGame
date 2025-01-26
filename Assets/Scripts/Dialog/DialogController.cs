using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogController")]
public class DialogController : ScriptableObject {
    private DialogData _dialogData;
    private int _paragraphCounter;

    public event Action<string, List<DialogData>> OnDialoguesOpened;
    public event Action<DialogData> OnDialogStarted;
    public event Action<string, bool> OnParagraphShowed;
    public DialogData DialogData { get => _dialogData; }

    public void OpenDialogues(string speakerName, Dialogues dialogues) {
        //List<DialogData> _sortedDialogues = dialogues.DialoguesData.OrderBy(dialog => !dialog.IsHaveConditionToUnlockDialog).ToList();
        //OnDialoguesOpened?.Invoke(speakerName, _sortedDialogues);
    }

    public void StartDialog(DialogData dialogData) {
        //_dialogData = dialogData;
        //_paragraphCounter = 0;

        //if (!dialogData.IsHaveToSaySomething) {
        //    foreach (var dialogAction in dialogData.DialogActions) {
        //        dialogAction.Execute();
        //    }

        //    return;
        //}

        //CheckLastParagraph();
    }

    public void CheckLastParagraph() {
        //if (_dialogData.Paragraphs.Count > _paragraphCounter + 1) {
        //    OnParagraphShowed?.Invoke(_dialogData.GetParagraph(_paragraphCounter), false);
        //    _paragraphCounter += 1;
        //}
        //else {
        //    OnParagraphShowed?.Invoke(_dialogData.GetParagraph(_paragraphCounter), true);
        //}
    }
}
