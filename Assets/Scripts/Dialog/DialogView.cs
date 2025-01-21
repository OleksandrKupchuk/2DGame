using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogView : MonoBehaviour {
    private ObjectPool<StartDialogButton> _startDialogButtonPool;
    private int _amountDialogsCurrentNpc;
    private float _heightDialogTitle;
    private LayoutElement _dialogContainerLayoutElement;
    private RectTransform _backgroundRectTransform;
    private RectTransform _speakerRectTransform;
    private VerticalLayoutGroup _backgroundVerticalLayoutGroup;

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
        _startDialogButtonPool = new ObjectPool<StartDialogButton>(_startDialogButtonPrefab, _dialogContainer.transform);
        _dialogContainerLayoutElement = _dialogContainer.GetComponent<LayoutElement>();
        _heightDialogTitle = _dialogContainer.GetComponent<GridLayoutGroup>().cellSize.y;
        _backgroundRectTransform = _background.GetComponent<RectTransform>();
        _speakerRectTransform = _speakerName.gameObject.GetComponent<RectTransform>();
        _backgroundVerticalLayoutGroup = _background.GetComponent<VerticalLayoutGroup>();
    }

    private void Start() {
        CloseDialogs();
        DisableNextButton();
        DisableBackButton();    
        DisableDescription();
    }

    public void OpenDialogs(string speakerName, List<Dialog> dialogs) {
        _speakerName.text = speakerName;
        _amountDialogsCurrentNpc = dialogs.Count;
        RemoveAllListenersCloseButton();
        AddListenerCloseButton(() => { CloseDialogs(); });
        _background.SetActive(true);
        UpdatePositionAndSizeBackground();
        InitStartDialogButtons(dialogs);
    }

    private void UpdatePositionAndSizeBackground() {
        _dialogContainerLayoutElement.minHeight = _amountDialogsCurrentNpc * _heightDialogTitle;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_backgroundRectTransform);
        float _heightBackground = _speakerRectTransform.rect.height + _backgroundVerticalLayoutGroup.spacing + _backgroundVerticalLayoutGroup.padding.top +
            _backgroundVerticalLayoutGroup.padding.bottom + _dialogContainerLayoutElement.minHeight;
        _backgroundRectTransform.anchoredPosition = new Vector2(_backgroundRectTransform.anchoredPosition.x, _heightBackground);
    }

    private void InitStartDialogButtons(List<Dialog> dialogs) {
        foreach (Dialog dialog in dialogs) {
            StartDialogButton _startDialogButton = _startDialogButtonPool.Get();
            _startDialogButton.Init(dialog.DialogData.Title, dialog.Start);
        }
    }

    public void CloseDialogs() {
        ResetStartButtons();
        DisableStartButtons();
        _background.SetActive(false);
    }

    public void ResetStartButtons() {
        for (int i = 0; i < _amountDialogsCurrentNpc; i++) {
            _startDialogButtonPool.Objects[i].RemoveListenersStartButton();
        }
    }

    public void DisableStartButtons() {
        for (int i = 0; i < _amountDialogsCurrentNpc; i++) {
            _startDialogButtonPool.Objects[i].gameObject.SetActive(false);
        }
    }

    public void EnableStartButtons() {
        for (int i = 0; i < _amountDialogsCurrentNpc; i++) {
            _startDialogButtonPool.Objects[i].gameObject.SetActive(true);
        }
    }

    public void DisableNextButton() {
        _nextButton.gameObject.SetActive(false);
    }

    public void EnableNextButton() {
        _nextButton.gameObject.SetActive(true);
    }

    public void AddListenerNextButton(UnityAction action) {
        _nextButton.onClick.AddListener(action);
    }

    public void RemoveAllListenersNextButton() {
        _nextButton.onClick.RemoveAllListeners();
    }

    public void DisableCloseButton() {
        _closeButton.gameObject.SetActive(false);
    }

    public void EnableCloseButton() {
        _closeButton.gameObject.SetActive(true);
    }

    public void AddListenerCloseButton(UnityAction action) {
        _closeButton.onClick.AddListener(action);
    }

    public void RemoveAllListenersCloseButton() {
        _closeButton.onClick.RemoveAllListeners();
    }

    public void DisableBackButton() {
        _backButton.gameObject.SetActive(false);
    }

    public void EnableBackButton() {
        _backButton.gameObject.SetActive(true);
    }

    public void AddListenerBackButton(UnityAction action) {
        _backButton.onClick.AddListener(action);
    }

    public void RemoveAllListenersBackButton() {
        _backButton.onClick.RemoveAllListeners();
    }

    public void EnableDescription() {
        _description.gameObject.SetActive(true);
    }

    public void DisableDescription() {
        _description.gameObject.SetActive(false);
    }

    public void SetDescriptionText(string text) {
        _description.text = text;
    }
}
