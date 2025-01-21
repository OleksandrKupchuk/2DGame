using System;
using UnityEngine;

public class Dialog {
    public DialogView _dialogView;
    private DialogData _dialogData;
    private int _paragraphCounter = 0;
    private IDialogAction _dialogAction;

    public DialogData DialogData { get => _dialogData; }

    public Dialog(DialogData dialogData, DialogView dialogView, IDialogAction dialogAction) : this(dialogData, dialogView) {
        _dialogAction = dialogAction;
    }

    public Dialog(DialogData dialogData, DialogView dialogView) {
        _dialogData = dialogData;
        _dialogView = dialogView;
    }

    public Dialog(DialogData dialogData, IDialogAction dialogAction) {
        _dialogData = dialogData;
        _dialogAction = dialogAction;
    }

    public void Start() {
        //Debug.Log($"start click button Title '{_dialogData.Title}'");

        //if (_dialogData.Paragraphs.Length == 0) {
        //    Debug.Log("Do some action");
        //    _dialogAction.DoAction();
        //    return;
        //}

        //_dialogView.DisableStartButtons();

        //_dialogView.DisableCloseButton();
        //_dialogView.EnableDescription();
        //_dialogView.SetDescriptionText(_dialogData.Paragraphs[0]);

        //if (_dialogData.Paragraphs.Length == 1) {
        //    _dialogView.EnableBackButton();
        //    _dialogView.AddListenerBackButton(() => { BackToDialogs(); });
        //}
        //else if (_dialogData.Paragraphs.Length > 1) {
        //    _dialogView.EnableNextButton();
        //    _dialogView.AddListenerNextButton(() => { NextParagraph(); });
        //}
    }

    private void BackToDialogs() {
        _paragraphCounter = 0;

        _dialogView.DisableDescription();
        _dialogView.DisableBackButton();

        _dialogView.EnableStartButtons();
        _dialogView.EnableCloseButton();

        _dialogView.RemoveAllListenersNextButton();
        _dialogView.RemoveAllListenersBackButton();
    }

    private void NextParagraph() {
        //Console.WriteLine($"next click button Title '{_dialogData.Title}'");

        //_paragraphCounter++;

        //if (_paragraphCounter < _dialogData.Paragraphs.Length) {
        //    _dialogView.SetDescriptionText(_dialogData.Paragraphs[_paragraphCounter]);
        //}

        //if (_paragraphCounter == _dialogData.Paragraphs.Length - 1) {
        //    _dialogView.DisableNextButton();
        //    _dialogView.EnableBackButton();
        //    _dialogAction?.DoAction();
        //    _dialogView.AddListenerBackButton(() => { BackToDialogs(); });
        //}
    }
}
