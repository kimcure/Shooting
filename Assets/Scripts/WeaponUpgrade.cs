using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.UpgradeFireRate(duration);
            Destroy(gameObject);
        }
    }
}
