using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // ตำแหน่งเดินลาดตระเวน (2 ตำแหน่ง)
    public float speed = 2f; // ความเร็วในการเดิน
    public float waitTime = 2f; // เวลาในการหยุดที่ตำแหน่ง
    public float detectionRange = 5f; // ระยะในการตรวจจับ Player
    public float returnSpeed = 3f; // ความเร็วในการกลับไปยังจุดลาดตระเวน
    public Animator animator; // อ้างอิงไปยัง Animator
    private SpriteRenderer spriteRenderer; // อ้างอิงไปยัง SpriteRenderer

    private Transform player; // อ้างอิง Player
    private int currentPointIndex = 0; // ตำแหน่งปัจจุบันที่ศัตรูจะไป
    private bool isChasingPlayer = false; // เช็คว่าไล่ตาม Player อยู่หรือไม่
    private bool isReturningToPatrol = false; // เช็คว่ากำลังกลับไปจุดลาดตระเวนอยู่หรือไม่

    private void Start()
    {
        // หา Player จากแท็ก
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // อ้างอิง SpriteRenderer และ Animator
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        StartCoroutine(Patrol());
    }

    private void Update()
    {
        // ตรวจสอบระยะระหว่างศัตรูกับ Player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasingPlayer = true;
            isReturningToPatrol = false;
            StopCoroutine(Patrol()); // หยุดการลาดตระเวนเมื่อเจอ Player
            ChasePlayer(); // เริ่มไล่ตาม Player
        }
        else if (isChasingPlayer && distanceToPlayer > detectionRange)
        {
            isChasingPlayer = false;
            isReturningToPatrol = true;
            StartCoroutine(ReturnToPatrol()); // กลับไปยังจุดลาดตระเวนเมื่อ Player ออกจากระยะ
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            if (!isChasingPlayer && !isReturningToPatrol)
            {
                // เคลื่อนที่ไปยังจุดลาดตระเวนที่กำหนด
                Transform targetPoint = patrolPoints[currentPointIndex];
                animator.SetBool("isWalking", true); // เปิดการเล่นแอนิเมชันการเดิน

                // ตรวจสอบการพลิกทิศทาง
                FlipSprite(targetPoint.position.x - transform.position.x);

                while (Vector2.Distance(transform.position, targetPoint.position) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
                    yield return null;
                }

                // รอ 2 วินาทีหลังจากถึงจุดลาดตระเวน
                animator.SetBool("isWalking", false); // หยุดการเล่นแอนิเมชันการเดิน
                yield return new WaitForSeconds(waitTime);

                // เปลี่ยนจุดลาดตระเวนไปยังจุดถัดไป
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }

            yield return null;
        }
    }

    private void ChasePlayer()
    {
        // เคลื่อนที่ไปหา Player
        if (isChasingPlayer)
        {
            animator.SetBool("isWalking", true); // เปิดการเล่นแอนิเมชันการเดิน

            // ตรวจสอบการพลิกทิศทาง
            FlipSprite(player.position.x - transform.position.x);

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private IEnumerator ReturnToPatrol()
    {
        // กลับไปยังจุดลาดตระเวนปัจจุบัน
        Transform targetPoint = patrolPoints[currentPointIndex];
        animator.SetBool("isWalking", true); // เปิดการเล่นแอนิเมชันการเดิน

        // ตรวจสอบการพลิกทิศทาง
        FlipSprite(targetPoint.position.x - transform.position.x);

        while (Vector2.Distance(transform.position, targetPoint.position) > 0.1f && isReturningToPatrol)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, returnSpeed * Time.deltaTime);
            yield return null;
        }

        if (Vector2.Distance(transform.position, targetPoint.position) <= 0.1f)
        {
            animator.SetBool("isWalking", false); // หยุดการเล่นแอนิเมชันการเดิน
            isReturningToPatrol = false;
            StartCoroutine(Patrol()); // กลับไปเดินลาดตระเวนต่อเมื่อถึงจุดแล้ว
        }
    }

    private void FlipSprite(float direction)
    {
        // ถ้าทิศทางเป็นบวก ให้หันไปทางขวา ถ้าทิศทางเป็นลบ ให้หันไปทางซ้าย
        if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // แสดงระยะการตรวจจับ Player ใน Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
