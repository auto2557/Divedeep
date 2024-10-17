using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Movement
{
    protected int attackCount = 0; 
    protected float comboResetTime = 1.5f; 
    protected float lastAttackTime;
    protected bool isAttacking = false;

     public GameObject hitBlockRight; 
    public GameObject hitBlockLeft;  
   
     protected void UpdateHitBlockPosition()
    {
        if (facingRight)
        {
            hitBlockRight.SetActive(true);
            hitBlockLeft.SetActive(false);
        }
        else
        {
            hitBlockRight.SetActive(false);
            hitBlockLeft.SetActive(true);
        }
    }

    protected void HandleAttack()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isAttacking && stateInfo.normalizedTime < 1f)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
          rb.velocity = Vector2.zero;

            if (Time.time - lastAttackTime > comboResetTime)
            {
                attackCount = 0;
            }

            attackCount++; 
            lastAttackTime = Time.time;

            if (attackCount > 4)
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
    }

    protected void PlayJumpAttack()
    {
        isAttacking = true; 
        animator.Play("JumpAttack"); 
        if (facingRight == false)
                {
                    rb.velocity = Vector2.left;
                }
                else if (facingRight == true)
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
                animator.Play("Attack1");
                break;
            case 2:
                animator.Play("Attack2");
                break;
            case 3:
                animator.Play("Attack3");
                if (facingRight == false)
                {
                    rb.velocity = Vector2.left;
                }
                else if (facingRight == true)
                {
                    rb.velocity = Vector2.one;
                }
                break;
            case 4:
                animator.Play("Attack4");
                break;
        }

        StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator ResetAttackState(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        isAttacking = false;
    }

}
