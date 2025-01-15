using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; protected set; }
    [field: SerializeField]
    public string Description { get; protected set; }
    [field: SerializeField]
    public int Price { get; protected set; }
    [field: SerializeField]
    public Sprite Icon { get; protected set; }
    [field: SerializeField]
    public List<AttributeData> Attributes { get; protected set; } = new List<AttributeData>();
}
