using UnityEngine;

public class FireBall : Damage
{
    private Rigidbody2D _rigidbody2D;
    private float _timer;
    private Transform _parent;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timeToDisable;

    public void Init(Transform parent) {
        _parent = parent;
    }

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = _timeToDisable;
    }

    private void Start() {
        _parent = transform.GetComponentInParent<Transform>();
    }

    private void Update() {
        if (!gameObject.activeSelf) {
            return;
        }

        _timer -= Time.deltaTime;

        if(_timer <= 0) {
            _timer = _timeToDisable;
            gameObject.transform.SetParent(_parent);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move() {
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * _speed, _rigidbody2D.velocity.y);
    }
}
