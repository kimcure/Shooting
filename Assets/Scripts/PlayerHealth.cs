using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 5;//최대체력
    public int CurrentHealth { get; private set; }//현재체력

    public GameManager gameManager;
    private bool isInvincible = false;
    public float invincibleTime = 1.0f;

    void Start()
    {
        CurrentHealth = MaxHealth;//시작할 때 현재체력을 최대체력으로 설정
    }

    //대미지 입는 함수
    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        CurrentHealth -= amount;//amount 만큼 체력이 감소함
        if (CurrentHealth <= 0)//만약 체력이 0보다 작으면
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
