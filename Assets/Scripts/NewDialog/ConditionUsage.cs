using UnityEngine;

[System.Serializable]
public class ConditionUsage {
    [SerializeField]
    private bool _expectedResult;
    [SerializeField]
    private Condition _condition;
}
