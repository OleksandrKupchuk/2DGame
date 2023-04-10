using UnityEngine;
using UnityEngine.InputSystem;

public class CheckItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        Debug.Log("check item");
    }

    public void Exit() {
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();

        if (_controller.Cursor.RaycastHit2D.transform == null) {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("name obj = " + _controller.Cursor.RaycastHit2D.transform);
            Cell _cell = _controller.Cursor.RaycastHit2D.transform.GetComponent<Cell>();
            if (_cell == null || _cell.IsEmptyCell) {
                Debug.Log("cell is null or empty");
                return;
            }

            _controller.Cursor.SetAndEnableIcon(_cell.Item.Icon);
            _controller.Cursor.SetItem(_cell.Item);

            _cell.DisableIcon();
            _cell.SetItem(null);
            _controller.ChangeState(_controller.RaisedItemState);
        }
    }
}
