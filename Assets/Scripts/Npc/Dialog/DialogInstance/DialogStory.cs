using System;
using UnityEngine.UI;

public class DialogStory : IDialog {
    public DialogController _dialogController;
    public DialogData _dialogData;
    private Button _nextButton;
    private Button _backButton;
    private Button _closeButton;
    private Text _description;
    private int _paragraphCounter = 0;
    private IQuest _quest;

    public DialogData DialogData { get => _dialogData; }

    public DialogStory(DialogData dialogData, DialogController dialogController, IQuest quest) : this (dialogData, dialogController) {
        _dialogData = dialogData;
        _quest = quest;
    }

    public DialogStory(DialogData dialogData, DialogController dialogController) {
        _dialogData = dialogData;
        _dialogController = dialogController;
        _nextButton = dialogController.NextButton;
        _backButton = dialogController.BackButton;
        _closeButton = dialogController.CloseButton;
        _description = dialogController.Description;
    }

    public void Start() {
        Console.WriteLine($"start click button title '{_dialogData.title}'");

        if (_dialogData.paragraphs != null && _dialogData.paragraphs.Length != 0) {
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
            ProjectContext.Instance.QuestSystem.AddQuest(_quest);
            _backButton.onClick.AddListener(() => { BackToDialogs(); });
        }
    }
}
