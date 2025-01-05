using UnityEngine;

public class QuestBringItem : IQuest {
    private Player _player;
    private bool _isRewardTaken = false;
    private GameObject _target;

    public void Init(Player player) {
        _player = player;
        _target = new GameObject("Quest item king");
        _target.AddComponent<BoxCollider2D>();
        _target.AddComponent<Rigidbody2D>();
        _target.AddComponent<SpriteRenderer>();
    }

    public bool IsRewardTaken => _isRewardTaken;

    public void GiveReward() {
        if (IsDone()) {
            _player.Config.coins += 50;
            _isRewardTaken = true;
        }
    }

    public bool IsDone() {
        return QuestSystem.Instance.IsQuestItem(_target);
    }
}
