using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button[] menuButtons;    
    public GameObject[] Tab;     
    public Image highlight;           
    public Image arrowIcon;              
    
    protected int selectedIndex = 0;
    protected Vector3 targetArrowPosition;  
    public float arrowMoveSpeed = 5f;    
    public float arrowMoveRange = 10f;   
    public float arrowMoveFrequency = 2f; 
    public float lerpSpeed = 5f;        

    protected Vector3 currentArrowPosition; 

    void Start()
    {
        Dictionary<int, int> bgmSelections = new Dictionary<int, int>
        {
            { 0, 0 }
          
        };

        SoundManager.instance.PlayMultipleBGM(bgmSelections);
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
            SoundManager.instance.PlaySFX("other", 0,0);
        }

    
        float xOffset = Mathf.Sin(Time.time * arrowMoveFrequency) * arrowMoveRange;
        Vector3 desiredPosition = new Vector3(targetArrowPosition.x + xOffset, targetArrowPosition.y, targetArrowPosition.z);

        currentArrowPosition = Vector3.Lerp(currentArrowPosition, desiredPosition, Time.deltaTime * lerpSpeed);
        arrowIcon.transform.position = currentArrowPosition;
    }

    public virtual void UpdateMenuUI()
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

    public virtual void ExecuteSelectedOption()
    {
        switch (selectedIndex)
        {
            case 0:
                SceneManager.LoadScene("CutSceneBeforeGame"); 
                break;
            case 1:
                Debug.Log("Setting selected");
                Tab[1].gameObject.SetActive(true);
                Tab[0].gameObject.SetActive(false);
                selectedIndex = 1;
                break;
            case 2:
            SceneManager.LoadScene("credits");
                Debug.Log("Credit selected");
                break;
            case 3:
                Application.Quit();
                break;
        }
    }

    public void OnMouseEnterButton(int index)
    {
        selectedIndex = index;  
        UpdateMenuUI();
    }

    public void OnClickButton()
    {
        ExecuteSelectedOption();
    }
}
