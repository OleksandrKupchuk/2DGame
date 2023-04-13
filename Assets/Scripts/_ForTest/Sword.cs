using UnityEngine;

public class Sword : Damage
{
    [SerializeField]
    private bool _isMove = false;
    [SerializeField]
    private float _speed;

    private void Update() {
        if (_isMove) {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
    }
}
