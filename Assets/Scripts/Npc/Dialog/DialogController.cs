using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {
    private ObjectPool<Dialog> _dialogPool;
    private Dictionary<string, Dialog> _dialogDictionary = new Dictionary<string, Dialog>();
    private InputActionMap _actionMap;

    [SerializeField]
    private InputActionAsset _inputActionAsset;

    [SerializeField]
    private Button _next;
    [SerializeField]
    private Button _back;
    [SerializeField]
    private Button _close;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private GameObject _dialogContainer;
    [SerializeField]
    private Dialog _dialogPrefab;

    private void Awake() {
        _dialogPool = new ObjectPool<Dialog>(_dialogPrefab, _dialogContainer.transform);
    }

    private void Start() {
        Hide();
        _next.gameObject.SetActive(false);
        _back.gameObject.SetActive(false);
        _description.gameObject.SetActive(false);
    }

    public void Show(Dialogs dialogs) {
        InitDialogs(dialogs);
        _close.onClick.AddListener(() => { _actionMap.Enable(); Hide(); });
        _background.SetActive(true);
        _actionMap = _inputActionAsset.FindActionMap("Player");
        _actionMap.Disable();
    }

    private void InitDialogs(Dialogs dialogs) {
        _dialogDictionary.Clear();

        foreach (DialogData dialogData in dialogs.dialogsData) {
            Dialog _dialog = _dialogPool.Get();
            _dialog.Init(dialogData, _next, _back, _close, ref _dialogDictionary, _description);

            if (!_dialogDictionary.ContainsKey(dialogData.title)) {
                _dialogDictionary.Add(dialogData.title, _dialog);
            }
        }
    }

    private void Hide() {
        foreach (Dialog dialog in _dialogDictionary.Values) {
            _dialogPool.PutAndDisable(dialog);
        }

        _background.SetActive(false);
    }
}
