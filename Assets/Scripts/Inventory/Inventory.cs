using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField]
    private Cell _cellPrefab;
    [SerializeField]
    private int _amountCells;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _inventoryBackground;

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

    private void Start() {
        GameManager.OpenCloseInventory += EnableDisableInventory;
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
        print("active");
        _inventoryBackground.SetActive(!_inventoryBackground.activeSelf);
    }

    public void PutItemInEmptyCell(Item item) {
        foreach (Cell cell in _cells) {
            if (cell.IsEmptyCell) {
                cell.CreateCellContentAndSetIcon(item.Icon, _inventoryBackground.transform);
                item.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void OnDestroy() {
        GameManager.OpenCloseInventory -= EnableDisableInventory;
    }
}