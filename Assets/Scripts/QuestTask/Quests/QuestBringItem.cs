using UnityEngine;

public class QuestBringItem : IQuest {
    private PlayerConfig _playerConfig;
    private bool _isRewardTaken = false;
    private GameObject _target;

    public QuestBringItem(PlayerConfig playerConfig) {
        _playerConfig = playerConfig;
        _target = new GameObject("DialogAction item king");
        _target.AddComponent<BoxCollider2D>();
        _target.AddComponent<Rigidbody2D>();
        _target.AddComponent<SpriteRenderer>();
    }

    public bool IsRewardTaken => _isRewardTaken;

    public void GiveReward() {
        if (IsDone()) {
            _playerConfig.coins += 50;
            _isRewardTaken = true;
        }
    }

    public bool IsDone() {
        //return QuestSystem.Instance.IsQuestItem(_target);
        return false;
    }
}
