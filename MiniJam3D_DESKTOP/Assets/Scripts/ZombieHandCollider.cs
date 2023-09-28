using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHandCollider : MonoBehaviour
{
    [SerializeField] private EnemyController catComponent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
            catComponent.RequestDoDamageToPlayer(other.GetComponent<IDamageable>());
    }
}
