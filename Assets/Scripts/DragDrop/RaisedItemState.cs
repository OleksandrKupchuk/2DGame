using UnityEngine;
using UnityEngine.InputSystem;

public class RaisedItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;
        //_controller.Cursor.SetItem(_controller.Cursor.Cell.Item);
        _controller.RaiseItem(_controller.Cursor.Item);
    }

    public void Exit() {
        _controller.DropPutItem();
    }

    public void Update() {
        _controller.Cursor.FollowTheMouse();

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            _controller.Cursor.StartRaycast();

            if (_controller.Cursor.RaycastHit2D.transform == null) {
                _controller.ChangeState(_controller.DropItemState);
                Debug.Log("object null");
                return;
            }

            if (_controller.Cursor.Cell == null) {
                return;
            }

            if (!_controller.Cursor.Cell.IsAvailableForInteraction) {
                Debug.Log("cell not avaible for iteraction");
                return;
            }

            if (_controller.Cursor.Cell.HasItem) {
                Debug.Log("cell not empty");
                _controller.ChangeState(_controller.SwapItemState);
                return;
            }

            _controller.ChangeState(_controller.PutItemState);
        }
    }
}