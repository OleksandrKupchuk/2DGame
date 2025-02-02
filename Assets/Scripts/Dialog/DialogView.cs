using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogView : MonoBehaviour {
    private ObjectPool<StartDialogButton> _startDialogButtonsPool;
    private float _heightDialogTitle;
    private LayoutElement _dialogContainerLayoutElement;
    private RectTransform _backgroundRectTransform;
    private RectTransform _speakerRectTransform;
    private VerticalLayoutGroup _backgroundVerticalLayoutGroup;
    private List<DialogData> _dialogues;

    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private GameObject _dialogContainer;
    [SerializeField]
    private StartDialogButton _startDialogButtonPrefab;
    [SerializeField]
    private Text _speakerName;
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private Button _nextButton;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private Text _description;

    private void Awake() {
        _startDialogButtonsPool = new ObjectPool<StartDialogButton>(_startDialogButtonPrefab, _dialogContainer.transform);
        _dialogContainerLayoutElement = _dialogContainer.GetComponent<LayoutElement>();
        _heightDialogTitle = _dialogContainer.GetComponent<GridLayoutGroup>().cellSize.y;
        _backgroundRectTransform = _background.GetComponent<RectTransform>();
        _speakerRectTransform = _speakerName.gameObject.GetComponent<RectTransform>();
        _backgroundVerticalLayoutGroup = _background.GetComponent<VerticalLayoutGroup>();

        _dialogController.OnDialoguesOpened += OpenDialogues;
        _dialogController.OnDialoguesClosed += CloseDialogues;
        _dialogController.OnParagraphShowed += ShowParagraph;

        AddListenerCloseButton(() => { CloseDialogues(); });
        AddListenerBackButton(() => { 
            DisableBackButton(); 
            DisableDescription();
            EnableCloseButton(); 
            ShowDialogues(_dialogController.GetUpdatedDialogues());
        });
        AddListenerNextButton(() => { _dialogController.GoToNextParagraph(); });
    }

    public void OnDestroy() {
        _dialogController.OnDialoguesOpened -= OpenDialogues;
        _dialogController.OnDialoguesClosed -= CloseDialogues;
        _dialogController.OnParagraphShowed -= ShowParagraph;
    }

    private void Start() {
        CloseDialogues();
        DisableNextButton();
        DisableBackButton();
        DisableDescription();
    }

    public void OpenDialogues(string speakerName, List<DialogData> dialogues) {
        _dialogues = dialogues;
        _speakerName.text = speakerName;
        _background.SetActive(true);
        ShowDialogues(dialogues);
    }

    private void ShowDialogues(List<DialogData> dialogues) {
        UpdatePositionAndSizeBackground(dialogues.Count);

        foreach (var dialog in dialogues) {
            StartDialogButton _startDialogButton = _startDialogButtonsPool.GetEnabled();
            _startDialogButton.Init(dialog);
        }
    }

    private void UpdatePositionAndSizeBackground(int amountDialoguesCurrentNpc) {
        _dialogContainerLayoutElement.minHeight = amountDialoguesCurrentNpc * _heightDialogTitle;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_backgroundRectTransform);
        float _heightBackground = _speakerRectTransform.rect.height + _backgroundVerticalLayoutGroup.spacing + _backgroundVerticalLayoutGroup.padding.top +
            _backgroundVerticalLayoutGroup.padding.bottom + _dialogContainerLayoutElement.minHeight;
        _backgroundRectTransform.anchoredPosition = new Vector2(_backgroundRectTransform.anchoredPosition.x, _heightBackground);
    }

    private void ShowParagraph(string paragraph, bool isLastParagraph) {
        DisableCloseButton();
        DisableStartButtons();

        if (isLastParagraph) {
            DisableNextButton();
            EnableBackButton();
            EnableDescription();
            SetDescriptionText(paragraph);
        }
        else {
            DisableBackButton();
            EnableNextButton();
            EnableDescription();
            SetDescriptionText(paragraph);
        }
    }

    private void CloseDialogues() {
        DisableStartButtons();
        _background.SetActive(false);
    }

    private void DisableStartButtons() {
        _startDialogButtonsPool.DisabledAllObjects();
    }

    private void DisableNextButton() {
        _nextButton.gameObject.SetActive(false);
    }

    private void EnableNextButton() {
        _nextButton.gameObject.SetActive(true);
    }

    private void AddListenerNextButton(UnityAction action) {
        _nextButton.onClick.AddListener(action);
    }

    private void DisableCloseButton() {
        _closeButton.gameObject.SetActive(false);
    }

    private void EnableCloseButton() {
        _closeButton.gameObject.SetActive(true);
    }

    private void AddListenerCloseButton(UnityAction action) {
        _closeButton.onClick.AddListener(action);
    }

    private void DisableBackButton() {
        _backButton.gameObject.SetActive(false);
    }

    private void EnableBackButton() {
        _backButton.gameObject.SetActive(true);
    }

    private void AddListenerBackButton(UnityAction action) {
        _backButton.onClick.AddListener(action);
    }

    private void EnableDescription() {
        _description.gameObject.SetActive(true);
    }

    private void DisableDescription() {
        _description.gameObject.SetActive(false);
    }

    private void SetDescriptionText(string text) {
        _description.text = text;
    }
}
