using System.Collections;
using UnityEngine;

public class enemyHP : MonoBehaviour
{
    public int hp;
    public Rigidbody2D rb;
    public player Playerscript;
    public Animator anim;
    public bool isDead = false;
   

    public GameObject damagePopupPrefab;  

    public virtual IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(2f); 
        Destroy(gameObject); 
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead)
        {

            SoundManager.instance.PlaySFX("player", 7, 0);
            int damageAmount = Playerscript.damage;  
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            if (Playerscript.facingRight)
            {
                rb.velocity = new Vector2(1f, 1f);
            }
            else
            {
                rb.velocity = new Vector2(-1f, 1f);
            }

            if (hp <= 0)
            {
                isDead = true;
                StartCoroutine(Die());
            }

           
        }
    }

    protected void ShowDamagePopup(int damageAmount)
    {
        GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
        damagePopup.GetComponent<DamagePopup>().Setup(damageAmount);
    }

}
