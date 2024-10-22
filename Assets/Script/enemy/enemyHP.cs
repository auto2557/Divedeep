using System.Collections;
using UnityEngine;

public class enemyHP : MonoBehaviour
{
    public int hp;
    public Rigidbody2D rb;
    public player Playerscript;
    public Animator anim;
    public bool isDead = false;
    protected bool isHit = false;

    public GameObject damagePopupPrefab;  

    public IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(2f); 
        Destroy(gameObject); 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hitblock") && !isDead && !isHit)
        {
            isHit = true;  // Set isHit to true immediately to prevent repeated damage

            int damageAmount = Playerscript.damage;  
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            // Apply knockback based on player's facing direction
            if (Playerscript.facingRight)
            {
                rb.velocity = new Vector2(3.5f, 3f);
            }
            else
            {
                rb.velocity = new Vector2(-3.5f, 3f);
            }

            // Check if the enemy is dead
            if (hp <= 0)
            {
                isDead = true;
                StartCoroutine(Die());
            }

            StartCoroutine(ResetHit()); 
        }
    }

    private void ShowDamagePopup(int damageAmount)
    {
        GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
        damagePopup.GetComponent<DamagePopup>().Setup(damageAmount);
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.3f); 
        isHit = false;  
    }
}
