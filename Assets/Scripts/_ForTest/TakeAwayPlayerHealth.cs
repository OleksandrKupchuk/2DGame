using UnityEngine;
using UnityEngine.UI;

public class TakeAwayPlayerHealth : MonoBehaviour {
    [SerializeField]
    private float _health;
    [SerializeField]
    private Button _addHealthButton;
    [SerializeField]
    private PlayerHealthController _healthController;

    private void Start() {
        _addHealthButton.onClick.AddListener(() => {
            _healthController.TakeDamage(_health);
        });
    }
}
