using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public int newBGMIndex;
    private bool hasTriggered = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; 

            Dictionary<int, int> bgmSelections = new Dictionary<int, int>
            {
                { 0, newBGMIndex } 
            };

            SoundManager.instance.PlayMultipleBGM(bgmSelections);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.StopAllBGM(); 
        }
    }
}
