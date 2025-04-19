using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.2f;//���ݹ���
    public float attackDelay = 1.0f;//���ݵ�����
    public int damage = 1;//�����

    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;//�÷��̾� �ڵ带 ã�ƶ�
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && Time.time - lastAttackTime > attackDelay)//���ݹ����� ���ݽð����� ���������� ������ ���� ���ݵ����̺��� Ŭ ��
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);//�÷��̾ ���ظ� ����
            lastAttackTime = Time.time;
        }
    }
}
