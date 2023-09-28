using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RegisterObjective();
    }
    
}
