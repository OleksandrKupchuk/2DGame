using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Vector3 _spawnPosition;

    [SerializeField]
    private Enemy _enemy;

    private void OnEnable() {
        _spawnPosition = gameObject.transform.position;
        Instantiate(_enemy.gameObject, _spawnPosition, Quaternion.identity);
    }
}
