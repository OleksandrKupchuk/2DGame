using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Vector3 _cameraPosition;
    private Player _player;

    private Vector3 _offset = new Vector3(4f, 7f, -10f);
    private float _speedSmooth = 2f;

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
