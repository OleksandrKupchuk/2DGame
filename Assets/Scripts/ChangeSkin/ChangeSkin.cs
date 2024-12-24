using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour {
    [SerializeField]
    private List<BodyPart> _bodyParts = new List<BodyPart>();

    private void Awake() {
        EventManager.PutOnItem += Change;
        EventManager.TakeAwayItem += Reset;
    }

    private void OnDestroy() {
        EventManager.PutOnItem -= Change;
        EventManager.TakeAwayItem -= Reset;
    }

    public void Change(Item item) {
        if (item is WearableItem) {
            WearableItem _item = item as WearableItem;

            foreach (BodyPart bodyPart in _bodyParts) {
                if (_item.BodyType == bodyPart.Type) {
                    bodyPart.ChangeSkin(_item);
                }
            }

        }
    }

    public void Reset(Item item) {
        if (item is WearableItem) {
            WearableItem _item = item as WearableItem;

            foreach (BodyPart bodyPart in _bodyParts) {
                if (_item.BodyType == bodyPart.Type) {
                    bodyPart.ResetSkin(_item);
                }
            }
        }
    }
}
