using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField]
    private Sprite _icon;

    public Sprite Icon { get => _icon; }
}