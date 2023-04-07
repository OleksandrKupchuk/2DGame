using System.Collections.Generic;
using UnityEngine;

public class InitEvents : MonoBehaviour
{
    [SerializeField]
    private List<EnemyOfMelee> _enemyMelee = new List<EnemyOfMelee>();
    [SerializeField]
    private List<EnemyOfRange> _enemyRange = new List<EnemyOfRange>();
    [SerializeField]
    private EnemyDragorWarrior _enemyDragon;
    [SerializeField]
    private EnemyKnight _enemyKnight;
    [SerializeField]
    private EnemyRogue _enemyRogue;

    private void Awake() {
        InitEnemyOfMeleeEvents();
        InitEnemyOfRangeEvents();
        InitEnemyDragonWarrior();
        InitEnemyKnight();
        InitEnemyRogue();
    }

    private void InitEnemyOfMeleeEvents() {
        for (int i = 0; i < _enemyMelee.Count; i++) {
            //_enemyMelee[i].AddEnableAttackCoolliderEventForAttackAnimation();
            //_enemyMelee[i].AddDisableAttackCoolliderEventForAttackAnimation();
        }
    }

    private void InitEnemyOfRangeEvents() {
        for (int i = 0; i < _enemyRange.Count; i++) {
            //_enemyRange[i].AddEnableFireBallEventForAttackAnimation();
        }
    }

    private void InitEnemyDragonWarrior() {
        if(_enemyDragon == null) {
            Debug.LogError("Object EnemyDragon.name is null");
        }
        _enemyDragon.AddEnableFireBallEventForAttackAnimation();
        _enemyDragon.AddEnableStrikeColliderForStrikeAnimation();
        _enemyDragon.AddCanMoveStrikeTrue();
    }

    private void InitEnemyKnight() {
        if (_enemyKnight == null) {
            Debug.LogError("Object EnemyKnight is null");
        }
        _enemyKnight.AddEnableAttackColliderForAttackAnimation();
        _enemyKnight.AddDisableAttackCoolliderEventForAttackAnimation();
        _enemyKnight.AddEnableFireBallEventForCastAnimation();
        _enemyKnight.AddEnableStrikeColliderForStrikeAnimation();
    }

    private void InitEnemyRogue() {
        if (_enemyRogue == null) {
            Debug.LogError("Object EnemyRogue is null");
        }
        _enemyRogue.AddEnableAttackLeftHandUpColliderForAttackLeftHandUpAnimation();
        _enemyRogue.AddEnableAttackRightHandUpColliderForAttackLeftHandUpAnimation();
    }
}
