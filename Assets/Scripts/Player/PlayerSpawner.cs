using System;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    [SerializeField]
    private Player _player;

    public static event Action<Player> OnPlyerSpawned;

    private void Awake() {
        Player _playerObject = Instantiate(_player, gameObject.transform.position, Quaternion.identity);
        OnPlyerSpawned?.Invoke(_playerObject);
    }
}