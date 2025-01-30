using UnityEngine;

[System.Serializable]
public abstract class Condition : ScriptableObject {
    [SerializeField]
    private bool _expectedResult;

    public abstract bool IsTrue();
}
