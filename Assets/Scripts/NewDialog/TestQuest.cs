using UnityEngine;

[CreateAssetMenu(fileName = "TestQuest")]
public class TestQuest : Quest {
    [SerializeField]
    private bool _isDone;

    public override void GiveReward() {
    }

    public override bool IsComplete() {
        return _isDone;
    }
}
