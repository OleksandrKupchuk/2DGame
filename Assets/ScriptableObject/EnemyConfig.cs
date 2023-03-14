using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Config/EnemyConfig", order = 2)]
public class EnemyConfig : Config {
    public float timerMinIdle;
    public float timerMaxIdle;
    public float timerMinRun;
    public float timerMaxRun;
    public float delayAttack;
    public float delayStrikeAttack;
    public float distanceStrikeAttack;
    public float distanceMeleeAttack;
    public float distanceRangeAttack;
}
