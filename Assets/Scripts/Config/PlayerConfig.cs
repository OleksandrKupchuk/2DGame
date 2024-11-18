using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Config/PlayerConfig", order = 3)]
public class PlayerConfig : Config {
    public float healthRegeneration;
    public float delayHealthRegeneration;
    public int conis;
}