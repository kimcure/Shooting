using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.2f;
    public float attackDelay = 1.0f;
    public int damage = 1;

    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && Time.time - lastAttackTime > attackDelay)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
