using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spreadAngle = 15f;
    public float speed = 10f;

    private void Start()
    {
        FireAngle(0f);
        FireAngle(spreadAngle);
        FireAngle(-spreadAngle);
        Destroy(gameObject);
    }

    void FireAngle(float angleOffset)
    {
        Quaternion rot = Quaternion.Euler(0, 0, transform.eulerAngles.z + angleOffset);
        GameObject b = Instantiate(bulletPrefab, transform.position, rot);
        b.GetComponent<Rigidbody2D>().velocity = b.transform.up * speed;
        Destroy(b, 5f);
    }
}
