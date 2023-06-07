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

    public void ShowAttributes(Item item, Vector2 positionCell, float heightCell) {
        Equipment _equipment = item as Equipment;
        if (_equipment == null) {
            print("Equipment is null");
            return;
        }
        DisableAttributes();
        SetSizeBackground(_equipment.Attributes.Count);
        SetPosition(positionCell, heightCell);

        for (int i = 0; i < _equipment.Attributes.Count; i++) {
            Sprite _icon = LoadAttributesIcon.GetIcon(_equipment.Attributes[i].type);
            _attributeTooltips[i].SetValue(_equipment.Attributes[i]);
            _attributeTooltips[i].SetIcon(_icon);
            _attributeTooltips[i].gameObject.SetActive(true);
        }
    }

    public void DisableAttributes() {
        foreach (AttributeTooltip attributeTooltip in _attributeTooltips) {
            attributeTooltip.gameObject.SetActive(false);
        }

        _backgroundRectTransform.sizeDelta = new Vector2(_backgroundRectTransform.rect.width, 0f);
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
}