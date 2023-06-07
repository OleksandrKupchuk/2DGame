using UnityEngine;
using UnityEngine.InputSystem;

public class CheckItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        //Debug.Log("check item");
    }

    public void Exit() {
        _controller.Cursor.ItemTooltip.DisableAttributes();
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();


        if (Mouse.current.leftButton.wasPressedThisFrame) {
            _controller.Cursor.StartRaycast();

            if (_controller.Cursor.RaycastHit2D.transform == null) {
                return;
            }

            if (_controller.Cursor.Cell == null) {
                Debug.Log("cell component is null");
                return;
            }

            if (!_controller.Cursor.Cell.HasItem) {
                Debug.Log("cell not have item");
                return;
            }

            _controller.Cursor.SetItem(_controller.Cursor.Cell.Item);
            if (_controller.Cursor.TryGetPlayerSlotComponentAndCallEvent()) {
                EventManager.TakeAwayItemEventHandler(_controller.Cursor.Item);
            }

            _controller.Cursor.Cell.DisableIcon();
            _controller.Cursor.Cell.PutItem(null);
            _controller.ChangeState(_controller.RaisedItemState);
        }
    }
}