using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
    public DialogData _dialogData;
    private Button _nextButton;
    private Button _backButton;
    private Button _closeButton;
    private Dictionary<string, Dialog> _dialogs;
    private Text _description;
    private int _paragraphCounter = 0;

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Text _title;

    public void Init(DialogData dialogData, Button next, Button back, Button close, ref Dictionary<string, Dialog> dialogs, Text description) {
        _dialogData = dialogData;
        _title.text = dialogData.title;
        _nextButton = next;
        _backButton = back;
        _closeButton = close;
        _dialogs = dialogs;
        _description = description;
        _startButton.onClick.AddListener(() => { StartDialog(); });
    }

    private void StartDialog() {
        print($"start click button title '{_dialogData.title}'");

        if (_dialogData.paragraphs != null && _dialogData.paragraphs.Length != 0) {
            DisableAllStartButton();

            _startButton.gameObject.SetActive(false);
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

    private void DisableAllStartButton() {
        foreach (KeyValuePair<string, Dialog> dialog in _dialogs) {
            print($"hide button title '{dialog.Value._dialogData.title}'");
            dialog.Value.DisableStartButton();
        }
    }

    private void BackToDialogs() {
        _paragraphCounter = 0;

        _description.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);

        foreach (KeyValuePair<string, Dialog> dialog in _dialogs) {
            dialog.Value.EnableStartButton();
        }

        _nextButton.onClick.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();
        _closeButton.gameObject.SetActive(true);
    }

    private void NextParagraph() {
        print($"next click button title '{_dialogData.title}'");

        _paragraphCounter++;

        if (_paragraphCounter < _dialogData.paragraphs.Length) {
            _description.text = _dialogData.paragraphs[_paragraphCounter];
        }

        if (_paragraphCounter == _dialogData.paragraphs.Length - 1) {
            _nextButton.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(true);
            _backButton.onClick.AddListener(() => { BackToDialogs(); });
        }
    }

    public void EnableStartButton() {
        _startButton.gameObject.SetActive(true);
    }

    public void DisableStartButton() {
        _startButton.gameObject.SetActive(false);
    }
}
