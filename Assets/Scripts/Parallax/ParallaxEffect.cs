using System;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {
    private float _startPosition;
    private float _length;
    private SpriteRenderer _spriteRenderer;

    [Range(0f, 1f)]
    [SerializeField]
    private float _smoothSpeed;
    [SerializeField]
    private Camera _camera;

    private void Start() {
        _startPosition = _camera.transform.position.x;
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _length = _spriteRenderer.bounds.size.x;
    }

    private void FixedUpdate() {
        float difference = _camera.transform.position.x - transform.position.x;

        float destination = _camera.transform.position.x * _smoothSpeed;
        transform.position = new Vector3(_startPosition + destination, transform.position.y, transform.position.z);

        if (difference > 0 && difference >= _length) {
            _startPosition += _length;
        }
        else if (difference < 0 && Math.Abs(difference) >= _length) {
            _startPosition -= _length;
        }

    }
}
