using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChangeSkin : MonoBehaviour {
    [SerializeField]
    private List<BodyPart> _bodyParts = new List<BodyPart>();
    [SerializeField]
    private List<SpriteResolver> _spriteResolvers = new List<SpriteResolver>();

    private void Awake() {
        EventManager.PutOnItem += Chnage;
        EventManager.TakeAwayItem += Reset;
    }

    private void OnDestroy() {
        EventManager.PutOnItem -= Chnage;
        EventManager.TakeAwayItem -= Reset;
    }

    public void Chnage(Item item) {
        foreach(BodyPart bodyPart in _bodyParts) {
            if(item.BodyType == bodyPart.Type) {
                bodyPart.ChangeSkin(item);
            }
        }
    }

    public void Reset(Item item) {
        foreach (BodyPart bodyPart in _bodyParts) {
            if (item.BodyType == bodyPart.Type) {
                bodyPart.ResetSkin(item);
            }
        }
    }
}
