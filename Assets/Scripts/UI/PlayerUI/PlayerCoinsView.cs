using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsView : MonoBehaviour {
    [SerializeField]
    private PlayerConfig _playerConfig;

    [SerializeField]
    private Text _value;

    private void Awake() {
        EventManager.BuyItem += UpdateCoins;
    }

    private void OnDestroy() {
        EventManager.BuyItem -= UpdateCoins;
    }

    private void Start() {
        UpdateCoins(null);
    }

    public void UpdateCoins(ItemData item) {
        _value.text = "" + _playerConfig.coins;
    }
}
