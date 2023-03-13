using System;
using UnityEngine;

[Serializable]
public class Config : MonoBehaviour {
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