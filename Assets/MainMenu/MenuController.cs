using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button[] menuButtons;        
    public Image highlight;           
    public Image arrowIcon;              
    private int selectedIndex = 0;       

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

  
    void UpdateMenuUI()
    {
        highlight.gameObject.SetActive(false);
        arrowIcon.gameObject.SetActive(false);

        highlight.gameObject.SetActive(true);
        arrowIcon.gameObject.SetActive(true);

        highlight.transform.position = menuButtons[selectedIndex].transform.position;
        arrowIcon.transform.position = new Vector3(menuButtons[selectedIndex].transform.position.x - 130, 
                                                   menuButtons[selectedIndex].transform.position.y, 0);

    }


    void ExecuteSelectedOption()
    {
        switch (selectedIndex)
        {
            case 0:
              
                SceneManager.LoadScene("DemoScene"); 
                break;
            case 1:
                Debug.Log("Setting selected");
                break;
            case 2:
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
