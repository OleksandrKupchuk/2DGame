using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private Player _player;
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

    private List<Cell> _cells = new List<Cell>();

    private void OnEnable() {
        GameManager.OpenCloseInventory += EnableDisableInventory;
        GenerateCellsOfPlayerBag();
    }

    private void OnDestroy() {
        GameManager.OpenCloseInventory -= EnableDisableInventory;
    }

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
        if(_closeButton == null) {
            Debug.LogError("object is null");
            return;
        }

        AddListenerForCloseButton();
    }

    private void AddListenerForCloseButton() {
        _closeButton.onClick.AddListener(() => {
            _inventoryBackground.SetActive(false);
        });
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
            if (!cell.HasItem) {
                cell.SetAvailableForInteraction(true);
                cell.SetItem(item);
                cell.SetAndEnableIcon(item.Icon);
                cell.EnableIcon();
                item.gameObject.SetActive(false);
                return;
            }
        }
    }
}