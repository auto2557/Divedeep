using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : Attack
{
    
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
        CheckGround();
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("deadblock"))
        {
            StartCoroutine(die());
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
