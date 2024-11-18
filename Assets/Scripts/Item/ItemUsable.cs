using UnityEngine;

public class ItemUsable : Item {
    [field: SerializeField]
    public float Duration { get; protected set; }

    public virtual void Use() {
        EventManager.UseItemEventHandler(this);
        Invoke(nameof(SrartTimerDelay), Duration);
    }

    private void SrartTimerDelay() {
        EventManager.ActionItemOverEventHandler(this);
    }
}
