using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {
    private Inventory _inventory;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Canvas _canvas;

    public Item Item { get; private set; }
    public IDragDropState CurrentState { get; private set; }
    public RaycastHit2D RaycastHit2D { get; private set; }
    public LayerMask LayerMask { get; }
    public Player Player { get; private set; }

    public void ChangeState(IDragDropState newState) {
        if (CurrentState != null) {
            CurrentState.Exit();
        }
        CurrentState = newState;
        CurrentState.Enter(this);
    }

    private void Awake() {
        _inventory = FindObjectOfType<Inventory>();
        DisableIcon();
    }

    private void Start() {
        Player = FindObjectOfType<Player>();
        ChangeState(new CheckItemState());
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

    public void SetAndEnableIcon(Sprite icon) {
        _icon.sprite = icon;
        EnableIcon();
    }

    public void DisableIcon() {
        _icon.enabled = false;
    }

    public void EnableIcon() {
        _icon.enabled = true;
    }

    public void SetItem(Item item) {
        Item = item;
    }

    public void FollowTheMouse() {
        Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(_mousePosition.x, _mousePosition.y, 0);
        RaycastHit2D = Physics2D.Raycast(_mousePosition, Vector3.forward, 100f, _layerMask);
    }
}