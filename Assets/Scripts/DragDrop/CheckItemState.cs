using UnityEngine;
using UnityEngine.InputSystem;

public class CheckItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        //Debug.Log("check item");
    }

    public void Exit() {
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            _controller.Cursor.StartRaycast();

            if (_controller.Cursor.RaycastHit2D.transform == null) {
                return;
            }
            Cell _cell = _controller.Cursor.RaycastHit2D.transform.GetComponent<Cell>();
            if (_cell == null) {
                Debug.Log("cell component is null");
                return;
            }
            if (!_cell.HasItem) {
                Debug.Log("cell not have item");
                return;
            }

            _controller.Cursor.SetAndEnableIcon(_cell.Item.Icon);
            _controller.Cursor.SetItem(_cell.Item);

            _cell.DisableIcon();
            _cell.SetItem(null);
            _controller.ChangeState(_controller.RaisedItemState);

            _controller.Cursor.TryGetPlayerSlotComponentAndCallEvent();
        }
    }
}
