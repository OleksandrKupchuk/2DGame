using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private readonly int AMOUNT_OBJECTS_BY_DEFAULT = 5;
    private List<GameObject> _objects = new List<GameObject>();
    private GameObject _prefab;

    public ObjectPool(GameObject prefab) {
        _prefab = prefab;

        for (int i = 0; i  < AMOUNT_OBJECTS_BY_DEFAULT; i++) {
            GameObject newObject = Instantiate(_prefab);
            newObject.SetActive(false);
            _objects.Add(newObject);
        }
    }

    public GameObject Get() {
        if (_objects.Count == 0) {
            GameObject _newObject = Instantiate(_prefab);
            _objects.Add( _newObject );
        }
        
        int _lastIndex = _objects.Count - 1;
        GameObject _instance = _objects[_lastIndex];
        _instance.SetActive(true);
        _objects.RemoveAt(_lastIndex);
        return _instance;
    }

    public void Put(GameObject instance) {
        instance.SetActive(false);
        _objects.Add( instance );
    }
}
