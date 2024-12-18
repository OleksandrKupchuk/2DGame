using UnityEngine;

public class King : Npc, IInteracvite {
    private Player _player;
    private QuestBringItem _bringItem;

    private void Awake() {
        _popup.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            print("king touch player");
            _player = player;
            _popup.SetActive(true);
            if (_bringItem == null) {
                _bringItem = new QuestBringItem(_player);
            }
            _player.QuestSystem.AddQuest(_bringItem);

            if (!_bringItem.IsRewardTaken) {
                _bringItem.GiveReward();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _popup.SetActive(false);
        }
    }

    public override void Interact() {
        _player.QuestSystem.AddQuest(_bringItem);
    }
}
