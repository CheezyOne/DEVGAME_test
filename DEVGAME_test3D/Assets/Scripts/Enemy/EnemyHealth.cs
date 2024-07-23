using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public static Action<int> onEnemyDeath;
    [SerializeField] private int _killPoints;
    public override void Die()
    {
        onEnemyDeath?.Invoke(_killPoints);
        base.Die();
    }
}
