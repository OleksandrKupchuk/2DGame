using UnityEngine;
using UnityEngine.InputSystem;

public class UsageItemsPanel : MonoBehaviour {
    private InputActionMap _actionMap;

    [SerializeField]
    private UsageSlotView _usageSlotView;
    [SerializeField]
    private InputActionAsset _inputActionAsset;

    private void Awake() {
        _actionMap = _inputActionAsset.FindActionMap("UsagePanel");
        CreateUsageSlots();
    }

    private void CreateUsageSlots() {
        foreach (var action in _actionMap.actions) {
            UsageSlotView _usageSlotObject = Instantiate(_usageSlotView, transform);
            _usageSlotObject.PutItem(null);
            _usageSlotObject.SetInputAction(action);
        }
    }
}
