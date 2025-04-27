using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;
        uiManager = FindObjectOfType<UIManager>();
        uiManager.ShowBossHealthBar();
        uiManager.updateBossHealth(1f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float normalizedHealth = Mathf.Clamp01((float)currentHealth / maxHealth);
        uiManager.updateBossHealth(normalizedHealth);

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        uiManager.HideBossHealthBar();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            TakeDamage(5);
            Destroy(other.gameObject);
        }
    }
}
