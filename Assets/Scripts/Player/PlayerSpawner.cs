using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    [SerializeField]
    private Player _player;

    public static event Action<Player> OnPlayerSpawned;

    private void Awake() {
        Player _playerObject = Instantiate(_player, gameObject.transform.position, Quaternion.identity);
        OnPlayerSpawned?.Invoke(_playerObject);
    }
}
