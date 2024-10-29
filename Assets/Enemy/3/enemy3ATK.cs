using System.Collections;
using UnityEngine;

public class enemy3ATK : MonoBehaviour
{
    private bool isDamaging = false; 
    public enemy3 enem3; 
    private Coroutine damageCoroutine;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null && !isDamaging)
            {
                isDamaging = true;
                damageCoroutine = StartCoroutine(ApplyDamageOverTime(playerHealth));
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDamaging)
        {
            isDamaging = false;
            StopCoroutine(damageCoroutine);
        }
    }

    IEnumerator ApplyDamageOverTime(PlayerHealth playerHealth)
    {
        while (isDamaging)
        {
            if (enem3 != null && enem3.animator != null)
            {
                enem3.animator.SetTrigger("Attack");
            }
            else
            {
                Debug.LogWarning("Animator or enemy3 is not assigned.");
            }

            int damageMelee = Random.Range(20, 30);
            playerHealth.TakeDamage(damageMelee);

            yield return new WaitForSeconds(2f);
        }
    }
}
