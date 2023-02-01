using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : IState<EnemyOfMelee> {

    protected EnemyOfMelee _enemy;
    public virtual void Enter(EnemyOfMelee owner) {
        _enemy = owner;
    }

    public virtual void ExecuteUpdate() {
    }

    public virtual void ExecuteFixedUpdate() {
    }

    public virtual void Exit() {
    }
}
