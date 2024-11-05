using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreditScroller : MonoBehaviour
{
   public TextMeshProUGUI creditText; // Drag your TextMeshProUGUI object here
    public float scrollSpeed = 50f; // Speed of scrolling in units per second

    private RectTransform creditTransform;

    private void Start()
    {
        // Get the RectTransform of the TextMeshPro object
        creditTransform = creditText.GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Move the text up along the Y-axis based on scroll speed
        creditTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Check if the text has moved off the screen
        if (creditTransform.anchoredPosition.y > Screen.height + creditTransform.rect.height)
        {
            // Change scene when credits finish scrolling
            ChangeScene();
        }

        // Check for Spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        // Load the next scene (replace "YourSceneName" with the actual name of your scene)
        SceneManager.LoadScene("Main");
    }
}
