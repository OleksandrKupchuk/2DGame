using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    [SerializeField]
    private Text _healthValue;
    [SerializeField]
    private Text _armorValue;
    [SerializeField]
    private Text _damageValue;
    [SerializeField]
    private Text _speedValue;

    public void SetHealth(float value) {
        _healthValue.text = "" + value;
    }

    public void SetArmor(float value) {
        _armorValue.text = "" + value;
    }

    public void SetSpeed(float value) {
        _speedValue.text = "" + value;
    }

    public void SetDamage(float valueMin, float valueMax) {
        _damageValue.text = valueMin + "-" + valueMax;
    }
}
