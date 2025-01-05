using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsView : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Text _value;

    private void Awake() {
        EventManager.BuyItem += UpdateCoins;
    }

    private void OnDestroy() {
        EventManager.BuyItem -= UpdateCoins;
    }

    private void Start() {
        _player = FindObjectOfType<Player>();
        UpdateCoins(null);
    }

    public void UpdateCoins(Item item) {
        _value.text = "" + _player.Config.coins;
    }
}
