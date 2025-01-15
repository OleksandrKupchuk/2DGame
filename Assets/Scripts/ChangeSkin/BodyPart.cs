using UnityEngine;

public enum BodyType {
    BODY,
    HIP,
    ANOTHER
}

public class BodyPart : MonoBehaviour {
    private Sprite _sprite;

    [SerializeField]
    private BodyType _type = new BodyType();
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _sprite = _spriteRenderer.sprite;
    }

    public BodyType Type { get => _type; }

    public void ChangeSkin(ItemData item) {
        _spriteRenderer.sprite = item.Icon;
    }

    public void ResetSkin(ItemData item) {
        _spriteRenderer.sprite = _sprite;
    }
}
