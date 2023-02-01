using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttckState : EnemyMeleeAttackState
{
    private EnemyOfRange _enemyRange;

    public override void Enter(EnemyOfMelee owner) {
        _enemyRange = (EnemyOfRange)owner;
    }
}
