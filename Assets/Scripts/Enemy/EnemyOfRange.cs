using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOfRange : EnemyOfMelee
{
    public override EnemyMeleeAttackState AttckState { get; protected set; } = new EnemyRangeAttckState();

    protected new void Awake() {
        base.Awake();
        FireBallsPrefabs = CreateAndGetListFireBalls(_fireBallPrefab, _parentFireBalls);
    }
}
