using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MenuController
{
    void Start()
    {
        UpdateMenuUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            UpdateMenuUI();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            UpdateMenuUI();
        }

      
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteSelectedOption();
        }
    }

    public override void ExecuteSelectedOption()
    {
        switch (selectedIndex)
        {
            case 0:
              Debug.Log("Control");
                break;
            case 1:
                Debug.Log("Audio");
                break;
            case 2:
                Debug.Log("Back");
                 Tab[1].gameObject.SetActive(true);
                Tab[0].gameObject.SetActive(false);
                break;
          
        }
    }

}
