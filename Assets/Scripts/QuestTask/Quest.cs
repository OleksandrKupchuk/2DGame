using UnityEngine;

public abstract class Quest : ScriptableObject {
    public abstract bool IsComplete();
    public abstract bool IsFailed();
    public abstract void GiveReward();
}
