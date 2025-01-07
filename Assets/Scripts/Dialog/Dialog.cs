using System;
using UnityEngine;

public class Dialog {
    public DialogView _dialogView;
    private DialogData _dialogData;
    private int _paragraphCounter = 0;
    private IDialogAction _dialogAction;

    public DialogData DialogData { get => _dialogData; }

    public Dialog(DialogData dialogData, DialogView dialogView, IDialogAction dialogAction) : this (dialogData, dialogView) {
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
        Debug.Log($"start click button title '{_dialogData.title}'");

        if (_dialogData.paragraphs.Length == 0) {
            Debug.Log("Do some action");
            _dialogAction.DoAction();
        }
        else if (_dialogData.paragraphs.Length > 0) {
            _dialogView.DisableStartButtons();

            _dialogView.DisableCloseButton();
            _dialogView.EnableDescription();
            _dialogView.SetDescriptionText(_dialogData.paragraphs[0]);

            if (_dialogData.paragraphs.Length == 1) {
                _dialogView.EnableBackButton();   
                _dialogView.AddListenerBackButton(() => { BackToDialogs(); });
            }
            else if (_dialogData.paragraphs.Length > 1) {
                _dialogView.EnableNextButton();
                _dialogView.AddListenerNextButton(() => { NextParagraph(); });
            }
        }
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
        Console.WriteLine($"next click button title '{_dialogData.title}'");

        _paragraphCounter++;

        if (_paragraphCounter < _dialogData.paragraphs.Length) {
            _dialogView.SetDescriptionText(_dialogData.paragraphs[_paragraphCounter]);
        }

        if (_paragraphCounter == _dialogData.paragraphs.Length - 1) {
            _dialogView.DisableNextButton();
            _dialogView.EnableBackButton();
            _dialogAction?.DoAction();
            _dialogView.AddListenerBackButton(() => { BackToDialogs(); });
        }
    }
}
