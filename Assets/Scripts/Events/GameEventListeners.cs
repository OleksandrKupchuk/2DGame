using UnityEngine;
using UnityEngine.Events;

public class GameEventListeners : MonoBehaviour {
    [SerializeField]
    private GameEvent _gameEvent;
    [SerializeField]
    private UnityEvent _responce;

    private void OnEnable() {
        _gameEvent.AddListener(this);
    }

    private void OnDisable() {
        _gameEvent.RemoveListener(this);
    }

    public void RaiseEvent() {
        _gameEvent.Raise();
    }
}
