using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MenuController
{
     void Start()
    {
        UpdateMenuUI();
        currentArrowPosition = arrowIcon.transform.position;  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
             SoundManager.instance.PlaySFX("other", 0,0);
            UpdateMenuUI();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
             SoundManager.instance.PlaySFX("other", 0,0);
            UpdateMenuUI();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteSelectedOption();
        }

    
        float xOffset = Mathf.Sin(Time.time * arrowMoveFrequency) * arrowMoveRange;
        Vector3 desiredPosition = new Vector3(targetArrowPosition.x + xOffset, targetArrowPosition.y, targetArrowPosition.z);

        currentArrowPosition = Vector3.Lerp(currentArrowPosition, desiredPosition, Time.deltaTime * lerpSpeed);
        arrowIcon.transform.position = currentArrowPosition;
    }

    public override void ExecuteSelectedOption()
    {
        switch (selectedIndex)
        {
            case 0:
              Debug.Log("Control");
              Tab[2].gameObject.SetActive(true);
              Tab[3].gameObject.SetActive(false);
                break;
            case 1:
                Debug.Log("Audio");
                Tab[2].gameObject.SetActive(false);
                Tab[3].gameObject.SetActive(true);
                break;
            case 2:
                Debug.Log("Back");
                 Tab[1].gameObject.SetActive(true);
                Tab[0].gameObject.SetActive(false);
                Tab[2].gameObject.SetActive(false);
                Tab[3].gameObject.SetActive(false);
                selectedIndex = 2;
                break;
          
        }
    }

    

}
