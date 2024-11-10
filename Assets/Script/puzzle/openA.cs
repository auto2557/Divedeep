using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openA : MonoBehaviour
{
    private Animator anim;
    public GameObject UnlockDanger;
    public Image finishQuetsA;       
    public Sprite newSprite;     
    public GameObject E1;      
    public bool GenA = false;
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
            GenA = true;
            lightPower.SetActive(true);
            Destroy(UnlockDanger);
            Destroy(E1);
         
            if (newSprite != null)
            {
                finishQuetsA.sprite = newSprite;
            }
            
            Debug.Log("open A gen");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("dashMode"))
        {
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("dashMode"))
        {
            isInTrigger = false;
        }
    }
}
