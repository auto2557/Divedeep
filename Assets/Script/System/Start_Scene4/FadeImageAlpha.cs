using UnityEngine;
using UnityEngine.UI;

public class FadeImageAlpha : MonoBehaviour
{
    public float fadeDuration = 2f; 
    private Image image;
    private Color originalColor;
    private float fadeTimer;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
            fadeTimer = 0f;
        }
    }

    void Update()
    {
        if (image != null)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, fadeTimer / fadeDuration); 
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (fadeTimer >= fadeDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
