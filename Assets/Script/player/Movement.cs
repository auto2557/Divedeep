using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     
    public float jumpForce = 7f;
    public LayerMask groundLayer; // เลเยอร์สำหรับตรวจสอบพื้น
    public Transform groundCheck;  // ตำแหน่งจุดตรวจสอบพื้น
    public float groundCheckRadius = 0.2f;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isGrounded;
    protected bool canDoubleJump;
    protected bool facingRight = true;
    protected SpriteRenderer spriteRenderer;

    // Dash
    protected bool canDash = true;
    protected bool isDashing;
    public float dashingPower = 24f;
    protected float dashingTime = 0.4f;
    public float dashingCooldown = 1f;
    [SerializeField] protected TrailRenderer tr;
    

    protected void movement()
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

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("isJumping", true); 
    }

     protected void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    protected void AnimatePlayer()
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

    protected void Flip()
    {
         facingRight = !facingRight;
    spriteRenderer.flipX = !facingRight;
        
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
