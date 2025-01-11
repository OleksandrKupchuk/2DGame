using UnityEngine;

public class ItemView : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer _icon;

    [field: SerializeField]
    public Item ItemData { get; private set; }

    private void OnEnable() {
        _icon.sprite = ItemData.Icon;
    }
}
