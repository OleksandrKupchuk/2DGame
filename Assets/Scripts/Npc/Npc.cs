using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Npc : MonoBehaviour, IInteracvite {
    protected List<Dialog> _dialogs = new();

    [SerializeField]
    protected GameObject _interactionIcon;
    public abstract void Interact();
}
