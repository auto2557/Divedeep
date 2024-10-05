using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // ความเร็วในการเคลื่อนที่
    public float jumpForce = 7f;        // แรงกระโดด
    public float dashForce = 10f;       // แรงสำหรับ dash
    public float dashCooldown = 0.1f;     // เวลารอในการ dash
    private float dashCooldownTimer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool canDash;
    
    private bool facingRight = true;    // เช็คทิศทางการหันหน้าของตัวละคร
    private SpriteRenderer spriteRenderer; // อ้างอิงไปยัง SpriteRenderer ของตัวละคร

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // โหลด SpriteRenderer จาก GameObject
        dashCooldownTimer = 0f;
    }

    void Update()
    {
        // การเคลื่อนไหวซ้ายขวา
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // เช็คการ flip ภาพตามทิศทางการเคลื่อนที่
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // เช็คว่าผู้เล่นอยู่บนพื้นหรือไม่
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // กระโดด
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        // Dash
        dashCooldownTimer -= Time.deltaTime; // ลดเวลาคูลดาวน์

        if (Input.GetKeyDown(KeyCode.L) && dashCooldownTimer <= 0)
        {
            Dash();
        }
    }

    // ฟังก์ชันการกระโดด
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // ฟังก์ชัน Dash ตามทิศทางที่หันหน้า
    void Dash()
    {
        float dashDirection = facingRight ? 1f : -1f; // ถ้าหันขวา dash ไปทางขวา ถ้าหันซ้าย dash ไปทางซ้าย
        rb.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);
        dashCooldownTimer = dashCooldown; // รีเซ็ตคูลดาวน์หลังจาก dash
    }

    // ฟังก์ชันสำหรับการ flip ตัวละคร
    void Flip()
    {
        facingRight = !facingRight; // เปลี่ยนสถานะการหันหน้าของตัวละคร
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // ทำการ flip ในแกน X
        transform.localScale = scaler; // อัปเดตขนาดของตัวละคร
    }

    // เช็คการชนพื้น
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // เมื่อออกจากการชนพื้น
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
