using System.Collections;
using UnityEngine;

public class ItemUsable : Item {
    [field: SerializeField]
    public float Duration { get; protected set; }

    public virtual void Use() {
        EventManager.UseItemEventHandler(this);
        StartCoroutine(SrartTimer());
    }

    protected IEnumerator SrartTimer() {
        yield return new WaitForSeconds(Duration);
        EventManager.ActionItemOverEventHandler(this);
    }

}
