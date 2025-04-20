using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 5;//�ִ�ü��
    public int CurrentHealth { get; private set; }//����ü��

    public GameManager gameManager;
    private bool isInvincible = false;
    public float invincibleTime = 1.0f;

    void Start()
    {
        CurrentHealth = MaxHealth;//������ �� ����ü���� �ִ�ü������ ����
    }

    //����� �Դ� �Լ�
    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        CurrentHealth -= amount;//amount ��ŭ ü���� ������
        if (CurrentHealth <= 0)//���� ü���� 0���� ������
        {
            gameManager.GameOver();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}
