using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartDialogButton : MonoBehaviour {
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Text _title;

    public void Init(string title, UnityAction action) {
        _title.text = title;
        _startButton.onClick.AddListener(action);
    }

    public void RemoveListenersStartButton() {
        _startButton.onClick.RemoveAllListeners();
    }
}
