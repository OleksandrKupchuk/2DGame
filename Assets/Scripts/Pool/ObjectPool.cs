using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> where T : Component {
    private readonly int AMOUNT_OBJECTS_BY_DEFAULT = 5;
    private List<T> _objects = new List<T>();
    private T _prefab;
    private Transform _parent;

    public List<T> Objects { get => _objects; }

    public ObjectPool(T prefab, Transform parent) {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < AMOUNT_OBJECTS_BY_DEFAULT; i++) {
            T newObject = Object.Instantiate(_prefab, parent);
            newObject.gameObject.name += $" {i}";
            newObject.gameObject.SetActive(false);
            _objects.Add(newObject);
        }
    }

    public T GetEnabled() {
        T _instance = null;

        foreach (T newObject in _objects) {
            if (!newObject.gameObject.activeSelf) {
                _instance = newObject;
                _instance.gameObject.SetActive(true);
                return _instance;
            }
        }

        if (_instance == null) {
            _instance = Object.Instantiate(_prefab, _parent);
            _objects.Add(_instance);
        }

        _instance.gameObject.SetActive(true);
        return _instance;
    }

    public void PutAndDisabled(T instance) {
        if (instance.gameObject.activeSelf) {
            instance.gameObject.SetActive(false);
        }
    }

    public void DisabledAllObjects() {
        foreach (T instance in _objects) {
            if (instance.gameObject.activeSelf) {
                instance.gameObject.SetActive(false);
            }
        }
    }
}
