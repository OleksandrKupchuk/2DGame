using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Text _healthValueTextComponent;
    [SerializeField]
    private Image _healthBar;

    private void OnEnable() {
        EventManager.UpdatingHealthBar += UpdateHealthBar;
    }

    private void OnDestroy() {
        EventManager.UpdatingHealthBar -= UpdateHealthBar;
    }

    private void Awake() {
        _player = FindObjectOfType<Player>();
    }

    private void UpdateHealthBar() {
        float _value = _player.CurrentHealth / _player.Inventory.PlayerAttributes.Health;
        _healthBar.fillAmount = _value;
        UpdateHealthText(_player.CurrentHealth, _player.Inventory.PlayerAttributes.Health);
    }

    private void UpdateHealthText(float currentValue, float maxValue) {
        _healthValueTextComponent.text = string.Format("{0:0.0}", currentValue) + "/" + string.Format("{0:0.0}", maxValue);
    }
}