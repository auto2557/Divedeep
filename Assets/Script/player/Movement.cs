using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     
    public float jumpForce = 7f;
    public GameObject hitBlockRight; 
    public GameObject hitBlockLeft;  
    public LayerMask groundLayer; // เลเยอร์สำหรับตรวจสอบพื้น
    public Transform groundCheck;  // ตำแหน่งจุดตรวจสอบพื้น
    public float groundCheckRadius = 0.2f;

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
    private float comboResetTime = 1.5f; 
    private float lastAttackTime;
    private bool isAttacking = false;

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
        if (isDashing || isAttacking)
        {
            return;
        }
        CheckGround(); // เพิ่มฟังก์ชันตรวจสอบพื้น
        movement();
        AnimatePlayer();
        UpdateHitBlockPosition();
        HandleAttack(); 
    }

    private void FixedUpdate()
    {
        if (isDashing || isAttacking)
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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isAttacking && stateInfo.normalizedTime < 1f)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
          rb.velocity = Vector2.zero;

            if (Time.time - lastAttackTime > comboResetTime)
            {
                attackCount = 0;
            }

            attackCount++; 
            lastAttackTime = Time.time;

            if (attackCount > 4)
            {
                attackCount = 1;
            }

            if (!isGrounded)
            {
                PlayJumpAttack();
            }
            else
            {
                PlayAttackAnimation(attackCount); 
            }
        }
    }

    private void PlayJumpAttack()
    {
        isAttacking = true; 
        animator.Play("JumpAttack"); 
        if (facingRight == false)
                {
                    rb.velocity = Vector2.left;
                }
                else if (facingRight == true)
                {
                    rb.velocity = Vector2.one;
                }

        StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    private void PlayAttackAnimation(int attackStep)
    {
        isAttacking = true; 

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
                if (facingRight == false)
                {
                    rb.velocity = Vector2.left;
                }
                else if (facingRight == true)
                {
                    rb.velocity = Vector2.one;
                }
                break;
            case 4:
                animator.Play("Attack4");
                break;
        }

        StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator ResetAttackState(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        isAttacking = false;
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
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
