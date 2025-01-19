using UnityEngine;

public class Character : MonoBehaviour {
    protected float _blockedDamagePerOneArmor = 0.2f;

    [SerializeField]
    protected Config _config;

    public Rigidbody2D Rigidbody { get; protected set; }
    public Animator Animator { get; protected set; }
    public AttachingEventToAnimation AttachingEventToAnimation { get; private set; } = new AttachingEventToAnimation();

    protected void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
    }

    public void ResetRigidbodyVelocity() {
        Rigidbody.velocity = Vector2.zero;
    }

    public bool IsEndCurrentAnimation(Animator animator, int layer) {
        AnimatorStateInfo _info = animator.GetCurrentAnimatorStateInfo(layer);

        if (_info.normalizedTime >= 1) {
            return true;
        }

        return false;
    }

    public bool IsEndCurrentAnimation(Animator animator, int layer, string currentAnimation) {
        AnimatorStateInfo _info = animator.GetCurrentAnimatorStateInfo(layer);

        if (!_info.IsName(currentAnimation)) {
            return false;
        }
        if (_info.normalizedTime >= 1) {
            return true;
        }

        return false;
    }

    public void Move(float inputDirection) {
        Rigidbody.velocity = new Vector2(inputDirection * _config.speed, Rigidbody.velocity.y);
    }

    public void MoveEaseInQuint(float inputDirection, float speed, float time) {
        Rigidbody.velocity = new Vector2(inputDirection * speed * time * time, Rigidbody.velocity.y);
    }

    public float GetBlockedDamage(float armor) { 
        float _blockedDamage = armor * _blockedDamagePerOneArmor;
        return _blockedDamage;
    }
}
