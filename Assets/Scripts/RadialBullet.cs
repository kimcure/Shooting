using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBullet : MonoBehaviour
{
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
