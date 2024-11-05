using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f; 
    public int attackDamage;    
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f; 

    public Transform player; 
    public LayerMask playerLayer; 

    private enemyHP enemyHpScript; 
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHpScript = GetComponent<enemyHP>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemyHpScript.hp <= 0)
        {
            return; 
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; 
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null) 
            {
                int randomDamage = Random.Range(3,7);
                attackDamage = randomDamage;
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
