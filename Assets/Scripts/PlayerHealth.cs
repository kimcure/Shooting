using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 5;//최대체력
    public int CurrentHealth { get; private set; }//현재체력

    void Start()
    {
        CurrentHealth = MaxHealth;//시작할 때 현재체력을 최대체력으로 설정
    }

    //대미지 입는 함수
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;//amount 만큼 체력이 감소함
        if (CurrentHealth <= 0)//만약 체력이 0보다 작으면
        {
            Debug.Log("죽음");//죽음 디버그로그 발동
        }
    }

}
