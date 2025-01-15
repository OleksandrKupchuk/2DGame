using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthController")]
public class PlayerHealthController : ScriptableObject {
    private float _delayBeforeRegenerationHealth;
    private float _timeRegenerationHealth;
    private float _currentHealth;
    private List<Damage> _objectsAttack = new List<Damage>();
    private float _blockedDamagePerOneArmor = 0.2f;

    [SerializeField]
    private InvulnerabilityStatus _invulnerabilityStatus;
    [SerializeField]
    private HealthRegenerationAttribute _healthRegenerationAttribute;
    [SerializeField]
    private HealthAttribute _healthAttribute;
    [SerializeField]
    private ArmorAttribute _armorAttribute;
    [SerializeField]
    private PlayerConfig _config;

    public float CurrentHealth { get => _currentHealth; }
    public float MaxHealth { get => _healthAttribute.MaxHealth; }
    public bool IsDead { get => CurrentHealth <= 0; }

    private void OnEnable() {
        EventManager.OnHealthChanged += CheckCurrentHealth;
        _currentHealth = _config.Health;
    }

    private void OnDisable() {
        EventManager.OnHealthChanged -= CheckCurrentHealth;
    }

    public void RegenerationHealth() {
        if (_currentHealth >= _healthAttribute.MaxHealth) {
            _delayBeforeRegenerationHealth = 0;
            return;
        }

        _delayBeforeRegenerationHealth += Time.deltaTime;

        if (_delayBeforeRegenerationHealth >= _config.DelayHealthRegeneration) {

            _timeRegenerationHealth -= Time.deltaTime;

            if (_timeRegenerationHealth <= 0) {
                _timeRegenerationHealth = 1;
                AddHealth(_healthRegenerationAttribute.HealthRegeneration);
                Debug.Log($"regeneration Health + <color=green>{_healthRegenerationAttribute.HealthRegeneration}</color>");
                Debug.Log($"Health after healing + <color=blue>{_healthAttribute.MaxHealth}</color>");
            }
        }
    }

    public void AddHealth(float health) {
        _currentHealth += health;

        CheckCurrentHealth();

        Debug.Log("Health was added, value = " + health);
        EventManager.OnHealthChangedHandler();
    }

    private void CheckCurrentHealth() {
        if (_currentHealth >= _healthAttribute.MaxHealth) {
            _currentHealth = _healthAttribute.MaxHealth;
        }
    }

    public void CheckTakeDamage(float damage, Damage damageObject) {
        if (_objectsAttack.Contains(damageObject)) {
            Task.Delay(System.TimeSpan.FromSeconds(2d)).ContinueWith(task => UnregisteredDamageObject(damageObject));
        }
        else if (_invulnerabilityStatus.IsInvulnerability) {
            Debug.Log("Player IsInvulnerability");
        }
        else {
            RegisterDamageObject(damageObject);
            TakeDamage(damage);
        }
    }

    private void UnregisteredDamageObject(Damage damageObject) {
        _objectsAttack.Remove(damageObject);
    }

    private void RegisterDamageObject(Damage damageObject) {
        _objectsAttack.Add(damageObject);
    }

    public void TakeDamage(float damage) {
        float _cleanDamage = damage - GetBlockedDamage(_armorAttribute.Armor);

        if (_cleanDamage <= 0) {
            return;
        }

        _currentHealth -= _cleanDamage;

        if (IsDead) {
            EventManager.OnDeadHandler();
        }
        else {
            _delayBeforeRegenerationHealth = 0;
            EventManager.OnHitHandler();
            EventManager.OnHealthChangedHandler();
        }
    }

    public float GetBlockedDamage(float armor) {
        float _blockedDamage = armor * _blockedDamagePerOneArmor;
        return _blockedDamage;
    }
}
