using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Enemy enemy;

    void Start()
    {
        currentHealth = maxHealth;

        if (enemy == null) {
            enemy = GetComponent<Enemy>();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            enemy.Kill();
        }
    }
}
