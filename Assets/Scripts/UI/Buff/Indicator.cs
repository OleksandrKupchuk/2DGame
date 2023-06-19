using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour {
    private Potion _potion;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _border;

    public void SetPotion(Potion potion) {
        _potion = potion;

        if (_potion == null) {
            ActiveIcon(false);
        }
        else {
            SetIcon(potion.Icon);
            ActiveIcon(true);
        }
    }

    private void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    private void ActiveIcon(bool isActive) {
        _icon.enabled = isActive;
    }

    public IEnumerator ShowDurationEffect(float duration) {
        float _time = duration;

        while (_time > 0) {
            _time -= Time.deltaTime;
            _border.fillAmount = _time / duration;
            yield return null;
        }

        _border.fillAmount = 1;
        gameObject.SetActive(false);
    }
}