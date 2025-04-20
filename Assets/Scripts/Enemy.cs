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
        //�Ѿ˿� �¾��� �� Kill �Լ� ����
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
        mainCam = Camera.main;//ī�޶� ����

        player = GameObject.FindWithTag("Player")?.transform;//Player �±׸� ���� ������Ʈ�� ã��

        //ī�޶� �ٱ��� ��ġ����
        Vector2 min = mainCam.ViewportToWorldPoint(new Vector2(0.1f, 0.1f));
        Vector2 max = mainCam.ViewportToWorldPoint(new Vector2(0.9f, 0.9f));

        //������ġ
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

        //�� ������
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        //���� �ٶ󺸴� �ޱ�(z������ �������� �ʵ��� �ϴ� ����)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Kill()
    {
        if (spawner != null)
        {

            spawner.OnEnemyKilled(gameObject);//�������� OnEnemyKilled �Լ� ȣ��
        }

        Destroy(gameObject);//����
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
