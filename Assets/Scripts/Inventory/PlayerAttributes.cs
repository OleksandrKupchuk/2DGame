using UnityEngine;

public class PlayerAttributes : MonoBehaviour {
    [SerializeField]
    private AttributeHealthUI _attributeHealthUI;

    public float ResultHealth { get => _attributeHealthUI.Value; }
}