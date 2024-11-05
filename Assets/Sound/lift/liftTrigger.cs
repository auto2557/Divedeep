using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftTrigger : MonoBehaviour
{
    private bool hasTriggered = false; 
    public int num1;
    public int num2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            SoundManager.instance.PlaySFX("other", 1,1);
            hasTriggered = true; 

            Dictionary<int, int> bgmSelections = new Dictionary<int, int>
            {
                { num1, num2 } 
               
            };

            SoundManager.instance.PlayMultipleBGM(bgmSelections);

          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.StopBGM(num1,num2); 
        }
    }
}
