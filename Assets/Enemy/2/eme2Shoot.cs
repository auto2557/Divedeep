using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eme2Shoot : MonoBehaviour
{
   public int damage;

   void Start()
   {
    damage = 10;
    Destroy(gameObject,6f);
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
