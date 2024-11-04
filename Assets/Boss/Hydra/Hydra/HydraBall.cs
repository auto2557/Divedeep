using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraBall : MonoBehaviour
{
      void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            int damageMelee = Random.Range(1, 2);
         PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageMelee);
            }
        }
    }
}
