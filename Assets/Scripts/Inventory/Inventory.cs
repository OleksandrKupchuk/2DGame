using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private Cursor _cursor;
    [SerializeField]
    private Cell _cellPrefab;
    [SerializeField]
    private int _amountCells;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _inventoryBackground;
    [SerializeField]
    private Transform _canvas;

    private List<Cell> _cells = new List<Cell>();

    private void Awake() {
        if(_cellPrefab == null) {
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
    }

    private void OnEnable() {
        GameManager.OpenCloseInventory += EnableDisableInventory;
        _cursor = FindObjectOfType<Cursor>();
        GenerateCellsOfPlayerBag();
    }

    private void GenerateCellsOfPlayerBag() {
        for (int i = 0; i < _amountCells; i++) {
            Cell _cellObject = Instantiate(_cellPrefab);
            _cellObject.transform.SetParent(_bag);
            _cellObject.transform.localScale = new Vector3(1, 1, 1);
            _cells.Add(_cellObject);
        }
    }

    public void EnableDisableInventory() {
        //print("active");
        _inventoryBackground.SetActive(!_inventoryBackground.activeSelf);
    }

    public void PutItemInEmptyCell(Item item) {
        foreach (Cell cell in _cells) {
            if (cell.IsEmptyCell) {
                cell.SetItem(item);
                cell.SetAndEnableIcon(item.Icon);
                cell.EnableIcon();
                item.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void OnDestroy() {
        GameManager.OpenCloseInventory -= EnableDisableInventory;
    }
}