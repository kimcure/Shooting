using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 5;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomOutsidePosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().spawner = this;
        activeEnemies.Add(enemy);
    }

    Vector2 GetRandomOutsidePosition()
    {
        float x, y;
        int edge = Random.Range(0, 4);

        Vector2 screenMin = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = mainCam.ViewportToWorldPoint(new Vector2(1, 1));

        switch (edge)
        {
            case 0:
                x = screenMin.x - 1f;
                y = Random.Range(screenMin.y, screenMax.y);
                break;
            case 1:
                x = screenMax.x + 1f;
                y = Random.Range(screenMin.y, screenMax.y);
                break;
            case 2:
                y = screenMax.y + 1f;
                x = Random.Range(screenMin.x, screenMax.x);
                break;
            case 3:
                y = screenMin.y - 1f;
                x = Random.Range(screenMin.x, screenMax.x);
                break;
            default:
                x = 0; y= 0; break;
        }

        return new Vector2(x, y);
    }

    public void OnEnemyKilled(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        StartCoroutine(RespawnAfterDelay(5f));
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (activeEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }
}
