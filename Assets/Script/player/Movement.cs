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
    private float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

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
