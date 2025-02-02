using UnityEngine;

[CreateAssetMenu(fileName = "TestQuest")]
public class TestQuest : Quest {
    [SerializeField]
    private bool _isDone;
    [SerializeField]
    private bool _isFailed;

    public override void GiveReward() {
    }

    public override bool IsComplete() {
        if (IsFailed()) {
            return false;
        }

        return _isDone;
    }

    public override bool IsFailed() {
        return _isFailed;
    }
}
