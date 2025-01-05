using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {
    private Player _player;
    private Dictionary<Item, CartItem> _dictionaryItems = new();
    private int _commission;
    private int _bufferComission;

    [SerializeField]
    private bool _isDiscount;
    [SerializeField]
    private GameObject _discount;
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

    public void Init(int tradeCommission) {
        _commission = tradeCommission;
        GenerateCartItems();
        Close();
    }

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

            cartItemObject.Init(itemObject, () => { Buy(itemObject); });

            //cartItemObject.gameObject.SetActive(false);
            _dictionaryItems.Add(itemObject, cartItemObject);
        }
    }

    private int GetPriceWithTraderComission(int itemPrice) {
        return itemPrice + (itemPrice * _commission / 100);
    }

    private void Buy(Item item) {
        if (!_dictionaryItems.GetValueOrDefault(item).gameObject.activeSelf) {
            print("You bought this item " + item.Name);
            return;
        }

        if (_player.Config.coins >= GetPriceWithTraderComission(item.Price)) {
            _player.Config.coins -= GetPriceWithTraderComission(item.Price);
            _player.Inventory.AddItem(item);

            CartItem _cartItem = _dictionaryItems.GetValueOrDefault(item);
            _cartItem.gameObject.SetActive(false);
            _dictionaryItems.Remove(item);
            EventManager.BuyItemEventHandler(item);
        }
        else {
            print("Not enough money");
        }
    }

    public void Open(Player player) {
        _player = player;
        //_background.SetActive(true);
        gameObject.SetActive(true);
        ShowDiscount();
        _player.Inventory.Open();
    }

    private void ShowDiscount() {
        if (_isDiscount) {
            _bufferComission = _commission;
            _commission = 0;
            _discount.SetActive(true);
            Invoke(nameof(DelayDiscount), 10f);
        }
        else {
            _commission = _bufferComission;
            _discount.SetActive(false);
        }

        UpdatePrice();
    }

    private void DelayDiscount() {
        _isDiscount = false;
    }

    private void UpdatePrice() {
        foreach (KeyValuePair<Item, CartItem> item in _dictionaryItems) {
            int price = GetPriceWithTraderComission(item.Key.Price);
            item.Value.SetPrice(price);
        }
    }

    public void Close() {
        gameObject.SetActive(false);
        //_background.SetActive(false);
    }
}
