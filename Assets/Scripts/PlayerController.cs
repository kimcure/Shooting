using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootForce = 20f;
    public float fireRate = 0.1f;
    private float lastShotTime = 0f;

    void Update()
    {
        //ĳ���� ������
        float h = Input.GetAxisRaw("Horizontal");//����
        float v = Input.GetAxisRaw("Vertical");//����

        transform.position += new Vector3(h, v, 0).normalized * moveSpeed * Time.deltaTime;//�ӵ�

        //ZŰ�� ������ ��
        if (Input.GetKey(KeyCode.Z))
        {
            if (Time.time - lastShotTime >= fireRate)//�ð����� lastShotTime�� �� ���� fireRate���� ũ�ٸ�
            {
                Shoot();//Shoot �Լ� ����
                lastShotTime = Time.time;
            }
        }
    }

    //�߻�ü �߻��ϴ� �ڵ�
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();//�����ٵ� ���� ����Ʈ
        rb.isKinematic = false;//isKinematic:�ܺο��� �������� ������ ���� �������� �ʴ� ������Ʈ. false�ϱ� �ݴ�� �����ϴ� ������Ʈ...?
        rb.AddForce(firePoint.up * shootForce, ForceMode2D.Impulse);//���� Impulse���� �ְڴٴ� �ǹ�
    }


}
