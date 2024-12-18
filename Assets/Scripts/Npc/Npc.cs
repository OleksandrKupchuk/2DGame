using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Npc : MonoBehaviour, IInteracvite {
    [SerializeField]
    protected GameObject _popup;
    public abstract void Interact();
}
