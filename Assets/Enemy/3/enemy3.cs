using System.Collections;
using UnityEngine;

public class enemy3 : EnemyPatrolByDistance
{
    public GameObject meleeATK;
    private Coroutine meleeCoroutine;
    public GameObject missilePrefab1;
    public GameObject missilePrefab2;

    private Coroutine missileCoroutine;
    private bool canFireMissiles = true;
    private int missileCount = 0;
    private float cooldownTime;

    public Transform missileSpawnPoint1; 
    public Transform missileSpawnPoint2; 

    void Start()
    {
        hp = 100;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        cooldownTime = 20f;
        speed = 0.5f;
        returnSpeed = 0.5f;
        detectionRange = 7f;

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

      
        if (distanceToPlayer <= 4f && canFireMissiles)
        {
            if (missileCoroutine == null)
            {
                StopCoroutine(Patrol()); 
                missileCoroutine = StartCoroutine(SpawnMissiles());
            }
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
        }
    }

    private IEnumerator ToggleMeleeAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("isAttack", true);
            meleeATK.SetActive(true);

            yield return new WaitForSeconds(1.5f);
            meleeATK.SetActive(false);
            animator.SetBool("isAttack", false);
        }
    }

    private IEnumerator SpawnMissiles()
    {
        while (missileCount < 6)
        {
            
            animator.SetTrigger("Fire");

            
            Instantiate(missilePrefab1, missileSpawnPoint1.position, Quaternion.identity);

            
            Instantiate(missilePrefab2, missileSpawnPoint2.position, Quaternion.identity);

            missileCount++;
            yield return new WaitForSeconds(1f); 
        }

        canFireMissiles = false;
        missileCount = 0; 
        missileCoroutine = null;

        yield return new WaitForSeconds(cooldownTime); 

        canFireMissiles = true; 
        StartCoroutine(Patrol()); 
    }

    public override IEnumerator Die()
    {
        animator.SetTrigger("Die");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
