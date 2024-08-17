using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChangeSkin : MonoBehaviour
{
    private int _skinCounter = 0;
    private SpriteLibraryAsset _spriteLibraryAsset;
    private List<string> _categories = new();
    private List<string> _labels = new();
    [SerializeField]
    private List<SpriteResolver> _spriteResolvers = new List<SpriteResolver>();

    //private void Awake() {
    //    _spriteLibraryAsset = _spriteResolvers[0].spriteLibrary.spriteLibraryAsset;
    //    _categories = new List<string>(_spriteLibraryAsset.GetCategoryNames());
    //    _labels = new List<string>(_spriteLibraryAsset.GetCategoryLabelNames(_categories[0]));
    //}

    [ContextMenu("Change Skin")]
    public void NextSkin() {
        _spriteLibraryAsset = _spriteResolvers[0].spriteLibrary.spriteLibraryAsset;
        _categories = new List<string>(_spriteLibraryAsset.GetCategoryNames());
        _labels = new List<string>(_spriteLibraryAsset.GetCategoryLabelNames(_categories[0]));

        foreach (string _category in _categories) {
            print("category " + _category);


            foreach (string _label in _labels) {
                print("label " + _label);
            }
        }

        _skinCounter++;

        if (_skinCounter >= _labels.Count) {
            _skinCounter = 0;
        }

        foreach (SpriteResolver _spriteResolver in _spriteResolvers) {
            _spriteResolver.SetCategoryAndLabel(_spriteResolver.GetCategory(), _labels[_skinCounter]);
        }
    }
}
