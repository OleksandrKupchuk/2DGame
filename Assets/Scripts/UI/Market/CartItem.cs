using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CartItem : MonoBehaviour {
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _price;
    [SerializeField]
    private Button _buy;

    public void Init(Item item, UnityAction action) {
        _name.text = item.Name;
        _icon.sprite = item.Icon;
        _buy.onClick.AddListener(action);
    }

    public void SetPrice(int price) {
        _price.text = "" + price;
    }
}
