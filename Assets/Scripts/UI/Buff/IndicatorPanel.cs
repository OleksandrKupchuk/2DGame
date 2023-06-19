using System.Collections.Generic;
using UnityEngine;

public class IndicatorPanel : MonoBehaviour {
    private List<Indicator> _indicators = new List<Indicator>();
    [SerializeField]
    private Indicator _indicator;

    private void Awake() {
        CreateIndicators();
        EventManager.UsePotion += Show;
    }

    private void OnDestroy() {
        EventManager.UsePotion -= Show;
    }

    private void CreateIndicators() {
        for (int i = 0; i < 5; i++) {
            Indicator _indicatorObject = Instantiate(_indicator);
            _indicatorObject.transform.SetParent(transform, false);
            _indicatorObject.gameObject.SetActive(false);
            _indicators.Add(_indicatorObject);
        }
    }

    private void Show(Potion potion) {
        foreach (var indicator in _indicators) {
            if (!indicator.gameObject.activeSelf) {
                indicator.SetPotion(potion);
                StartCoroutine(indicator.ShowDurationEffect(potion.Duration));
                indicator.gameObject.SetActive(true);
                return;
            }
        }
    }
}