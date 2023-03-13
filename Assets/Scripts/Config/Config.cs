using System;
using UnityEngine;

[Serializable]
public class Config : MonoBehaviour {

    public void SetParameters(Config config) {
        timerMinIdle = config.timerMinIdle;
        timerMaxIdle = config.timerMaxIdle;
        timerMinRun = config.timerMinRun;
        timerMaxRun = config.timerMaxRun;
        delayAttack = config.delayAttack;
        delayStrikeAttack = config.delayStrikeAttack;
    }

    public float timerMinIdle;
    public float timerMaxIdle;
    public float timerMinRun;
    public float timerMaxRun;
    public float delayAttack;
    public float delayStrikeAttack;
}