using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private Collider2D _collider;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        Debug.Log(_collider.gameObject.name);
    }

    void Update()
    {
        
    }
}
