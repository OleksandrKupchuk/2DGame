using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Text _healthValue;
    [SerializeField]
    private Image _healthBar;

    private void OnEnable() {
        EventManager.UpdateAttributes += UpdateHealthBar;
    }

    private void OnDestroy() {
        EventManager.UpdateAttributes -= UpdateHealthBar;
    }

    private void Awake() {
        _player = FindObjectOfType<Player>();
    }

    private void UpdateHealthBar() {
        float _value = _player.CurrentHealth / _player.PlayerAttributes.Health;
        _healthBar.fillAmount = _value;
        UpdateHealthValue(_player.CurrentHealth, _player.PlayerAttributes.Health);
    }

    private void UpdateHealthValue(float currentValue, float maxValue) {
        _healthValue.text = string.Format("{0:0.0}", currentValue) + "/" + string.Format("{0:0.0}", maxValue);
    }
}
