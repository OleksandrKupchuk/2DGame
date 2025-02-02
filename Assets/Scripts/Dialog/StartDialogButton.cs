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
        _startButton.onClick.RemoveAllListeners();

        _title.text = _dialogController.GetDialogTitle(dialogData);
        _startButton.onClick.AddListener(() => _dialogController.StartDialog(dialogData));
    }
}
