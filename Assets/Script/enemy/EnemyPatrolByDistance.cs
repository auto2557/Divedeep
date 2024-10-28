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
    public float separationDistance = 1.5f; 
    public Animator animator; 
    public SpriteRenderer spriteRenderer; 

    public Transform player; 
    public bool isChasingPlayer = false; 
    public bool isReturningToPatrol = false; 
    public Vector2 patrolStartPoint; 
    public bool movingRight = true; 

    public LayerMask enemyLayer; 

    public IEnumerator Patrol()
    {
        int randomWaittime = Random.Range(1, 5);
        waitTime = randomWaittime;
        while (true)
        {
            if (isDead) 
            {
                animator.SetBool("isWalking", false);
                yield break;  
            }

            if (!isChasingPlayer && !isReturningToPatrol)
            {
                animator.SetBool("isWalking", true); 

                FlipSprite(movingRight ? 1 : -1);

                Vector2 targetPosition = movingRight 
                    ? patrolStartPoint + Vector2.right * patrolDistance 
                    : patrolStartPoint;

                Vector2 adjustedPosition = AvoidOverlap(targetPosition);

                transform.position = Vector2.MoveTowards(transform.position, adjustedPosition, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, adjustedPosition) < 0.1f)
                {
                    movingRight = !movingRight;
                    animator.SetBool("isWalking", false);
                    yield return new WaitForSeconds(waitTime);
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

            Vector2 adjustedPosition = AvoidOverlap(player.position);

            transform.position = Vector2.MoveTowards(transform.position, adjustedPosition, speed * Time.deltaTime);
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
            Vector2 adjustedPosition = AvoidOverlap(targetPoint);

            transform.position = Vector2.MoveTowards(transform.position, adjustedPosition, returnSpeed * Time.deltaTime);

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

    private Vector2 AvoidOverlap(Vector2 targetPosition)
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationDistance, enemyLayer);

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.gameObject != gameObject)
            {
                Vector2 separationDirection = (transform.position - enemy.transform.position).normalized;
                targetPosition += separationDirection * 0.5f; 
            }
        }

        return targetPosition;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - Vector3.right * patrolDistance, transform.position + Vector3.right * patrolDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, separationDistance);
    }
}
