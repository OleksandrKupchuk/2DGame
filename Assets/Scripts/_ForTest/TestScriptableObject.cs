using UnityEngine;

public class TestScriptableObject : MonoBehaviour {
    [SerializeField]
    private Attribute _attribute;

    void Start() {
        if (_attribute.icon != null) {
            Debug.Log("name sprite = " + _attribute.icon);
        }
        else {
            Debug.Log("icon is null");
        }
    }
}
