using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Npc : MonoBehaviour, IInteracvite {
    public abstract void Interact();
}
