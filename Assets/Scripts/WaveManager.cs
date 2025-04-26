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
        wave++;//웨이브 숫자 증가
        //spawner.maxEnemies += 2;
        lastWaveTime = Time.time;

        if (wave % bossWave == 0)//웨이브가 보스웨이브의 배수일 시
        {
            SpawnBoss();//보스를 스폰
        }
        else//아닐시
        {
            for (int i = 0; i < spawner.maxEnemies; i++)
            {
                spawner.SpawnEnemy();//맥스Enemies만큼 for문을 돌림
            }
        }

        uiManager.ShowWave(wave);//UImanager에서 웨이브를 보여줌
    }

    //보스 스폰하는 함수
    void SpawnBoss()
    {
        Vector3 spawnPos = spawner.GetRandomOutsidePosition();
        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        uiManager.ShowBossWarning();
    }
}
