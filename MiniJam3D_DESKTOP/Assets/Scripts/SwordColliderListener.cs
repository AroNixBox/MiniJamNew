using System;
using UnityEngine;

public class SwordColliderListener : MonoBehaviour
{ 
    [SerializeField] private Combat _combat;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Enemy"))
            _combat.HandleSwordTriggerEnter(other);
    }
}