using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     
    public float firstJumpForce = 7f;  
    public float secondJumpForce = 10f; 
    public LayerMask groundLayer;
    public Transform groundCheck;  
    public float groundCheckRadius = 0.2f;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool isGrounded;
    protected bool wasGrounded;
    protected bool canDoubleJump;
    public bool facingRight = true;
    protected SpriteRenderer spriteRenderer;

    // Dash
    protected bool canDash = true;
    protected bool isDashing;
    public float dashingPower = 24f;
    protected float dashingTime = 0.4f;
    public float dashingCooldown = 1f;
    [SerializeField] protected TrailRenderer tr;

    void Update()
    {
        CheckGround(); 
        movement();    
        AnimatePlayer();
    }

    protected virtual void movement()
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isGrounded)
            {
                Jump(firstJumpForce);  
                SoundManager.instance.PlaySFX("player", 1,4);
            }
            else if (canDoubleJump)
            {
                Jump(secondJumpForce); 
                canDoubleJump = false;  
                SoundManager.instance.PlaySFX("player", 2,2);
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            SoundManager.instance.PlaySFX("player", 0,0);
            StartCoroutine(Dash());
        }
    }

    protected void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
        animator.SetBool("isJumping", true); 
    }

    protected void CheckGround()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    protected void AnimatePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal");
        animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

        if (isGrounded && !wasGrounded) 
        {
            animator.SetBool("isLanding", true);  
        }
        else if (!isGrounded) 
        {
            animator.SetBool("isLanding", false); 
        }

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

    public IEnumerator Dash()
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
