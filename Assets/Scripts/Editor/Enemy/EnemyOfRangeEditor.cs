using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(EnemyOfRange))]
public class EnemyOfRangeEditor : Editor
{
    public override void OnInspectorGUI() {
        EnemyOfRange _target = (EnemyOfRange)target;

    }
}
