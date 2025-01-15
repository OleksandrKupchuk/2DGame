using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour {
    [SerializeField]
    private Text _healthValue;
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private PlayerHealthController _healthController;

    private void Awake() {
        EventManager.ActionItemOver += UpdateHealthBar;
        EventManager.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDestroy() {
        EventManager.ActionItemOver -= UpdateHealthBar;
        EventManager.OnHealthChanged -= UpdateHealthBar;
    }

    public void UpdateHealthBar(ItemData item) {
        float _value = _healthController.CurrentHealth / _healthController.MaxHealth;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _healthController.CurrentHealth) + "/" +
            string.Format("{0:0.0}", _healthController.MaxHealth);
    }

    public void UpdateHealthBar() {
        float _value = _healthController.CurrentHealth / _healthController.MaxHealth;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _healthController.CurrentHealth) + "/" +
            string.Format("{0:0.0}", _healthController.MaxHealth);
    }
}
