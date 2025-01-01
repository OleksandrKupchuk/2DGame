using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Text _healthValue;
    [SerializeField]
    private Image _healthBar;

    private void Awake() {
        //EventManager.OnHealthChanged += UpdateHealthBar; краще мати подію що здоров'я змінилося і просто оновлювати вигляд тай все 
        //UpdateHealthBar(float health);
        EventManager.PutOnItem += UpdateHealthBar;
        EventManager.TakeAwayItem += UpdateHealthBar;
        EventManager.UseItem += UpdateHealthBar;
        EventManager.ActionItemOver += UpdateHealthBar;
        HealthController.OnHealthChanged += UpdateHealthBar;
        HealthController.OnDamageTaken += UpdateHealthBar;
        _player = FindObjectOfType<Player>();
    }

    private void OnDestroy() {
        EventManager.PutOnItem -= UpdateHealthBar;
        EventManager.TakeAwayItem -= UpdateHealthBar;
        EventManager.UseItem -= UpdateHealthBar;
        EventManager.ActionItemOver -= UpdateHealthBar;
        HealthController.OnHealthChanged -= UpdateHealthBar;
        HealthController.OnDamageTaken -= UpdateHealthBar;
    }

    public void UpdateHealthBar(Item item) {
        float _currentHealth = _player.HealthController.CurrentHealth;
        float _maxHealth = _player.PlayerAttributes.MaxHealth;
        float _value = _currentHealth / _maxHealth;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _currentHealth) + "/" + 
            string.Format("{0:0.0}", _maxHealth);
    }

    public void UpdateHealthBar() {
        float _currentHealth = _player.HealthController.CurrentHealth;
        float _maxHealth = _player.PlayerAttributes.MaxHealth;
        float _value = _currentHealth / _maxHealth;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _currentHealth) + "/" +
            string.Format("{0:0.0}", _maxHealth);
    }
}
