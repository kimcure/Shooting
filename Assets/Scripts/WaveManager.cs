using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;
    public UIManager uiManager;
    public float timeBetweenWave = 10f;

    private int wave = 0;
    private float lastWaveTime;

    public GameObject bossPrefab;
    public int bossWave = 5;

    void Start()
    {
        lastWaveTime = Time.time;
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastWaveTime >= timeBetweenWave)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        wave++;
        //spawner.maxEnemies += 2;
        lastWaveTime = Time.time;

        if (wave % bossWave == 0)
        {
            SpawnBoss();
        }
        else
        {
            for (int i = 0; i < spawner.maxEnemies; i++)
            {
                spawner.SpawnEnemy();
            }
        }

        uiManager.ShowWave(wave);
    }

    void SpawnBoss()
    {
        Vector3 spawnPos = spawner.GetRandomOutsidePosition();
        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        uiManager.ShowBossWarning();
    }
}
