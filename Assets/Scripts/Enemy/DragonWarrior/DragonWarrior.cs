using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWarrior : Enemy
{
    [SerializeField]
    private GameObject _fireBallPrefab;
    [SerializeField]
    private Transform _parentFireBalls;
    [SerializeField]
    private Transform _shotPoint;

    public List<GameObject> FireBallsPrefabs { get; private set; } = new List<GameObject>();
    public List<FireBall> FireBalls { get; private set; } = new List<FireBall>();
    public Transform ShotPoint { get => _shotPoint; }

    public StateMachine<DragonWarrior> StateMachine { get; private set; }
    public DragonWarriorIdleState IdleState = new DragonWarriorIdleState();
    public DragonWarriorRunState RunState = new DragonWarriorRunState();
    public DragonWarriorAttack AttckState = new DragonWarriorAttack();

    private new void Awake() {
        base.Awake();
        StateMachine = new StateMachine<DragonWarrior>(this);
        FireBallsPrefabs = CreateAndGetListFireBalls(_fireBallPrefab, _parentFireBalls);
    }

    private void Start()
    {
        StateMachine.ChangeState(IdleState);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }
}
