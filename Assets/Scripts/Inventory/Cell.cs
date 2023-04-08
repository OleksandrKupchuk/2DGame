using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    [SerializeField]
    private CellContent _cellContentPrefab;
    [SerializeField]
    private Transform _cellContentParent;

    public bool IsEmptyCell { get => _cellContentParent.transform.childCount == 0; }

    public void CreateCellContentAndSetIcon(Sprite icon) {
        CellContent _cellContentObject = Instantiate(_cellContentPrefab);
        _cellContentObject.transform.SetParent(_cellContentParent);
        _cellContentObject.transform.localScale = new Vector3(1f, 1f, 1f);
        _cellContentObject.transform.position = _cellContentParent.transform.position;
        _cellContentObject.SetIcon(icon);
    }
}