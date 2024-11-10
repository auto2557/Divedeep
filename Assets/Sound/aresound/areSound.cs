using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areSound : MonoBehaviour
{
    private bool hasTriggered = false;
    public int num1;
    public int num2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if ((collision.CompareTag("Player") || collision.CompareTag("dashMode")) && !hasTriggered)
        {
            hasTriggered = true; 

     
            Dictionary<int, int> bgmSelections = new Dictionary<int, int>
            {
                { 1, 4 },
                { 2, 2 }
            };

            SoundManager.instance.PlayMultipleBGM(bgmSelections);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("dashMode"))
        {
           
            SoundManager.instance.StopBGM(1, 2);

            hasTriggered = false;
        }
    }
}
