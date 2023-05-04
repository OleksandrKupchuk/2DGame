using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Config/Config", order = 1)]
public class Config : ScriptableObject {
    public float health;
    public float speed;
    public float jumpVelocity;
    public float armor;
    public float damageMin;
    public float damageMax;
}