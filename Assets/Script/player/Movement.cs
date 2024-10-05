using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool facingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        movement();
        AnimatePlayer(); 
    }

    void movement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

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
}
