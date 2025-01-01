using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour {
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Text _title;

    //public void Init(IDialog dialog) {
    //    _title.text = dialog.DialogData.title;
    //    _startButton.onClick.AddListener(() => { dialog.Start(); });
    //}

    public void Init(Dialog dialog) {
        _title.text = dialog.DialogData.title;
        _startButton.onClick.AddListener(() => { dialog.Start(); });
    }

    public void Reset() {
        _startButton.onClick.RemoveAllListeners();
    }
}
