using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CreditScroller : MonoBehaviour
{
    public TextMeshProUGUI creditText; // Drag your TextMeshProUGUI object here
    public float scrollSpeed = 50f; // Speed of scrolling in units per second
    public float endDelay = 5f; // Delay before changing scenes when credits finish

    private RectTransform creditTransform;
    private bool creditsFinished = false;

    private void Start()
    {
        // Get the RectTransform of the TextMeshPro object
        creditTransform = creditText.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!creditsFinished)
        {
            // Move the text up along the Y-axis based on scroll speed
            creditTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // Check if the text has moved off the screen
            if (creditTransform.anchoredPosition.y > Screen.height + creditTransform.rect.height)
            {
                creditsFinished = true;
                StartCoroutine(WaitAndChangeScene());
            }
        }

        // Check for Spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }

    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(endDelay); // Wait for the specified delay
        ChangeScene();
    }

    private void ChangeScene()
    {
        // Load the next scene (replace "Main" with the actual name of your scene)
        SceneManager.LoadScene("Main");
    }
}
