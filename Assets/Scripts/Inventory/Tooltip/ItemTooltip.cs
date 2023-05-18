using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour {
    private RectTransform _backgroundRectTransform;
    private List<AttributeTooltip> _attributeTooltips = new List<AttributeTooltip>();
    private VerticalLayoutGroup _verticalLayoutGroup;

    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private AttributeTooltip _attributePrefab;

    public float GetHeightImage { get => _backgroundRectTransform.rect.height; }

    private void Awake() {
        _backgroundRectTransform = _background.GetComponent<RectTransform>();
        _verticalLayoutGroup = _background.GetComponent<VerticalLayoutGroup>();
        if (_background == null) {
            Debug.LogError("Object 'background' is null");
        }

        CreateAttributes();
    }

    private void CreateAttributes() {
        for (int i = 0; i < 5; i++) {
            AttributeTooltip _attributeTooltip = Instantiate(_attributePrefab);
            _attributeTooltip.transform.SetParent(_background.transform);
            _attributeTooltip.transform.localScale = new Vector3(1, 1, 1);
            _attributeTooltip.gameObject.SetActive(false);
            _attributeTooltips.Add(_attributeTooltip);
        }
    }

    public void ShowAttributes(Item item, Vector2 pos, float heightCell) {
        SetSizeBackground(item.Attributes.Count);
        SetPosition(pos, heightCell);

        //foreach (Attribute attribute in item.Attributes) {
        //    SetDataAndEnableAttribute(attribute);
        //}

        for (int i = 0; i < item.Attributes.Count; i++) {
            _attributeTooltips[i].SetValue(item.Attributes[i]);
            _attributeTooltips[i].gameObject.SetActive(true);
        }
    }

    private void SetDataAndEnableAttribute(Attribute attribute) {
        for (int i = 0; i < _attributeTooltips.Count; i++) {
            if (!_attributeTooltips[i].gameObject.activeSelf) {
                _attributeTooltips[i].SetValue(attribute);
                _attributeTooltips[i].gameObject.SetActive(true);
                return;
            }
        }
    }

    private void SetSizeBackground(int amountAttributes) {
        _backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.rect.width, (amountAttributes * _attributePrefab.RectTransform.rect.height) +
            _verticalLayoutGroup.padding.top + _verticalLayoutGroup.padding.bottom);
    }

    private void SetPosition(Vector2 pos, float heightCell) {
        _background.transform.position = pos;
        float _spaceBetweenTooltipAndCellInPixel = 10;
        float _heightInPixel = (_backgroundRectTransform.rect.height + heightCell) / 2 + _spaceBetweenTooltipAndCellInPixel;
        _backgroundRectTransform.anchoredPosition = new Vector2(_backgroundRectTransform.anchoredPosition.x, _backgroundRectTransform.anchoredPosition.y + _heightInPixel);
    }

    public void DisableAttributes() {
        foreach (AttributeTooltip attributeTooltip in _attributeTooltips) {
            attributeTooltip.gameObject.SetActive(false);
        }

        _backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.rect.width, 0f);
    }
}