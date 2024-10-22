using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Movement
{
    protected int attackCount = 0; 
    public int damage;
    protected float comboResetTime = 1.5f; 
    protected float lastAttackTime;
    protected bool isAttacking = false;
    protected bool inputBuffered = false;

    public GameObject hitBlockRight; 
    public GameObject hitBlockLeft;

    protected void UpdateHitBlockPosition()
    {
        if (facingRight && Input.GetKeyDown(KeyCode.J))
        {
            hitBlockRight.SetActive(true);
            hitBlockLeft.SetActive(false);
        }
        else if (!facingRight && Input.GetKeyDown(KeyCode.J))
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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Input buffering: Allow input buffering for smooth combo chaining
        if (Input.GetKeyDown(KeyCode.J))
        {
            inputBuffered = true;
        }

        // If already attacking and animation is not finished, wait
        if (isAttacking && stateInfo.normalizedTime < 1f)
        {
            return;
        }

        // Handle the attack only if input is buffered and not currently attacking
        if (inputBuffered && !isAttacking)
        {
            inputBuffered = false;
            StartAttackSequence();
        }
    }

    private void StartAttackSequence()
    {
        int randomDmg = Random.Range(9, 20);
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
                ResetHitBlockSize(); 
                break;
            case 2:
                animator.Play("Attack2");
             AdjustHitBlockSize(new Vector2(1.2f,1.2f));
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
                AdjustHitBlockSize(new Vector2(1.4f,1.4f));
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
}
