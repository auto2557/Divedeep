using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftstop : MonoBehaviour
{
    private bool hasTriggered = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && !hasTriggered)
        {
           
            hasTriggered = true; 
             SoundManager.instance.PlaySFX("other", 1,2);
          

        

          
        }
    }

}
