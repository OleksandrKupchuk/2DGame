using UnityEngine;

public abstract class Quest : ScriptableObject {
    public abstract bool IsDone();
    public abstract void GiveReward();
}
