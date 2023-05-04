using System;
using UnityEngine;

public class DropItemState : IDragDropState {
    private DragDropController _controller;
    public static event Action<Item> DropItem;

    public void Enter(DragDropController controller) {
        _controller = controller;
        DropItem(_controller.Cursor.Item);
        SpawnItem();
        _controller.Cursor.DisableIcon();
        _controller.Cursor.SetItem(null);
        _controller.ChangeState(_controller.CheckItemState);
    }

    public void Update() {
    }

    public void Exit() {
    }

    private void SpawnItem() {
        _controller.Cursor.Item.transform.position = new Vector3(_controller.Player.transform.position.x - (5 * _controller.Player.transform.localScale.x), _controller.Player.transform.position.y + 2);
        _controller.Cursor.Item.gameObject.SetActive(true);
    }
}