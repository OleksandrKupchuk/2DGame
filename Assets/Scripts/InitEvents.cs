using System.Collections.Generic;
using UnityEngine;

public class InitEvents : MonoBehaviour
{
    [SerializeField]
    private List<EnemyOfMelee> _enemy = new List<EnemyOfMelee>();

    private void Awake() {
        InitEnemyEvents();
    }

    private void InitEnemyEvents() {
        for (int i = 0; i < _enemy.Count; i++) {
            _enemy[i].AddEnableAttackCoolliderEventForAttackAnimation();
            _enemy[i].AddDisableAttackCoolliderEventForAttackAnimation();
        }
    }
}
