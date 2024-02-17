using UnityEngine;

public class PutItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        Debug.Log("put item enter");
        _controller = controller;

        Cell _cell = _controller.Cursor.GetCell();
        _cell.SetItem(_controller.Cursor.Item);

        if (_cell.HasItem) {
            _controller.Cursor.OnTriggerEnter2D(_cell.BoxCollider2D);
            _controller.Cursor.SetItem(null);
            _controller.ChangeState(_controller.CheckItemState);
        }
        else {
            _controller.ChangeState(_controller.RaisedItemState);
        }
    }

    public void Exit() {
        Debug.Log("put item exit");
    }

    public void Update() {
    }
}
