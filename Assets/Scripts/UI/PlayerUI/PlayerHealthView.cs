using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Text _healthValue;
    [SerializeField]
    private Image _healthBar;

    private void Awake() {
        EventManager.PutOnItem += UpdateHealthBar;
        EventManager.TakeAwayItem += UpdateHealthBar;
        EventManager.UseItem += UpdateHealthBar;
        EventManager.ActionItemOver += UpdateHealthBar;
        _player = FindObjectOfType<Player>();
    }

    private void OnDestroy() {
        EventManager.PutOnItem -= UpdateHealthBar;
        EventManager.TakeAwayItem -= UpdateHealthBar;
        EventManager.UseItem -= UpdateHealthBar;
        EventManager.ActionItemOver -= UpdateHealthBar;
    }

    public void UpdateHealthBar(Item item) {
        float _value = _player.CurrentHealth / _player.PlayerAttributes.Health;
        _healthBar.fillAmount = _value;
        _healthValue.text = string.Format("{0:0.0}", _player.CurrentHealth) + "/" + 
            string.Format("{0:0.0}", _player.PlayerAttributes.Health);
    }
}
