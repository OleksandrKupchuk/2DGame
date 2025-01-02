using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    private PlayerAttributes _playerAttributes;
    private float _timerHealthRegeneration;
    private float _timeRegenerationHealth;
    private float _currentHealth;
    private PlayerConfig _config;
    private List<Damage> _objectsAttack = new List<Damage>();
    private float _blockedDamagePerOneArmor = 0.2f;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private InvulnerabilityStatus _invulnerabilityStatus;

    public float CurrentHealth { get => _currentHealth > _playerAttributes.MaxHealth ? _playerAttributes.MaxHealth : _currentHealth; }
    public float MaxHealth { get => _playerAttributes.MaxHealth; }
    public bool IsDead { get => CurrentHealth <= 0; }

    public void Init(PlayerConfig config, PlayerAttributes playerAttributes) {
        _config = config;
        _currentHealth = _config.health;
        _playerAttributes = playerAttributes;
    }

    public void RegenerationHealth() {
        if (_animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).IsName(AnimationName.Hit)) {
            _timerHealthRegeneration = 0;
        }

        _timerHealthRegeneration += Time.deltaTime;

        if (_timerHealthRegeneration >= _config.delayHealthRegeneration) {
            if (CurrentHealth >= _playerAttributes.MaxHealth) {
                return;
            }

            _timeRegenerationHealth += Time.deltaTime;

            if (_timeRegenerationHealth >= 1) {
                _timeRegenerationHealth = 0;
                AddHealth(_playerAttributes.HealthRegeneration);
                Debug.Log($"regeneration health + <color=green>{_playerAttributes.HealthRegeneration}</color>");
                Debug.Log($"health after healing + <color=blue>{_playerAttributes.MaxHealth}</color>");
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
            //print("player is invulnerable");
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
        //print("damage = " + damage);
        float _cleanDamage = damage - GetBlockedDamage(_playerAttributes.Armor);
        //print("clear damage = " + _clearDamage);
        if (_cleanDamage <= 0) {
            return;
        }

        _currentHealth -= _cleanDamage;

        //print("health = " + _health);
        if (IsDead) {
            EventManager.OnDeadHandler();
        }
        else {
            EventManager.OnHealthChangedHandler();
        }
    }

    public float GetBlockedDamage(float armor) {
        float _blockedDamage = armor * _blockedDamagePerOneArmor;
        return _blockedDamage;
    }
}
