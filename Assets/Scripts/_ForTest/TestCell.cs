using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCell : MonoBehaviour
{
    private Cell _cell;
    void Start()
    {
        _cell = GetComponent<Cell>();
        Debug.Log(_cell.name);
    }

    void Update()
    {
        
    }
}
