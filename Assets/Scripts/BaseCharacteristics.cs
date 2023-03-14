using UnityEngine;

public class BaseCharacteristics : MonoBehaviour {
    [SerializeField]
    protected float _health;
    [SerializeField]
    protected Config _config;

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    protected void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        _health = _config.health;
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
}