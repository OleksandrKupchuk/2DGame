using UnityEngine;
using UnityEngine.InputSystem;

public class ItemUsagePanel : MonoBehaviour {
    private InputActionMap _actionMap;

    private RectTransform _rectTransform;
    [SerializeField]
    private UsageSlotView _usageSlotView;
    [SerializeField]
    private InputActionAsset _inputActionAsset;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _actionMap = _inputActionAsset.FindActionMap("UsagePanel");
        CreateUsageSlots();
        //SetPositionToLeftBottomCorner();
    }

    private void OnEnable() {
        _actionMap.Enable();
    }

    private void OnDisable() {
        _actionMap.Disable();
    }

    private void CreateUsageSlots() {
        foreach (var action in _actionMap.actions) {
            UsageSlotView _usageSlotObject = Instantiate(_usageSlotView, transform);
            _usageSlotObject.PutItem(null);
            //_usageSlotObject.transform.SetParent(transform, false);
            //_usageSlotObject.SetInputAction(action);
        }
    }

    private void SetPositionToLeftBottomCorner() {
        _rectTransform.anchoredPosition = new Vector2(0f, 0f);
        _rectTransform.anchorMin = new Vector2(0, 0);
        _rectTransform.anchorMax = new Vector2(0, 0);
        _rectTransform.pivot = new Vector2(0f, 0f);
    }
}
