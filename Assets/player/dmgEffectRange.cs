using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgEffectRange : MonoBehaviour
{
    public GameObject Range;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(Range, transform.position, transform.rotation);
            Destroy(Range, 1f);
        }
    }
}
