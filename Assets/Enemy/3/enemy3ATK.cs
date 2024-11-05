using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3ATK : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            int damageMelee = Random.Range(20, 30);
         PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageMelee);
            }
        }
    }

    
}
