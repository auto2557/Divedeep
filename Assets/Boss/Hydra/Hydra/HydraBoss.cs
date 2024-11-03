using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for using UI elements like Slider

public class HydraBoss : enemyHP
{
    public Transform player; 
    public float speed;
    private Animator animator;
    
    // Add references to the three health sliders
    public Slider healthSlider1;
    public Slider healthSlider2;
    public Slider healthSlider3;

    private const int maxHPPerSlider = 1000; // Each slider represents 1000 HP
    
    void Start()
    {
        speed = 1f;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        hp = 3000; 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject Player = GameObject.FindWithTag("Player");
        Playerscript = Player.GetComponent<player>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();

        healthSlider1.maxValue = maxHPPerSlider;
        healthSlider2.maxValue = maxHPPerSlider;
        healthSlider3.maxValue = maxHPPerSlider;

        healthSlider1.value = maxHPPerSlider;
        healthSlider2.value = maxHPPerSlider;
        healthSlider3.value = maxHPPerSlider;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead && !isHit)
        {
            isHit = true; 

            int damageAmount = Playerscript.damage;  
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            UpdateHealthSliders();

            if (hp <= 0)
            {
                isDead = true;
                StartCoroutine(Die());
            }

            StartCoroutine(ResetHit()); 
        }
    }

    private void UpdateHealthSliders()
    {
        int remainingHP = hp;

        healthSlider1.value = Mathf.Clamp(remainingHP, 0, maxHPPerSlider);
        remainingHP -= maxHPPerSlider;

        healthSlider2.value = Mathf.Clamp(remainingHP, 0, maxHPPerSlider);
        remainingHP -= maxHPPerSlider;

        healthSlider3.value = Mathf.Clamp(remainingHP, 0, maxHPPerSlider);
    }
}
