using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraULcharge : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] charge;
    private int currentSpriteIndex = 0;
    private float timer = 0f;
    public float changeInterval = 1f; 

    public GameObject bulletPrefab;   
    public int bulletCount = 12;      
    public float bulletSpeed = 5f;   
    public float bulletSpawnInterval = 0.5f; 
    private float bulletTimer = 0f;  

    private bool canShoot = false;    

    void Start()
    {
        SoundManager.instance.PlaySFX("boss", 4, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
       
        currentSpriteIndex = 0;
        timer = 0f;
        bulletTimer = 0f;
        canShoot = false;
        spriteRenderer.sprite = charge[currentSpriteIndex]; 
    }

    void Update()
    {
             Camera.main.orthographicSize = 22f;

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;

           
            if (currentSpriteIndex < charge.Length)
            {
                spriteRenderer.sprite = charge[currentSpriteIndex];
                currentSpriteIndex++;
                
              
                if (currentSpriteIndex == charge.Length)
                {
                    canShoot = true; 
                    currentSpriteIndex = 0; 
                }
            }
        }

     
        if (canShoot)
        {
            bulletTimer += Time.deltaTime;

            if (bulletTimer >= bulletSpawnInterval)
            {
                bulletTimer = 0f;
                SoundManager.instance.PlaySFX("boss", 5, 1);
                ShootHellBullets();
                canShoot = false; 
            }
        }
    }

    void ShootHellBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            
            float angle = i * (360f / bulletCount);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = rotation * Vector2.right;
            rb.velocity = direction * bulletSpeed;
        }
    }
}
