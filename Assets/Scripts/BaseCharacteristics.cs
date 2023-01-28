using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacteristic : MonoBehaviour
{
    [SerializeField]
    protected float _health;

    [SerializeField]
    protected float _maxHealth;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _jumpVelocity;

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    protected void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
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
}
