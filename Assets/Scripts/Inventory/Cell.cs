using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler {
    [SerializeField]
    private CellContent _cellContentPrefab;

    public bool IsEmptyCell { get => transform.GetChild(0).childCount == 0; }

    public void CreateCellContentAndSetIcon(Item item, Transform dragParent) {
        CellContent _cellContentObject = Instantiate(_cellContentPrefab);
        _cellContentObject.transform.SetParent(transform);
        _cellContentObject.transform.localScale = new Vector3(1f, 1f, 1f);
        _cellContentObject.transform.position = transform.position;
        _cellContentObject.SetIcon(item.Icon);
        _cellContentObject.SetRegisteredItem(item);
        _cellContentObject.SetDragParent(dragParent);
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null) {
            eventData.pointerDrag.transform.position = transform.position;
            eventData.pointerDrag.transform.SetParent(transform);
        }
    }
}