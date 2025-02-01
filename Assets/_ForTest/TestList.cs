using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TestList : MonoBehaviour {
    [SerializeField]
    private List<TestA> _nestedList;
    [SerializeField, ReadOnly]
    private bool _isExpired;
}
