using UnityEngine;

public class BaseCharacteristics : MonoBehaviour {
    protected float _blockedDamagePerOneArmor = 0.2f;
    protected float _currentHealth;

    [SerializeField]
    protected Config _config;

    public Rigidbody2D Rigidbody { get; protected set; }
    public Animator Animator { get; protected set; }

    public float CurrentHealth { get => _currentHealth; }
    public bool IsDead { get => _currentHealth <= 0; }
    public AttachingEventToAnimation AttachingEventToAnimation { get; private set; } = new AttachingEventToAnimation();

    protected void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        _currentHealth = _config.health;
    }

    public void ResetRigidbodyVelocity() {
        Rigidbody.velocity = Vector2.zero;
    }

    public bool IsEndCurrentAnimation(Animator animator, int layer) {
        if (animator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1) {
            return true;
        }

        return false;
    }

    public void Move(float inputDirection) {
        Rigidbody.velocity = new Vector2(inputDirection * _config.speed, Rigidbody.velocity.y);
    }

    public void Move(float inputDirection, float speed) {
        Rigidbody.velocity = new Vector2(inputDirection * speed, Rigidbody.velocity.y);
    }

    public void MoveEaseInQuint(float inputDirection, float speed, float time) {
        Rigidbody.velocity = new Vector2(inputDirection * speed * time * time, Rigidbody.velocity.y);
    }

    public float GetBlockedDamage(float armor) { 
        float _blockedDamage = armor * _blockedDamagePerOneArmor;
        return _blockedDamage;
    }
}