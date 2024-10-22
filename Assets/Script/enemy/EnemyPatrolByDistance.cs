using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolByDistance : enemyHP
{
    public float patrolDistance = 5f; 
    public float speed = 2f; 
    public float waitTime; 
    public float detectionRange = 5f; 
    public float returnSpeed = 3f; 
    public Animator animator; 
    public SpriteRenderer spriteRenderer; 

    public Transform player; 
    public bool isChasingPlayer = false; 
    public bool isReturningToPatrol = false; 
    public Vector2 patrolStartPoint; 
    public bool movingRight = true; 

    public IEnumerator Patrol()
    {
        int randomWaittime = Random.Range(1, 5);
        waitTime = randomWaittime;
        while (true)
        {
            // Stop patrol if the enemy is dead
            if (isDead) 
            {
                animator.SetBool("isWalking", false);
                yield break;  // Exit the coroutine
            }

            if (!isChasingPlayer && !isReturningToPatrol)
            {
                animator.SetBool("isWalking", true); 

                FlipSprite(movingRight ? 1 : -1);

                if (movingRight)
                {
                    transform.position = Vector2.MoveTowards(transform.position, patrolStartPoint + Vector2.right * patrolDistance, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolStartPoint + Vector2.right * patrolDistance) < 0.1f)
                    {
                        movingRight = false;
                        animator.SetBool("isWalking", false);
                        yield return new WaitForSeconds(waitTime);
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, patrolStartPoint, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolStartPoint) < 0.1f)
                    {
                        movingRight = true;
                        animator.SetBool("isWalking", false);
                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }

            yield return null;
        }
    }

    public void ChasePlayer()
    {
 
        if (isDead) 
        {
            animator.SetBool("isWalking", false);
            return;  
        }

        if (isChasingPlayer)
        {
            animator.SetBool("isWalking", true); 

            FlipSprite(player.position.x - transform.position.x);

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public IEnumerator ReturnToPatrol()
    {
   
        if (isDead) 
        {
            animator.SetBool("isWalking", false);
            yield break;  
        }

        Vector2 targetPoint = movingRight ? patrolStartPoint + Vector2.right * patrolDistance : patrolStartPoint;
        animator.SetBool("isWalking", true); 

        FlipSprite(targetPoint.x - transform.position.x);

        while (Vector2.Distance(transform.position, targetPoint) > 0.1f && isReturningToPatrol)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, returnSpeed * Time.deltaTime);

           
            if (isDead)
            {
                animator.SetBool("isWalking", false);
                yield break;  
            }

            yield return null;
        }

        if (Vector2.Distance(transform.position, targetPoint) <= 0.1f)
        {
            animator.SetBool("isWalking", false); 
            isReturningToPatrol = false;
            StartCoroutine(Patrol()); 
        }
    }

    public void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - Vector3.right * patrolDistance, transform.position + Vector3.right * patrolDistance);
    }
}
