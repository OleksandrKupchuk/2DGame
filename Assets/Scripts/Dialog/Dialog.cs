using System;
using UnityEngine;
using UnityEngine.UI;

public class Dialog {
    public DialogController _dialogController;
    public DialogData _dialogData;
    private Button _nextButton;
    private Button _backButton;
    private Button _closeButton;
    private Text _description;
    private int _paragraphCounter = 0;
    private IDialogAction _dialogAction;

    public DialogData DialogData { get => _dialogData; }

    public Dialog(DialogData dialogData, DialogController dialogController, IDialogAction dialogAction) : this (dialogData, dialogController) {
        _dialogAction = dialogAction;
    }

    public Dialog(DialogData dialogData, DialogController dialogController) {
        _dialogData = dialogData;
        _dialogController = dialogController;
        _nextButton = dialogController.NextButton;
        _backButton = dialogController.BackButton;
        _closeButton = dialogController.CloseButton;
        _description = dialogController.Description;
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
            _dialogController.DisableStartButtons();

            _closeButton.gameObject.SetActive(false);
            _description.gameObject.SetActive(true);
            _description.text = _dialogData.paragraphs[0];

            if (_dialogData.paragraphs.Length == 1) {
                _backButton.gameObject.SetActive(true);
                _backButton.onClick.AddListener(() => { BackToDialogs(); });
            }
            else if (_dialogData.paragraphs.Length > 1) {
                _nextButton.gameObject.SetActive(true);
                _nextButton.onClick.AddListener(() => { NextParagraph(); });
            }
        }
    }

    private void BackToDialogs() {
        _paragraphCounter = 0;

        _description.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);

        _dialogController.EnableStartButtons();

        _nextButton.onClick.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();
        _closeButton.gameObject.SetActive(true);
    }

    private void NextParagraph() {
        Console.WriteLine($"next click button title '{_dialogData.title}'");

        _paragraphCounter++;

        if (_paragraphCounter < _dialogData.paragraphs.Length) {
            _description.text = _dialogData.paragraphs[_paragraphCounter];
        }

        if (_paragraphCounter == _dialogData.paragraphs.Length - 1) {
            _nextButton.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(true);
            _dialogAction?.DoAction();
            _backButton.onClick.AddListener(() => { BackToDialogs(); });
        }
    }
}
