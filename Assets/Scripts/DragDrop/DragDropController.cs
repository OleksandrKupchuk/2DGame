using System;
using UnityEngine;

public class DragDropController : MonoBehaviour {
    private Inventory _inventory;
    [SerializeField]
    private Cursor _cursor;

    public CheckItemState CheckItemState { get; private set; }
    public RaisedItemState RaisedItemState { get; private set; }
    public PutItemState PutItemState { get; private set; }
    public DropItemState DropItemState { get; private set; }
    public SwapItemState SwapItemState { get; private set; }

    public IDragDropState CurrentState { get; private set; }
    public Cursor Cursor { get => _cursor; }
    public Player Player { get; private set; }

    public static event Action<Item> RaisedItemTrigger;
    public static event Action DropPutItemTrigger;

    private void Awake() {
        _inventory = FindObjectOfType<Inventory>();
        CheckItemState = new CheckItemState();
        RaisedItemState = new RaisedItemState();
        PutItemState = new PutItemState();
        DropItemState = new DropItemState();
        SwapItemState = new SwapItemState();
    }

    private void Start() {
        Player = FindObjectOfType<Player>();
        ChangeState(CheckItemState);
    }

    private void Update() {
        if (!_inventory.transform.GetChild(0).transform.gameObject.activeSelf) {
            //print("inventory not active");
            return;
        }

        if (CurrentState != null) {
            CurrentState.Update();
        }
    }

    public static void RaiseItem(Item item) {
        Equipment _equipment = item as Equipment;
        if(_equipment == null) {
            print("equipment is null");
            return;
        }
        RaisedItemTrigger?.Invoke(_equipment);
    }

    public static void DropPutItem() {
        DropPutItemTrigger?.Invoke();
    }

    public void ChangeState(IDragDropState newState) {
        if (CurrentState != null) {
            CurrentState.Exit();
        }
        CurrentState = newState;
        CurrentState.Enter(this);
    }
}