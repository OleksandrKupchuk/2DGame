using UnityEngine;

public class QuestBringItem : IQuestTask {
    private Player _player;
    private bool _isRewardTaken = false;
    private GameObject _target;

    public QuestBringItem(Player player) {
        _player = player;
        _target = new GameObject("Quest item king");
        _target.AddComponent<BoxCollider2D>();
        _target.AddComponent<Rigidbody2D>();
        _target.AddComponent<SpriteRenderer>();
    }

    public bool IsRewardTaken => _isRewardTaken;

    public void GiveReward() {
        if (isDone()) {
            _player.Config.conis += 50;
            _isRewardTaken = true;
        }
    }

    public bool isDone() {
        return _player.QuestSystem.IsQuestItem(_target);
    }
}
