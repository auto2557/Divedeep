using UnityEngine;

public class SoundController : MonoBehaviour
{
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.J))
        {
            SoundManager.instance.PlayBGM(0);  
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.instance.PlayBGM(1);  
        }

      
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundManager.instance.PlaySFX(0);  
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SoundManager.instance.PlaySFX(1);  
        }

       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.SetBGMVolume(1f);  
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.instance.SetBGMVolume(0.5f);  
        }

    
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundManager.instance.SetMasterVolume(0.5f);  
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundManager.instance.SetMasterVolume(1f);  
        }
    }
}
