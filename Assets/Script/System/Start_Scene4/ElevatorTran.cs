using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTran : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform endPoint;
    public player player;
    public FadeImageAlpha[] fade;
    public GameObject[] black;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,2f); 
         player.enabled = false; 
         fade[0].enabled = false;
         fade[1].enabled = false;
    }

    void Update()
    {
        if (transform.position.y >= endPoint.position.y)
        {
            rb.velocity = Vector2.zero;
             player.enabled = true; 
             fade[0].enabled = true;
             fade[1].enabled = true;
        }
    }
}
