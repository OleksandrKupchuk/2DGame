using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    [SerializeField]
    private Image _content;

    public bool IsEmptyCell { get => _content.sprite == null; }

    public void SetContentIcon(Sprite icon) {
        _content.sprite = icon;
    }
}