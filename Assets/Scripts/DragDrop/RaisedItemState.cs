using UnityEngine;
using UnityEngine.InputSystem;

public class RaisedItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        DragDropController.RaiseItem(_controller.Cursor.Item);
        Debug.Log("raised item enter");
    }

    public void Exit() {
        DragDropController.DropPutItem();
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();
        Debug.Log("raised item update");

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Cell _cell = _controller.Cursor.GetCell();

            if (_cell == null) {
                _controller.ChangeState(_controller.DropItemState);
                Debug.Log("object null");
                return;
            }

            if (CanSwap(_cell)) {
                Debug.Log($"cell have item {_cell.Item.name}");
                _controller.ChangeState(_controller.SwapItemState);
            }
            else {
                _controller.ChangeState(_controller.PutItemState);
            }
        }
    }

    private bool CanSwap(Cell cell) {
        return cell.HasItem && _controller.Cursor.HasItem;
    }
}
