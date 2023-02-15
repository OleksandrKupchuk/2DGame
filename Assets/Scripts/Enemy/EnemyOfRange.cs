using UnityEngine;

public class EnemyOfRange : EnemyOfMelee
{
    private Player _player;
    public override EnemyMeleeAttackState AttckState { get; protected set; } = new EnemyRangeAttckState();

    protected new void Awake() {
        base.Awake();
        FireBallsPrefabs = CreateAndGetListFireBalls(_fireBallPrefab, _parentFireBalls);
    }

    private new void Start() {
        base.Start();
        _player = FindObjectOfType<Player>();
    }

    protected new void Update() {
        base.Update();
        //print("distance to player = " + Mathf.Abs(Vector2.Distance(transform.position, _player.transform.position)));
    }
}
