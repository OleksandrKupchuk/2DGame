using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Vector3 _spawnPosition;

    [SerializeField]
    private Player _player;
    [SerializeField]
    private CameraMovement _cameraMovement;

    private void OnEnable() {
        _spawnPosition = gameObject.transform.position;
        GameObject _playerObject =  Instantiate(_player.gameObject, _spawnPosition, Quaternion.identity);
        _cameraMovement.Init(_playerObject.GetComponent<Player>());
    }
}