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
        if (collision.collider.CompareTag("Bullet"))
        {
            Debug.Log("총알 충돌 감지");
            spawner.OnEnemyKilled(gameObject);
            Destroy(gameObject, 0.3f);
        }
    }


    void Start()
    {
        mainCam = Camera.main;

        player = GameObject.FindWithTag("Player")?.transform;

        Vector2 min = mainCam.ViewportToWorldPoint(new Vector2(0.1f, 0.1f));
        Vector2 max = mainCam.ViewportToWorldPoint(new Vector2(0.9f, 0.9f));

        targetPosition = new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Kill()
    {
        if (spawner != null)
        {
            spawner.OnEnemyKilled(gameObject);
        }

        Destroy(gameObject);
    }
}
