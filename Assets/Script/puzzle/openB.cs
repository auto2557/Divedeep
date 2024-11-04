using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openB : MonoBehaviour
{
    private Animator anim;
    public Image finishQuetsB;       
    public Sprite newSprite;           
    public bool GenB = false;
    public GameObject lightPower;
    private bool isInTrigger = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        lightPower.SetActive(false);
    }

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("On", true);
            GenB = true;
            lightPower.SetActive(true);

         
            if (newSprite != null)
            {
                finishQuetsB.sprite = newSprite;
            }
            
            Debug.Log("open A gen");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}
