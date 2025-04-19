using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.2f;//공격범위
    public float attackDelay = 1.0f;//공격딜레이
    public int damage = 1;//대미지

    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;//플레이어 코드를 찾아라
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && Time.time - lastAttackTime > attackDelay)//공격범위와 공격시간에서 마지막으로 공격한 값이 공격딜레이보다 클 때
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);//플레이어가 피해를 입음
            lastAttackTime = Time.time;
        }
    }
}
