using UnityEngine;
using UnityEngine.InputSystem;

public class CheckItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        Debug.Log("check item enter");
    }

    public void Exit() {
        _controller.Cursor.ItemTooltip.HideTooltip();
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();
        //Debug.Log("check item update");

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            ICell _cell = _controller.Cursor.GetCell();

            if (_cell == null) {
                Debug.Log("cell component is null");
                return;
            }

            if (_cell.HasItem) {
                _controller.Cursor.SetItem(_cell.Item);
                _cell.RemoveItem();
                _controller.ChangeState(_controller.RaisedItemState);
            }
            else {
                Debug.Log("cell not have item");
            }
        }
    }
}
