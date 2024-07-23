using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.Die();
        }
    }
}
