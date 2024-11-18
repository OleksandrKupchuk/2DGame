using UnityEngine;
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

    public void Init(Item item) {
        _name.text = item.Name;
        _icon.sprite = item.Icon;
        _price.text = "" + item.Price;
        _buy.onClick.AddListener(() => {
            EventManager.BuyItemEventHandler(item);
            print("click buy button = " + item.Name);
        });
    }
}
