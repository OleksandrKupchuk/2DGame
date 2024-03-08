using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour {
    private RectTransform _backgroundRectTransform;
    private List<AttributeTooltip> _attributeTooltips = new List<AttributeTooltip>();

    [SerializeField]
    private Image _imageBackground;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private AttributeTooltip _attributePrefab;

    private void Awake() {
        _backgroundRectTransform = _background.GetComponent<RectTransform>();

        if (_background == null) {
            Debug.LogError("Object 'background' is null");
        }

        _imageBackground.enabled = false;
        CreateAttributeTooltips();
    }

    private void CreateAttributeTooltips() {
        for (int i = 0; i < 5; i++) {
            AttributeTooltip _attributeTooltip = Instantiate(_attributePrefab);
            _attributeTooltip.transform.SetParent(_background.transform);
            _attributeTooltip.transform.localScale = new Vector3(1, 1, 1);
            _attributeTooltip.gameObject.SetActive(false);
            _attributeTooltips.Add(_attributeTooltip);
        }
    }

    public void ShowTooltip(Item item, Vector2 positionCell, float heightCell) {
        InitAttributes(item);
        EnableImageBackground();
        StartCoroutine(SetPosition(positionCell, heightCell));
    }

    public void HideTooltip() {
        foreach (AttributeTooltip attributeTooltip in _attributeTooltips) {
            attributeTooltip.gameObject.SetActive(false);
        }

        DisableImageBackground();
    }

    private void InitAttributes(Item item) {
        for (int i = 0; i < item.Attributes.Count; i++) {
            _attributeTooltips[i].SetValue(item.GetAttributeString(item.Attributes[i]));
            _attributeTooltips[i].SetIcon(item.Attributes[i].icon);
            _attributeTooltips[i].gameObject.SetActive(true);
        }
    }

    private IEnumerator SetPosition(Vector2 pos, float height) {
        _background.transform.position = pos;
        float _spaceBetweenTooltipAndCellInPixel = 10;
        yield return StartCoroutine(WaitForEnableBackground());
        float _heightInPixel = (_backgroundRectTransform.rect.height + height) / 2 + _spaceBetweenTooltipAndCellInPixel;
        _backgroundRectTransform.anchoredPosition = new Vector2(_backgroundRectTransform.anchoredPosition.x, _backgroundRectTransform.anchoredPosition.y + _heightInPixel);
    }

    private IEnumerator WaitForEnableBackground() {
        yield return new WaitUntil(() => _imageBackground.enabled = true);
    }

    private void EnableImageBackground() {
        _imageBackground.enabled = true;
    }

    private void DisableImageBackground() {
        _imageBackground.enabled = false;
    }
}
