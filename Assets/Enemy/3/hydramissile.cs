using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hydramissile : MonoBehaviour
{
     public float rotationSpeed;   
    public float speed;          
    public float delayBeforeLaunch;

    public Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isLaunched = false;

    void Start()
    {
        float RandomTime = Random.Range(1f, 5f);
        delayBeforeLaunch = RandomTime;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
     
        GameObject boss= GameObject.FindWithTag("Enemy");
        if (boss != null)
        {
            target = boss.transform;
        }

        StartCoroutine(LaunchAfterDelay());
        StartCoroutine(boom());
    }

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLaunch);
        isLaunched = true;
    }

    void Update()
    {
        if (target != null)
        {
          
            Vector2 direction = (target.position - transform.position).normalized;

           
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            
       
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

          
            if (isLaunched)
            {
              
                rb.velocity = transform.right * speed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
{
    if (collision != null && collision.CompareTag("Enemy"))
    {
        int damageMelee = 10;
        HydraBoss hydraBoss = collision.GetComponent<HydraBoss>();

        if (hydraBoss != null)
        {
            hydraBoss.TakeDamage(damageMelee);
        }

        anim.SetTrigger("boom");
        isLaunched = false; 
        rb.velocity = Vector2.zero;
        
        Destroy(gameObject, 1f); 
    }
}





    IEnumerator boom()
    {
        yield return new WaitForSeconds(30f);
        anim.SetTrigger("boom");
        isLaunched = false; 
        rb.velocity = Vector2.zero;
        Destroy(gameObject,1f);
        
    }
}
