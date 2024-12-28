using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {
    private Player _player;
    private ObjectPool<DialogView> _dialogViewPool;
    private int _amountDialogsCurrentNpc;

    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private GameObject _dialogContainer;
    [SerializeField]
    private DialogView _dialogViewPrefab;

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
    }

    private void Start() {
        CloseDialogs();
        NextButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
    }

    public void OpenDialogs(List<IDialog> dialogs) {
        _player = ProjectContext.Instance.Player;
        InitDialogs(dialogs);
        CloseButton.onClick.AddListener(() => { _player.PlayerMovement.EnableInput(); CloseDialogs(); });
        _background.SetActive(true);
        _player.PlayerMovement.DisableInput();
    }

    private void InitDialogs(List<IDialog> dialogs) {
        _amountDialogsCurrentNpc = dialogs.Count;

        foreach (IDialog dialog in dialogs) {
            DialogView _dialogView = _dialogViewPool.Get();
            _dialogView.Init(dialog);
        }
    }

    public void CloseDialogs() {
        _dialogViewPool.PutAndDisable();
        _background.SetActive(false);
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
