using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgEffectRange : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 7f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
