using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    private float _timerHealthRegeneration;
    private float _timeRegenerationHealth;
    private float _currentHealth;
    private PlayerConfig _config;
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

    public float CurrentHealth { get { 
            if(_currentHealth > _healthAttribute.MaxHealth) {
                _currentHealth = _healthAttribute.MaxHealth;
                return _currentHealth;
            }
            else {
                return _currentHealth;
            }
        }
    }
    public float MaxHealth { get => _healthAttribute.MaxHealth; }
    public bool IsDead { get => CurrentHealth <= 0; }

    public void Init(PlayerConfig config) {
        _config = config;
        _currentHealth = config.Health;   
    }

    public void RegenerationHealth() {
        if (_currentHealth >= _healthAttribute.MaxHealth) {
            return;
        }

        _timerHealthRegeneration += Time.deltaTime;

        if (_timerHealthRegeneration >= _config.DelayHealthRegeneration) {

            _timeRegenerationHealth += Time.deltaTime;

            if (_timeRegenerationHealth >= 1) {
                _timeRegenerationHealth = 0;
                AddHealth(_healthRegenerationAttribute.HealthRegeneration);
                Debug.Log($"regeneration Health + <color=green>{_healthRegenerationAttribute.HealthRegeneration}</color>");
                Debug.Log($"Health after healing + <color=blue>{_healthAttribute.MaxHealth}</color>");
            }
        }
    }

    public void AddHealth(float health) {
        _currentHealth += health;
        EventManager.OnHealthChangedHandler();
    }

    public void CheckTakeDamage(float damage, Damage damageObject) {
        if (_objectsAttack.Contains(damageObject)) {
            StartCoroutine(UnregisteredDamageObject(damageObject));
        }
        else if (_invulnerabilityStatus.IsInvulnerability) {
        }
        else {
            RegisterDamageObject(damageObject);
            TakeDamage(damage);
        }
    }

    private IEnumerator UnregisteredDamageObject(Damage damageObject) {
        yield return new WaitForSeconds(2);
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
            EventManager.OnHitHandler();
            EventManager.OnHealthChangedHandler();
        }
    }

    public float GetBlockedDamage(float armor) {
        float _blockedDamage = armor * _blockedDamagePerOneArmor;
        return _blockedDamage;
    }

    public void ResetTimerHealthRegeneration() {
        _timerHealthRegeneration = 0;
    }
}
