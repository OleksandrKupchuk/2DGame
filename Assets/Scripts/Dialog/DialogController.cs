using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {
    private Player _player;
    private ObjectPool<DialogView> _dialogViewPool;
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
    private DialogView _dialogViewPrefab;
    [SerializeField]
    private Text _speakerName;

    [field: SerializeField]
    public Button NextButton { get; private set; }
    [field: SerializeField]
    public Button BackButton { get; private set; }
    [field: SerializeField]
    public Button CloseButton { get; private set; }
    [field: SerializeField]
    public Text Description { get; private set; }

    private void Awake() {
        _dialogViewPool = new ObjectPool<DialogView>(_dialogViewPrefab, _dialogContainer.transform);
        _dialogContainerLayoutElement = _dialogContainer.GetComponent<LayoutElement>();
        _heightDialogTitle = _dialogContainer.GetComponent<GridLayoutGroup>().cellSize.y;
        _backgroundRectTransform = _background.GetComponent<RectTransform>();
        _speakerRectTransform = _speakerName.gameObject.GetComponent<RectTransform>();
        _backgroundVerticalLayoutGroup = _background.GetComponent<VerticalLayoutGroup>();
    }

    private void Start() {
        CloseDialogs();
        NextButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
    }

    public void OpenDialogs(string speakerName, List<Dialog> dialogs) {
        _player = ProjectContext.Instance.Player;
        _speakerName.text = speakerName;
        _amountDialogsCurrentNpc = dialogs.Count;
        CloseButton.onClick.AddListener(() => { _player.PlayerMovement.EnableInput(); CloseDialogs(); });
        _background.SetActive(true);
        UpdatePositionAndSizeBackground();
        InitDialogs(dialogs);
        _player.PlayerMovement.DisableInput();
    }

    private void UpdatePositionAndSizeBackground() {
        _dialogContainerLayoutElement.minHeight = _amountDialogsCurrentNpc * _heightDialogTitle;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_backgroundRectTransform);
        float _heightBackground = _speakerRectTransform.rect.height + _backgroundVerticalLayoutGroup.spacing + _backgroundVerticalLayoutGroup.padding.top +
            _backgroundVerticalLayoutGroup.padding.bottom + _dialogContainerLayoutElement.minHeight;
        _backgroundRectTransform.anchoredPosition = new Vector2(_backgroundRectTransform.anchoredPosition.x, _heightBackground);
    }

    private void InitDialogs(List<Dialog> dialogs) {
        foreach (Dialog dialog in dialogs) {
            DialogView _dialogView = _dialogViewPool.Get();
            _dialogView.Init(dialog);
        }
    }

    public void CloseDialogs() {
        ResetStartButtons();
        DisableStartButtons();
        _background.SetActive(false);
    }

    public void ResetStartButtons() {
        for (int i = 0; i < _amountDialogsCurrentNpc; i++) {
            _dialogViewPool.Objects[i].Reset();
        }
    }

    public void DisableStartButtons() {
        for (int i = 0; i < _amountDialogsCurrentNpc; i++) {
            _dialogViewPool.Objects[i].gameObject.SetActive(false);
        }
    }

    public void EnableStartButtons() {
        for (int i = 0; i < _amountDialogsCurrentNpc; i++) {
            _dialogViewPool.Objects[i].gameObject.SetActive(true);
        }
    }
}
