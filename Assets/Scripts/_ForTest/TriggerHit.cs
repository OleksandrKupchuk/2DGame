using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHit : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private bool _isMove = false;
    [SerializeField]
    private float _spped;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.TryGetComponent(out Player player)) {
            player.TakeDamage(_damage);
        }
    }

    private void Update() {
        if (_isMove) {
            transform.Translate(Vector2.right * _spped * Time.deltaTime);
        }
    }
}
