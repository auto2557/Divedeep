using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameController : MenuController
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
            SoundManager.instance.PlaySFX(0);
            UpdateMenuUI();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            SoundManager.instance.PlaySFX(0);
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

    public override void UpdateMenuUI()
    {
        highlight.gameObject.SetActive(false);
        arrowIcon.gameObject.SetActive(false);

        highlight.gameObject.SetActive(true);
        arrowIcon.gameObject.SetActive(true);

        highlight.transform.position = menuButtons[selectedIndex].transform.position;
        
        targetArrowPosition = new Vector3(menuButtons[selectedIndex].transform.position.x - 300, 
                                          menuButtons[selectedIndex].transform.position.y, 
                                          0);

        currentArrowPosition = arrowIcon.transform.position;
    }

    public override void ExecuteSelectedOption()
    {
        switch (selectedIndex)
        {
            case 0:
                
                break;
            case 1:
                Tab[0].gameObject.SetActive(true);
                Tab[1].gameObject.SetActive(false);
                selectedIndex = 1;
                break;
            case 2:
                 Tab[1].gameObject.SetActive(true);
                Tab[0].gameObject.SetActive(false);
                selectedIndex = 2;
                break;
            case 3:
                SceneManager.LoadScene("Main"); 
                break;
        }
    }

}
