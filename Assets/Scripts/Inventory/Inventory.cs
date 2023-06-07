using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private Cursor _cursor;
    private List<Cell> _cells = new List<Cell>();

    [SerializeField]
    private Cell _cellPrefab;
    [SerializeField]
    private int _amountCells;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _inventoryBackground;
    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Button _closeButton;

    public bool IsOpen { get => _inventoryBackground.activeSelf; }

    private void OnEnable() {
        EventManager.InventoryOpenlyClosed += EnableDisableInventory;
        GenerateCellsOfPlayerBag();
    }

    private void OnDestroy() {
        EventManager.InventoryOpenlyClosed -= EnableDisableInventory;
    }

    private void Awake() {
        if (_cellPrefab == null) {
            Debug.LogError("object is null");
            return;
        }
        if (_bag == null) {
            Debug.LogError("object is null");
            return;
        }
        if (_inventoryBackground == null) {
            Debug.LogError("object is null");
            return;
        }
        if (_closeButton == null) {
            Debug.LogError("object is null");
            return;
        }

        _cursor = FindObjectOfType<Cursor>();
        AddListenerForCloseButton();
    }

    private void AddListenerForCloseButton() {
        _closeButton.onClick.AddListener(() => {
            CheckItemInCursorAndPutOnInInventory();
            _inventoryBackground.SetActive(false);
            _closeButton.gameObject.SetActive(false);
        });
    }

    private void GenerateCellsOfPlayerBag() {
        for (int i = 0; i < _amountCells; i++) {
            Cell _cellObject = Instantiate(_cellPrefab);
            _cellObject.transform.SetParent(_bag);
            _cellObject.transform.localScale = new Vector3(1, 1, 1);
            _cellObject.SetRectTransformPosition(new Vector3(_bag.transform.position.x, _bag.transform.position.y, _bag.transform.position.z));
            _cells.Add(_cellObject);
        }
    }

    public void EnableDisableInventory() {
        //print("active");
        _closeButton.gameObject.SetActive(!_closeButton.gameObject.activeSelf);
        _inventoryBackground.SetActive(!_inventoryBackground.activeSelf);
    }

    public void PutItemInEmptyCell(Item item) {
        foreach (Cell cell in _cells) {
            if (!cell.HasItem) {
                cell.PutItem(item);
                item.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void CheckItemInCursorAndPutOnInInventory() {
        if (_cursor.Item != null) {
            PutItemInEmptyCell(_cursor.Item);
            _cursor.SetItem(null);
            DragDropController.DropPutItem();
        }
    }
}