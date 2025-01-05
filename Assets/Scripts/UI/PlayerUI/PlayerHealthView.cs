using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Text _healthValue;
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private HealthAttribute _healthAttribute;

    private void Awake() {
        EventManager.ActionItemOver += UpdateHealthBar;
        EventManager.OnHealthChanged += UpdateHealthBar;
        _player = FindObjectOfType<Player>();
    }

    private void OnDestroy() {
        EventManager.ActionItemOver -= UpdateHealthBar;
        EventManager.OnHealthChanged -= UpdateHealthBar;
    }

    public void UpdateHealthBar(Item item) {
        float _currentHealth = _player.HealthController.CurrentHealth;
        float _maxHealth = _healthAttribute.MaxHealth;
        float _value = _currentHealth / _maxHealth;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _currentHealth) + "/" +
            string.Format("{0:0.0}", _maxHealth);
    }

    public void UpdateHealthBar() {
        float _currentHealth = _player.HealthController.CurrentHealth;
        float _maxHealth = _healthAttribute.MaxHealth;
        float _value = _currentHealth / _maxHealth;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _currentHealth) + "/" +
            string.Format("{0:0.0}", _maxHealth);
    }
}
