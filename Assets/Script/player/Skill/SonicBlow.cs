using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBlow : MonoBehaviour
{
    public GameObject boomSlash;
    void Start()
    {
        Destroy(gameObject,1.5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Instantiate(boomSlash, transform.position, transform.rotation);
            Destroy(gameObject,0.1f);
        }
    }

}
