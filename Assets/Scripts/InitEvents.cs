using System.Collections.Generic;
using UnityEngine;

public class InitEvents : MonoBehaviour
{
    [SerializeField]
    private List<EnemyOfMelee> _enemyMelee = new List<EnemyOfMelee>();
    [SerializeField]
    private List<EnemyOfRange> _enemyRange = new List<EnemyOfRange>();

    private void Awake() {
        InitEnemyOfMeleeEvents();
        InitEnemyOfRangeEvents();
    }

    private void InitEnemyOfMeleeEvents() {
        for (int i = 0; i < _enemyMelee.Count; i++) {
            //_enemyMelee[i].AddEnableAttackCoolliderEventForAttackAnimation();
            //_enemyMelee[i].AddDisableAttackCoolliderEventForAttackAnimation();
        }
    }

    private void InitEnemyOfRangeEvents() {
        for (int i = 0; i < _enemyRange.Count; i++) {
            _enemyRange[i].AddEnableFireBallEventForAttackAnimation();
        }
    }
}
