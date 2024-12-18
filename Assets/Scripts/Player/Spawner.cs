using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private Player _player;
    [SerializeField]
    private CameraMovement _cameraMovement;
    [SerializeField]
    private Npc _npc;

    private void Awake() {
        GameObject _playerObject = Instantiate(_player.gameObject, gameObject.transform.position, Quaternion.identity);
        _cameraMovement.Init(_playerObject.GetComponent<Player>());
    }
}