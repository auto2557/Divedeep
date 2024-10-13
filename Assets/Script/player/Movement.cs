using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     
    public float jumpForce = 7f;
    public GameObject hitBlockRight; 
    public GameObject hitBlockLeft;  

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;

    // Dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    private float dashingTime = 0.4f;
    public float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    // Attack combo
    private int attackCount = 0; 
    private float comboResetTime = 2f; // เวลาที่คอมโบจะรีเซ็ต
    private float lastAttackTime;
    private bool isAttacking = false; // ตรวจสอบว่าอนิเมชั่นโจมตีกำลังเล่นอยู่หรือไม่

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 

        hitBlockLeft.SetActive(false);
        hitBlockRight.SetActive(true);  
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        movement();
        AnimatePlayer();
        UpdateHitBlockPosition();
        HandleAttack(); // เพิ่มการตรวจจับการโจมตี
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void movement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }

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

        if (Input.GetKeyDown(KeyCode.L) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("isJumping", true); 
    }

    void AnimatePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal");
        animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

        if (isGrounded)
        {
            animator.SetBool("isJumping", false); 
        }

        if (isDashing)
        {
        animator.SetBool("isDash", true);  
        gameObject.transform.tag = "dashMode";
        }
        else
        {
        animator.SetBool("isDash", false); 
        gameObject.transform.tag = "Player";
        }
    }

    void Flip()
    {
        facingRight = !facingRight; 
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; 
        transform.localScale = scaler;
    }

    void UpdateHitBlockPosition()
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

    private void HandleAttack()
    {
        // ตรวจสอบว่าอนิเมชั่นโจมตีเสร็จหรือยัง
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isAttacking && stateInfo.normalizedTime < 1f)
        {
            // หากอนิเมชั่นยังไม่จบ ยังไม่อนุญาตให้โจมตีครั้งต่อไป
            return;
        }

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            if (Time.time - lastAttackTime > comboResetTime)
            {
                // รีเซ็ตคอมโบถ้ากดไม่ต่อเนื่อง
                attackCount = 0;
            }

            attackCount++; 
            lastAttackTime = Time.time;

            // จำกัดคอมโบที่ 4 ครั้ง
            if (attackCount > 4)
            {
                attackCount = 1;
            }

            PlayAttackAnimation(attackCount); 
        }
    }

    private void PlayAttackAnimation(int attackStep)
    {
        isAttacking = true; // ระบุว่ากำลังโจมตีอยู่

        // กำหนดการเล่นอนิเมชั่นตามคอมโบ
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
                break;
            case 4:
                animator.Play("Attack4");
                break;
        }

        // รีเซ็ตสถานะการโจมตีหลังจากอนิเมชั่นเสร็จ
        StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator ResetAttackState(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        
        rb.velocity = new Vector2((facingRight ? 1 : -1) * dashingPower, 0f);
        
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        
        rb.velocity = new Vector2(0f, rb.velocity.y);
        
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
