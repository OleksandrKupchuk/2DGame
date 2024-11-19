using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private Cursor _cursor;
    private List<InventoryCell> _cells = new List<InventoryCell>();

    [SerializeField]
    private InventoryCell _cellPrefab;
    [SerializeField]
    private int _amountCells;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private Button _closeButton;

    public bool IsOpen { get => _background.activeSelf; }

    private void Awake() {
        if (_cellPrefab == null) {
            Debug.LogError("object is null");
            return;
        }
        if (_bag == null) {
            Debug.LogError("object is null");
            return;
        }
        if (_background == null) {
            Debug.LogError("object is null");
            return;
        }
        if (_closeButton == null) {
            Debug.LogError("object is null");
            return;
        }

        _cursor = FindObjectOfType<Cursor>();
        CheckItemInCloseButton();
    }

    private void OnEnable() {
        //EventManager.InventoryOpenlyClosed += Open;
        GenerateCellsOfPlayerBag();
        _background.SetActive(false);
    }

    private void OnDestroy() {
        //EventManager.InventoryOpenlyClosed -= Open;
    }

    private void CheckItemInCloseButton() {
        _closeButton.onClick.AddListener(() => {
            CheckItemInCursorAndPutOnInInventory();
            Close();
        });
    }

    private void CheckItemInCursorAndPutOnInInventory() {
        if (_cursor.Item != null) {
            AddItem(_cursor.Item);
            _cursor.RemoveItem();
            DragDropController.DropPutItem();
        }
    }

    private void GenerateCellsOfPlayerBag() {
        for (int i = 0; i < _amountCells; i++) {
            InventoryCell _cellObject = Instantiate(_cellPrefab);
            _cellObject.transform.SetParent(_bag);
            _cellObject.transform.localScale = new Vector3(1, 1, 1);
            _cellObject.SetRectTransformPosition(new Vector3(_bag.transform.position.x, _bag.transform.position.y, _bag.transform.position.z));
            _cells.Add(_cellObject);
        }
    }

    public void Open() {
        //print("active");
        //_closeButton.gameObject.SetActive(!_closeButton.gameObject.activeSelf);
        //_background.SetActive(!_background.activeSelf);
        _closeButton.gameObject.SetActive(true);
        _background.SetActive(true);
    }

    public void Close() {
        //print("active");
        _closeButton.gameObject.SetActive(false);
        _background.SetActive(false);
    }

    public void AddItem(Item item) {
        foreach (InventoryCell cell in _cells) {
            if (!cell.HasItem) {
                cell.SetItem(item);
                item.Disable();
                return;
            }
        }
    }
}
