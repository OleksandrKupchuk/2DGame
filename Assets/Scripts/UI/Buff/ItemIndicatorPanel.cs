using System.Collections.Generic;
using UnityEngine;

public class ItemIndicatorPanel : MonoBehaviour {
    private List<BuffIndicator> _buffIndicators = new List<BuffIndicator>();
    [SerializeField]
    private BuffIndicator _indicator;

    private void Awake() {
        CreateBuffIndicators();
        EventManager.UseItem += ShowBuffIndacator;
        EventManager.ActionItemOver += HideBuffIndacator;
    }

    private void OnDestroy() {
        EventManager.UseItem -= ShowBuffIndacator;
        EventManager.ActionItemOver -= HideBuffIndacator;
    }

    private void CreateBuffIndicators() {
        for (int i = 0; i < 5; i++) {
            BuffIndicator _indicatorObject = Instantiate(_indicator);
            _indicatorObject.transform.SetParent(transform, false);
            _indicatorObject.gameObject.SetActive(false);
            _buffIndicators.Add(_indicatorObject);
        }
    }

    private void ShowBuffIndacator(UsableItem item) {
        foreach (BuffIndicator indicator in _buffIndicators) {
            if (!indicator.gameObject.activeSelf) {
                indicator.gameObject.SetActive(true);
                indicator.Display(item);
                break;
            }
        }
    }

    private void HideBuffIndacator(Item item) {
        foreach (BuffIndicator indicator in _buffIndicators) {
            if (indicator.Item == item) {
                indicator.gameObject.SetActive(false);
                break;
            }
        }
    }
}
