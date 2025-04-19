using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 5;//�ִ�ü��
    public int CurrentHealth { get; private set; }//����ü��

    void Start()
    {
        CurrentHealth = MaxHealth;//������ �� ����ü���� �ִ�ü������ ����
    }

    //����� �Դ� �Լ�
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;//amount ��ŭ ü���� ������
        if (CurrentHealth <= 0)//���� ü���� 0���� ������
        {
            Debug.Log("����");//���� ����׷α� �ߵ�
        }
    }

}
