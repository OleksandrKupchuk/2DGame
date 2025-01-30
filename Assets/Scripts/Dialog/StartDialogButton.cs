using UnityEngine;
using UnityEngine.UI;

public class StartDialogButton : MonoBehaviour {
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Text _title;
    [SerializeField]
    private DialogController _dialogController;

    public void Init(DialogData dialogData) {
        _title.text = dialogData.PlayerWords;
        _startButton.onClick.AddListener(() => _dialogController.StartDialog(dialogData));
    }

    public void RemoveListenersStartButton() {
        _startButton.onClick.RemoveAllListeners();
    }
}
