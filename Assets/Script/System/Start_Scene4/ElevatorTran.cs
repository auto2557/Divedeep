using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTran : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform endPoint;
    public player player;
    public GameObject[] black;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,1f); 
         player.enabled = false; 
    }

    void Update()
    {
        if (transform.position.y >= endPoint.position.y)
        {
            rb.velocity = Vector2.zero;
             player.enabled = true; 
             black[0].SetActive(false);
              black[1].SetActive(false);
        }
    }
}
