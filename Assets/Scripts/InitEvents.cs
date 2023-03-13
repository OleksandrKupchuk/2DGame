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

    private void Awake() {
        InitEnemyOfMeleeEvents();
        InitEnemyOfRangeEvents();
        InitEnemyDragonWarrior();
        InitEnemyKnight();
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
            Debug.LogError($"Object {_enemyDragon.name} is null");
        }
        _enemyDragon.AddEnableFireBallEventForAttackAnimation();
        _enemyDragon.AddEnableStrikeColliderForStrikeAnimation();
    }

    private void InitEnemyKnight() {
        if (_enemyKnight == null) {
            Debug.LogError($"Object {_enemyKnight.name} is null");
        }
        _enemyKnight.AddEnableAttackColliderForAttackAnimation();
        _enemyKnight.AddDisableAttackCoolliderEventForAttackAnimation();
        _enemyKnight.AddEnableFireBallEventForCastAnimation();
        _enemyKnight.AddEnableStrikeColliderForStrikeAnimation();
    }
}
