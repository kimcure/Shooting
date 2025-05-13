using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spreadAngle = 15f;//발사되는 앵글 값
    public float speed = 10f;

    private void Start()
    {
        FireAngle(0f);
        FireAngle(spreadAngle);
        FireAngle(-spreadAngle);
        Destroy(gameObject);
    }

    void FireAngle(float angleOffset)//앵글 정해주는 함수
    {
        Quaternion rot = Quaternion.Euler(0, 0, transform.eulerAngles.z + angleOffset);//z축에 angleOffset을 더해줌
        GameObject b = Instantiate(bulletPrefab, transform.position, rot);
        b.GetComponent<Rigidbody2D>().velocity = b.transform.up * speed;//날라가는 스피드
        Destroy(b, 5f);
    }
}
