using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySpawner spawner;
    public float speed = 2f;

    private Vector2 targetPosition;
    private Camera mainCam;

    private Transform player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //총알에 맞았을 시 Kill 함수 실행
        if (collision.collider.CompareTag("Bullet"))
        {
            EnemyHealth health = GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
            //Kill();
        }
    }


    void Start()
    {
        mainCam = Camera.main;//카메라 지정

        player = GameObject.FindWithTag("Player")?.transform;//Player 태그를 가진 오브젝트를 찾음

        //카메라 바깥쪽 위치지정
        Vector2 min = mainCam.ViewportToWorldPoint(new Vector2(0.1f, 0.1f));
        Vector2 max = mainCam.ViewportToWorldPoint(new Vector2(0.9f, 0.9f));

        //랜덤위치
        targetPosition = new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );

        StartCoroutine(AppearAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        //적 움직임
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        //적이 바라보는 앵글(z축으로 뒤집히지 않도록 하는 역할)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Kill()
    {
        if (spawner != null)
        {

            spawner.OnEnemyKilled(gameObject);//스포너의 OnEnemyKilled 함수 호출
        }

        Destroy(gameObject);//삭제
    }

    IEnumerator AppearAnimation()
    {
        transform.localScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;
        
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }
    }
}
