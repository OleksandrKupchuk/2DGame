using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Damage
{
    [SerializeField]
    private bool _isMove = false;
    [SerializeField]
    private float _speed;

    public float Damage { get => damage; }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.TryGetComponent(out PlayerBodyPart playerBodyPart)) {
           playerBodyPart.TakeDamage(damage, this);
        }
    }

    private void Update() {
        if (_isMove) {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
    }
}
