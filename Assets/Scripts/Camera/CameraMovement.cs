using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Vector3 _cameraPosition;
    private Player _player;

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float _speedSmooth;

    public void Init(Player player) {
        _player = player;
    }

    void FixedUpdate() {

        _cameraPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, _offset.z);

        if (_player.IsLookingLeft) {
            _cameraPosition = new Vector3(_player.transform.position.x - _offset.x, _player.transform.position.y + _offset.y, _offset.z);
        }
        else {
            _cameraPosition = new Vector3(_player.transform.position.x + _offset.x, _player.transform.position.y + _offset.y, _offset.z);

        }

        transform.position = Vector3.Lerp(transform.position, _cameraPosition, _speedSmooth * Time.deltaTime);
    }
}
