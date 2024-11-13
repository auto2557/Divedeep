using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : EnemyPatrolByDistance
{
    private enemy1 enem1;
    void Start()
    {
        enem1 = GetComponent<enemy1>();
        hp = 60;
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
            speed = 0f;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("deadblock"))
        {
            Destroy(gameObject);
        }
    }

    public override IEnumerator Die()
    {
        anim.SetTrigger("Die");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        enem1.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
