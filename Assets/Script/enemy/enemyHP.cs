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


    public IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(2f); 
        Destroy(gameObject); 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hitblock") && !isDead)
        {
            hp -= Playerscript.damage;
            isHit = true;

            if (Playerscript.facingRight == true)
            {
                rb.velocity = new Vector2(3f, 1f);
            }
            else if (Playerscript.facingRight == false)
            {
                rb.velocity = new Vector2(-3f, 1f);

            }

            StartCoroutine(ResetHit());

        }
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); 
        isHit = false; 
    }
}
