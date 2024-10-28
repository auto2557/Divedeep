using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollRRcafe : MonoBehaviour
{
    public GameObject eme3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("hitblock")||collision.CompareTag("slash"))
        {
            Instantiate(eme3);
            Destroy(gameObject);
        }
    }
}
