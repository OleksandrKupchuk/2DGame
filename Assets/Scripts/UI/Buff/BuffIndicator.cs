using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffIndicator : MonoBehaviour {
    private Item _item;

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _border;

    public Item Item => _item;

    public void Display(Item item) {
        _item = item;
        SetIcon(item.Icon);
        StartCoroutine(ShowDurationEffect(item.Duration));
    }

    private void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    private IEnumerator ShowDurationEffect(float duration) {
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
