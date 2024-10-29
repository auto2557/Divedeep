using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3 : EnemyPatrolByDistance
{
    public GameObject meleeATK;
    private Coroutine meleeCoroutine;

    void Start()
    {
        hp = 5;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject Player = GameObject.FindWithTag("Player");
        Playerscript = Player.GetComponent<player>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        patrolStartPoint = transform.position;

        StartCoroutine(Patrol());
    }

    void Update()
    {
        if (hp <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Die());
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasingPlayer = true;
            isReturningToPatrol = false;
            StopCoroutine(Patrol()); 
            ChasePlayer();
        }
        else if (isChasingPlayer && distanceToPlayer > detectionRange)
        {
            isChasingPlayer = false;
            isReturningToPatrol = true;
            StartCoroutine(ReturnToPatrol());
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (meleeCoroutine == null)
            {
                meleeCoroutine = StartCoroutine(ToggleMeleeAttack());
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (meleeCoroutine != null)
            {
                StopCoroutine(meleeCoroutine);
                meleeCoroutine = null;
            }
            meleeATK.SetActive(false);
             anim.ResetTrigger("Attack"); 
        }
    }

    private IEnumerator ToggleMeleeAttack()
    {
        while (true)
        {
           meleeATK.SetActive(true);
            anim.SetTrigger("Attack"); 
            yield return new WaitForSeconds(2f);
            meleeATK.SetActive(false);
            yield return new WaitForSeconds(2f);
        
        }
    }

    public override IEnumerator Die()
    {
        anim.SetTrigger("Die");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(2f);
    }
}
