using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : enemyHP
{
    public Transform player; 
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float fireRate = 1f; 
    public float shootingRangeX = 10f; 
    public float bulletSpeed = 10f; 
    private enemy2 enem2;
    private float nextFireTime;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {

        enem2 = GetComponent<enemy2>();
        bulletSpeed = 5f;
         hp = 40;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject Player = GameObject.FindWithTag("Player");
        Playerscript = Player.GetComponent<player>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float distanceToPlayerX = Mathf.Abs(direction.x);

       
        spriteRenderer.flipX = direction.x < 0;

        if (distanceToPlayerX <= shootingRangeX && Time.time >= nextFireTime)
        {
            ShootAtPlayer(direction.x);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void ShootAtPlayer(float directionX)
    {
       
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        float bulletDirection = directionX > 0 ? 1f : -1f;
        rb.velocity = new Vector2(bulletDirection * bulletSpeed, 0); 

        bullet.transform.localScale = new Vector3(bulletDirection * Mathf.Abs(bullet.transform.localScale.x), bullet.transform.localScale.y, bullet.transform.localScale.z);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead && !isHit)
        {
            isHit = true; 

            int damageAmount = Playerscript.damage;  
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            if (hp <= 0)
            {
                isDead = true;
                StartCoroutine(Die());
            }

            StartCoroutine(ResetHit()); 
        }
    }

     public override IEnumerator Die()
    {
        anim.SetTrigger("Die");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        enem2.enabled = false;
        yield return new WaitForSeconds(2f); 
    }
}
