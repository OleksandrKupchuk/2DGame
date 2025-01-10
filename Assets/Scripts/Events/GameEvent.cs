using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent/GameEvent")]
public class GameEvent : ScriptableObject {
    private List<GameEventListeners> _listeners = new List<GameEventListeners>();

    public void Raise() {
        for (int i = _listeners.Count - 1; i >= 0; i--) {
            _listeners[i].RaiseEvent();
        }
    }

    public void AddListener(GameEventListeners listener) {
        if (!_listeners.Contains(listener)) {
            _listeners.Add(listener);
        }
    }

    public void RemoveListener(GameEventListeners listener) {
        _listeners.Remove(listener);
    }
}
