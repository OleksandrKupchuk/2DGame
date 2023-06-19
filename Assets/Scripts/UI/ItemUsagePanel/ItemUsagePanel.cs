using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemUsagePanel : MonoBehaviour {
    private RectTransform _rectTransform;
    [SerializeField]
    private ItemUsageSlot _usageSlot;
    [SerializeField]
    private List<InputActionReference> _inputActionReferences = new List<InputActionReference>();

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        CreateUsageSlots();
        SetPositionToLeftBottomCorner();
    }

    private void CreateUsageSlots() {
        foreach (var action in _inputActionReferences) {
            ItemUsageSlot _usageSlotObject = Instantiate(_usageSlot);
            _usageSlotObject.transform.SetParent(transform, false);
            _usageSlotObject.SetInputAction(action);
        }
    }

    private void SetPositionToLeftBottomCorner() {
        _rectTransform.anchoredPosition = new Vector2(0f, 0f);
        _rectTransform.anchorMin = new Vector2(0, 0);
        _rectTransform.anchorMax = new Vector2(0, 0);
        _rectTransform.pivot = new Vector2(0f, 0f);
    }
}