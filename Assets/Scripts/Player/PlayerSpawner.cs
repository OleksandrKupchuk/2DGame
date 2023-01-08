using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Vector3 _spawnPosition;

    private void Awake() {
        Instantiate(_player.gameObject, _spawnPosition, Quaternion.identity);
    }
}
