using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaisedItem : IDragDrop {
    private Cursor _cursor;

    public void Enter(Cursor cursor) {
        _cursor = cursor;
    }

    public void Exit() {
    }

    public void Update() {
        _cursor.FollowTheMouse();

        if (Mouse.current.leftButton.wasPressedThisFrame) {

            if (_cursor.RaycastHit2D.transform == null) {
                return;
            }

            Debug.Log("name obj = " + _cursor.RaycastHit2D.transform);
            Cell _cell = _cursor.RaycastHit2D.transform.GetComponent<Cell>();

            if (_cell == null) {
                Debug.Log("cell is null");
                return;
            }

            if (!_cell.IsEmptyCell) {
                Debug.Log("cell not empty");
                return;
            }

            _cell.SetItem(_cursor.Item);
            _cell.SetAndEnableIcon(_cursor.Item.Icon);

            _cursor.DisableIcon();
            _cursor.SetItem(null);
            _cursor.ChangeState(new CheckItem());
        }
    }
}
