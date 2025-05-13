using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private UIManager uiManager;

    public GameObject laserPrefab;
    public Transform firePoint;
    public float attackInterval;

    private enum BossState { Idle, Charging, Cooldown }
    private BossState currentState = BossState.Idle;

    public float chargeSpeed = 8f;
    public float chargeCooldown = 2f;
    public float chargeInterval = 5f;

    private Transform player;
    private bool isCharging = false;

    public GameObject radialBulletPrefab;
    public int bulletCount = 12;
    public float radialInterval = 4f;

    void Start()
    {
        currentHealth = maxHealth;//시작체력을 맥스체력으로 설정
        uiManager = FindObjectOfType<UIManager>();//UIManager 호출
        uiManager.ShowBossHealthBar();//보스 체력바 보이게 하기
        uiManager.updateBossHealth(1f);

        player = GameObject.FindWithTag("Player")?.transform;

        StartCoroutine(AttackLoop()); // 레이저 공격
        StartCoroutine(ChargeAttackLoop()); // 돌진 공격
        StartCoroutine(RadialShotLoop()); // 원형 발사 공격
    }

    IEnumerator RadialShotLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(radialInterval);
            FireRadialBullets();
        }
    }

    IEnumerator ChargeAttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(chargeInterval);
            if (player != null && currentState == BossState.Idle)
            {
                StartCoroutine(ChargeAtPlayer());
            }
        }
    }

    IEnumerator ChargeAtPlayer()
    {
        currentState = BossState.Charging;

        Vector3 targetDirection = (player.position - transform.position).normalized;

        float chargeTime = 0.5f;
        float elapsed = 0f;

        while (elapsed < chargeTime)
        {
            transform.position = targetDirection * chargeSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        currentState = BossState.Cooldown;
        yield return new WaitForSeconds(chargeCooldown);

        currentState = BossState.Idle;
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            FireLaser();
        }
    }

    void FireRadialBullets()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(radialBulletPrefab, transform.position, rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 5f;

            Destroy(bullet, 5f);
        }
    }

    void FireLaser()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Destroy(laser, 5f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float normalizedHealth = Mathf.Clamp01((float)currentHealth / maxHealth);//체력 비율
        uiManager.updateBossHealth(normalizedHealth);//UIManager로 체력 업데이트

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        uiManager.HideBossHealthBar();
        uiManager.ShowStageClear();
        FindObjectOfType<GameManager>()?.StageClear();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            TakeDamage(5);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player") && currentState == BossState.Charging)
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(15);
            }
        }
    }
}
