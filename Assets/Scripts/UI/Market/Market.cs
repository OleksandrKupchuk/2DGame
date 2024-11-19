using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Market : MonoBehaviour {
    private Player _player;
    private UnityAction _buyAction;
    private Dictionary<Item, CartItem> _dictionaryItems = new();
    private int _commission;

    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private CartItem _cartItem;
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private Scrollbar _scrollbar;
    [SerializeField]
    private List<Item> _items;

    private void Awake() {
        GenerateCartItems();
        EventManager.BuyItem += Buy;
    }

    private void Start() {
        Disable();
    }

    private void OnDestroy() {
        EventManager.BuyItem -= Buy;
    }

    public void Init(int tradeCommission) {
        _commission = tradeCommission;
    }

    public void SetPlayer(Player player) { _player = player; }

    private void GenerateCartItems() {
        if (_items.Count <= 1) {
            return;
        }

        //int _range = Random.Range(1, _items.Count);
        int _range = _items.Count;
        print("range " + _range);

        for (int i = 0; i < _range; i++) {
            CartItem cartItemObject = Instantiate(_cartItem);
            cartItemObject.transform.SetParent(_content);
            cartItemObject.transform.localScale = Vector3.one;

            Item itemObject = Instantiate(_items[i]);
            itemObject.Disable();

            cartItemObject.Init(itemObject, _commission);

            //cartItemObject.gameObject.SetActive(false);
            _dictionaryItems.Add(itemObject, cartItemObject);
        }
    }

    private void SetScrollBarPositionAfterDelay() {
        Invoke(nameof(SetScrollBarPosition), 0.1f);
    }

    private void SetScrollBarPosition() {
        _scrollbar.value = 0;
    }

    private void Buy(Item item) {
        if (!_dictionaryItems.GetValueOrDefault(item).gameObject.activeSelf) {
            print("You bought this item " +  item.Name);
            return;
        }

        if(_player.Config.conis >= item.Price) {
            _player.Config.conis -= item.Price + _commission;
            _player.Inventory.AddItem(item);
            
            CartItem _cartItem = _dictionaryItems.GetValueOrDefault(item);
            _cartItem.gameObject.SetActive(false);
            _dictionaryItems.Remove(item);
        }
        else {
            print("Not enough money");
        }
    }

    private void Sell(Item item) {

    }

    public void Enable() {
        _background.SetActive(true);
        SetScrollBarPositionAfterDelay();
    }

    public void Disable() {
        _background.SetActive(false);
    }
}
