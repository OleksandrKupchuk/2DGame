using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {
    private List<CartItem> items = new();

    [SerializeField]
    private CartItem _cartItem;

    private void Awake() {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
