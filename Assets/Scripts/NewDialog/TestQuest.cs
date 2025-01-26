using UnityEngine;

[CreateAssetMenu(fileName = "TestQuest")]
public class TestQuest : Quest {
    public override void GiveReward() {
    }

    public override bool IsDone() {
        return false;
    }
}
