using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f; // ระยะการโจมตี
    public int attackDamage = 10;    // ความเสียหายของการโจมตี
    public float attackCooldown = 2f; // เวลารอการโจมตีถัดไป
    private float nextAttackTime = 0f; 

    public Transform player; // อ้างอิงไปยังตำแหน่งของผู้เล่น
    public LayerMask playerLayer; // ใช้ตรวจสอบผู้เล่นในระยะโจมตี

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับผู้เล่น
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ตรวจสอบว่าผู้เล่นอยู่ในระยะโจมตีและสามารถโจมตีได้
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // ตั้งเวลาสำหรับการโจมตีถัดไป
        }
    }

    void Attack()
{
    // เริ่มการโจมตี (เล่นแอนิเมชันโจมตี)
    if (animator != null)
    {
        animator.SetTrigger("Attack");
    }

    // ตรวจสอบว่าผู้เล่นอยู่ในระยะและได้รับความเสียหาย
    Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
    foreach (Collider2D player in hitPlayers)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null) // ตรวจสอบว่ามี PlayerHealth component
        {
            playerHealth.TakeDamage(attackDamage); // เรียกฟังก์ชันรับความเสียหายใน PlayerHealth
        }
    }
}


    // ใช้เพื่อแสดงการโจมตีในระยะใน Scene View (ไม่จำเป็น)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
