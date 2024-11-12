using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class Attack : Movement
{
    protected int attackCount = 0;
    public int damage;
    protected float comboResetTime = 1.5f;
    protected float lastAttackTime;
    protected bool isAttacking = false;
    protected bool inputBuffered = false;
    protected bool canSlash = true;
    protected bool isRangedMode = false;

    public GameObject hitBlockRight;
    public GameObject hitBlockLeft;

    public GameObject Sonicblow;
    public GameObject rangedProjectile;
    public TextMeshProUGUI cooldownslash;

 

    protected void HandleRangedAttack()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                animator.SetBool("attackRange",true);
                StartCoroutine(FireRangedAttack());
            }
            
        }
        else
        {
            animator.SetBool("attackRange", false);
        }
    }

    protected IEnumerator FireRangedAttack()
    {
        rb.velocity = Vector2.zero; 
        int randomDmg = Random.Range(3, 8);
        damage = randomDmg;
        Debug.Log("Ranged dmg = " + damage);

        Vector2 fireDirection = facingRight ? Vector2.right : Vector2.left;
        GameObject projectile = Instantiate(rangedProjectile, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = fireDirection * 5f;

        if (!facingRight)
        {
            projectile.transform.localScale = new Vector2(-1, 1);
        }

        yield return new WaitForSeconds(0.3f); 
        isAttacking = false;
    }


    protected void UpdateHitBlockPosition()
    {
        if (facingRight && Input.GetKeyDown(KeyCode.X))
        {
            hitBlockRight.SetActive(true);
            hitBlockLeft.SetActive(false);
        }
        else if (!facingRight && Input.GetKeyDown(KeyCode.X))
        {
            hitBlockRight.SetActive(false);
            hitBlockLeft.SetActive(true);
        }
        else
        {
            hitBlockRight.SetActive(false);
            hitBlockLeft.SetActive(false);
        }
    }

    protected void HandleAttack()
    {
        if (isRangedMode)
        {
            return; 
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.X))
        {
            inputBuffered = true;
        }

        if (isAttacking && stateInfo.normalizedTime < 1f)
        {
            return;
        }

        if (inputBuffered && !isAttacking)
        {
            inputBuffered = false;
            StartAttackSequence();
        }

        if (Input.GetKeyDown(KeyCode.S) && canSlash)
        {
            SoundManager.instance.PlaySFX("player", 3, 0);
            animator.SetBool("isSlash", true);
            IAISLASH();
            canSlash = false;
            StartCoroutine(cooldownSlash());
            StartCoroutine(waitForSlash());
        }
    }


    protected void StartAttackSequence()
    {
        int randomDmg = Random.Range(10, 20);
        damage = randomDmg;
        Debug.Log("dmg = " + damage);

        rb.velocity = Vector2.zero;

        if (Time.time - lastAttackTime > comboResetTime)
        {
            attackCount = 0;
        }

        attackCount++;
        lastAttackTime = Time.time;

        if (attackCount > 3)
        {
            attackCount = 1;
        }

        if (!isGrounded)
        {
            PlayJumpAttack();
        }
        else
        {
            PlayAttackAnimation(attackCount);
        }
    }

    protected void PlayJumpAttack()
    {
        isAttacking = true;
         SoundManager.instance.PlaySFX("player", 3,0);
        animator.Play("JumpAttack");

        if (!facingRight)
        {
            rb.velocity = Vector2.left;
        }
        else
        {
            rb.velocity = Vector2.one;
        }

        StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    protected void PlayAttackAnimation(int attackStep)
    {
        isAttacking = true;

        switch (attackStep)
        {
            case 1:
                animator.Play("Attack4");
                SoundManager.instance.PlaySFX("player", 4,0);
                AdjustHitBlockSize(new Vector2(0.7f, 0.7f));
                break;
            case 2:
                animator.Play("Attack2");
                SoundManager.instance.PlaySFX("player", 5,0);
                AdjustHitBlockSize(new Vector2(0.9f, 0.9f));
                break;
            case 3:
                animator.Play("Attack3");
                if (!facingRight)
                {
                    rb.velocity = Vector2.left;
                }
                else
                {
                    rb.velocity = Vector2.one;
                }
                SoundManager.instance.PlaySFX("player", 6,0);
                AdjustHitBlockSize(new Vector2(1.2f, 1.2f));
                break;
        }

        StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    protected void AdjustHitBlockSize(Vector2 newScale)
    {
        if (facingRight)
        {
            hitBlockRight.transform.localScale = newScale;
        }
        else
        {
            hitBlockLeft.transform.localScale = newScale;
        }
    }

    protected void ResetHitBlockSize()
    {
        Vector2 defaultScale = new Vector2(0.3f, 0.3f);
        hitBlockRight.transform.localScale = defaultScale;
        hitBlockLeft.transform.localScale = defaultScale;
    }

    IEnumerator ResetAttackState(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        isAttacking = false;

        if (inputBuffered)
        {
            inputBuffered = false;
            StartAttackSequence();
        }
    }

    protected void IAISLASH()
    {
         int randomDmg = Random.Range(25, 35);
        damage = randomDmg;
        Debug.Log("dmg = " + damage);
        
        Vector2 slahDirection = facingRight ? Vector2.right : Vector2.left;

        GameObject slash = Instantiate(Sonicblow, transform.position, Quaternion.identity);

        slash.GetComponent<Rigidbody2D>().velocity = slahDirection * 6f;

        if (!facingRight)
        {
            slash.transform.localScale = new Vector2(-1, 1);
        }
    }
     protected IEnumerator cooldownSlash()
    {
        float cooldownDuration = 1.5f;
        float remainingTime = cooldownDuration;

        while (remainingTime > 0)
        {
            cooldownslash.text = $"{remainingTime:F1}";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        cooldownslash.text = "Ready";
        canSlash = true;
    }

    

    protected IEnumerator waitForSlash()
    {
        yield return new WaitForSeconds(0.5f);
         animator.SetBool("isSlash", false);
    }
}
