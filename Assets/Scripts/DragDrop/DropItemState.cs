using System;
using UnityEngine;

public class DropItemState : IDragDropState {
    private Cursor _cursor;
    public static event Action<Item> DropItem;

    public void Enter(Cursor cursor) {
        _cursor = cursor;
        DropItem(_cursor.Item);
        SpawnItem();
        _cursor.DisableIcon();
        _cursor.SetItem(null);
        _cursor.ChangeState(new CheckItemState());
    }

    public void Update() {
    }

    public void Exit() {
    }

    private void SpawnItem() {
        _cursor.Item.transform.position = new Vector3(_cursor.Player.transform.position.x - (5 * _cursor.Player.transform.localScale.x), _cursor.Player.transform.position.y + 2);
        _cursor.Item.gameObject.SetActive(true);
    }
}
