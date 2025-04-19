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

        //SpawnEnemy 코드 반복호출
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomOutsidePosition();//스폰위치지정
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().spawner = this;//스포너 지정
        activeEnemies.Add(enemy);//적 캐릭터 생성
    }

    Vector2 GetRandomOutsidePosition()
    {
        float x, y;
        int edge = Random.Range(0, 4);//0에서부터 3까지 랜덤으로 배정

        Vector2 screenMin = mainCam.ViewportToWorldPoint(new Vector2(0, 0));//생성 위치(x)
        Vector2 screenMax = mainCam.ViewportToWorldPoint(new Vector2(1, 1));//생성 위치(y)

        switch (edge)
        {
            //랜덤으로 받은 숫자를 통해 어디서 생성될 지 결정
            case 0:
                x = screenMin.x - 1f;
                y = Random.Range(screenMin.y, screenMax.y);//좌
                break;
            case 1:
                x = screenMax.x + 1f;
                y = Random.Range(screenMin.y, screenMax.y);//우
                break;
            case 2:
                y = screenMax.y + 1f;
                x = Random.Range(screenMin.x, screenMax.x);//위
                break;
            case 3:
                y = screenMin.y - 1f;
                x = Random.Range(screenMin.x, screenMax.x);//아래
                break;
            default:
                x = 0; y= 0; break;
        }

        return new Vector2(x, y);
    }

    //적이 죽었을 때
    public void OnEnemyKilled(GameObject enemy)
    {
        activeEnemies.Remove(enemy);//리스트에서 적 제거
        StartCoroutine(RespawnAfterDelay(5f));//리스폰 이후 딜레이
    }

    //딜레이 만드는 코드
    IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);//??
        if (activeEnemies.Count < maxEnemies)//살아있는 적이 맥스치보다 적을 때
        {
            SpawnEnemy();//SpawnEnemy 함수 실행
        }
    }

    //살아있는 적 카운트하는 코드
    public int ActiveEnemyCount()
    {
        return activeEnemies.Count;
    }
}
