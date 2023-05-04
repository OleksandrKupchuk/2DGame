using System;
using UnityEngine;

public class DragDropController : MonoBehaviour {
    private Inventory _inventory;
    [SerializeField]
    private Cursor _cursor;

    [HideInInspector]
    public Cell cell;

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

    public void RaiseItem(Item item) {
        RaisedItemTrigger?.Invoke(item);
    }

    public void DropPutItem() {
        DropPutItemTrigger?.Invoke();
    }

    public void ChangeState(IDragDropState newState) {
        if (CurrentState != null) {
            CurrentState.Exit();
        }
        CurrentState = newState;
        CurrentState.Enter(this);
    }

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
            print("inventory not active");
            return;
        }

        if (CurrentState != null) {
            CurrentState.Update();
        }
    }
}