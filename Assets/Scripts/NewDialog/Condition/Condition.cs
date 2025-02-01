using UnityEngine;

[System.Serializable]
public abstract class Condition : ScriptableObject {
    [SerializeField]
    protected bool _expectedResult;
    public bool IsTrue => _expectedResult == GetActualResult();

    protected abstract bool GetActualResult();
}
