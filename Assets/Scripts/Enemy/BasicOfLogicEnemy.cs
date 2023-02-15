using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BasicOfLogicEnemy : BaseCharacteristics {
    protected float _xScale;

    [SerializeField]
    protected GameObject _fireBallPrefab;
    [SerializeField]
    protected Transform _parentFireBalls;
    [SerializeField]
    protected Transform _shotPoint;

    public float GetLocalScaleX { get => transform.localScale.x; }
    public List<GameObject> FireBallsPrefabs { get; protected set; } = new List<GameObject>();
    public List<FireBall> FireBalls { get; protected set; } = new List<FireBall>();
    public Transform ShotPoint { get => _shotPoint; }
    public FieldOfView FieldOfView { get; protected set; }
    public bool CanAttack { get; protected set; }
    public Player Target { get; protected set; }

    [HideInInspector]
    public float delayAttack = 0;

    protected new void Awake() {
        base.Awake();
        _xScale = transform.localScale.x;
    }

    public void Flip() {
        _xScale *= -1;
        gameObject.transform.localScale = new Vector3(_xScale, transform.localScale.y, transform.localScale.z);
    }

    public List<GameObject> CreateAndGetListFireBalls(GameObject prefab, Transform parentTransform) {
        List<GameObject> _fireBalls = new List<GameObject>();

        for (int i = 0; i < 5; i++) {
            GameObject _fireBall = Instantiate(prefab);
            _fireBall.transform.SetParent(parentTransform);
            _fireBall.SetActive(false);
            _fireBalls.Add(_fireBall);
        }

        return _fireBalls;
    }

    public void EnableFireBall(List<GameObject> fireBalls, Transform shotPoint, float direction) {
        foreach (var fireBall in fireBalls) {
            if (!fireBall.activeSelf) {
                fireBall.transform.position = shotPoint.transform.position;
                fireBall.transform.SetParent(null);
                fireBall.transform.localScale = new Vector3(direction, fireBall.transform.localScale.y, fireBall.transform.localScale.z);
                fireBall.SetActive(true);
                return;
            }
        }
    }

    public bool IsNeedLookOnPlayer() {
        if ((transform.position.x - FieldOfView.Target.transform.position.x) < 0 && transform.localScale.x == -1) {
            return true;
        }
        else if ((transform.position.x - FieldOfView.Target.transform.position.x) > 0 && transform.localScale.x == 1) {
            return true;
        }
        else {
            return false;
        }
    }
}
