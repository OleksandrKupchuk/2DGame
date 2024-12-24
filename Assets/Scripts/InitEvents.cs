using UnityEngine;

public class InitEvents : MonoBehaviour {
    [SerializeField]
    private EnemyDragorWarrior _enemyDragon;
    [SerializeField]
    private EnemyKnight _enemyKnight;
    [SerializeField]
    private EnemyRogue _enemyRogue;

    private void Awake() {
        InitEnemyDragonWarriorEvents();
        InitEnemyKnightEvents();
        InitEnemyRogueEvents();
    }

    private void InitEnemyDragonWarriorEvents() {
        if (_enemyDragon == null) {
            Debug.LogError($"Object {nameof(EnemyDragorWarrior)} is null");
        }
        _enemyDragon.AddEnableFireBallEventForAttackAnimation();
        _enemyDragon.AddEnableStrikeColliderForStrikeAnimation();
        _enemyDragon.AddCanMoveStrikeTrue();
    }

    private void InitEnemyKnightEvents() {
        if (_enemyKnight == null) {
            Debug.LogError($"Object {nameof(EnemyKnight)} is null");
        }
        _enemyKnight.AddEnableAttackColliderForAttackAnimation();
        _enemyKnight.AddDisableAttackCoolliderEventForAttackAnimation();
        _enemyKnight.AddEnableFireBallEventForCastAnimation();
        _enemyKnight.AddEnableStrikeColliderForStrikeAnimation();
    }

    private void InitEnemyRogueEvents() {
        if (_enemyRogue == null) {
            Debug.LogError($"Object {nameof(EnemyRogue)} is null");
        }
        _enemyRogue.AddEnableAttackLeftHandUpColliderForAttackLeftHandUpAnimation();
        _enemyRogue.AddEnableAttackRightHandUpColliderForAttackLeftHandUpAnimation();
    }
}
