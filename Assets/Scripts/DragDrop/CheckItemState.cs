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
        _controller.Cursor.StartRaycast();

        if (_controller.Cursor.RaycastHit2D.transform == null) {
            return;
        }

        if (_controller.Cursor.Cell != null && _controller.Cursor.Cell.HasItem) {
            _controller.Cursor.ItemTooltip.ShowAttributes(_controller.Cursor.Cell.Item, _controller.Cursor.Cell.transform.position, _controller.Cursor.Cell.RectTransform.rect.height);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) {
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

            _controller.Cursor.TryGetPlayerSlotComponentAndCallEvent(EventManager.PutOnOrTakenAwakeItemEventHandler);
        }
    }
}
