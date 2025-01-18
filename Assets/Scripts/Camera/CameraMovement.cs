using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField]
    private float _speedSmooth2;

    private Vector3 _cameraPosition;
    private Player _player;
    private Vector3 _offset = new Vector3(4f, 7f, -10f);
    private float _speedSmooth = 2f;

    private void Start() {
        _player = FindFirstObjectByType<Player>();
    }

    void FixedUpdate() {
        if(_player == null) {
            return;
        }

        _cameraPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, _offset.z);

        if (_player.PlayerMovement.IsLookingLeft) {
            _cameraPosition = new Vector3(_player.transform.position.x - _offset.x, _player.transform.position.y + _offset.y, _offset.z);
        }
        else {
            _cameraPosition = new Vector3(_player.transform.position.x + _offset.x, _player.transform.position.y + _offset.y, _offset.z);

        }

        transform.position = Vector3.Lerp(transform.position, _cameraPosition, _speedSmooth * Time.deltaTime);

        //just for test
        if (_speedSmooth2 != 0) {
            transform.position = new Vector3(transform.position.x + _speedSmooth2, transform.position.y, transform.position.z);
        }
    }
}
