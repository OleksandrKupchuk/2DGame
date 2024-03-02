using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffIndicator : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _border;

    public void Display(Item item) {
        SetIcon(item.Icon);
        StartCoroutine(ShowDurationEffect(item.Attributes[0].duration));
    }

    private void SetIcon(Sprite icon) {
        _icon.sprite = icon;
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
