using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckItem : IDragDrop {
    private Cursor _cursor;

    public void Enter(Cursor cursor) {
        _cursor = cursor;
        Debug.Log("check item");
    }

    public void Exit() {
    }

    public void Update() {
        _cursor.FollowTheMouse();

        if (_cursor.RaycastHit2D.transform == null) {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("name obj = " + _cursor.RaycastHit2D.transform);
            Cell _cell = _cursor.RaycastHit2D.transform.GetComponent<Cell>();
            if (_cell == null || _cell.IsEmptyCell) {
                Debug.Log("cell is null or empty");
                return;
            }

            _cursor.SetAndEnableIcon(_cell.Item.Icon);
            _cursor.SetItem(_cell.Item);

            _cell.DisableIcon();
            _cell.SetItem(null);
            _cursor.ChangeState(new RaisedItem());
        }
    }
}
