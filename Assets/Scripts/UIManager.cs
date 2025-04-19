using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerHealthText;
    public Text enemyCountText;
    public PlayerHealth playerHealth; 
    public EnemySpawner spawner;

    private void Start()
    {
        playerHealthText.fontSize = 32;
        playerHealthText.color = Color.white;

        enemyCountText.fontSize = 32;
        enemyCountText.color = Color.white;
    }

    void Update()
    {
        playerHealthText.text = $"HP : {playerHealth.CurrentHealth}/{playerHealth.MaxHealth}";
        enemyCountText.text = $"Enemies: {spawner.ActiveEnemyCount()}";


    }
}
