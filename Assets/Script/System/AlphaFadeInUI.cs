using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaFadeInUI : MonoBehaviour
{
    public float fadeDuration; 
    private Image image;

    void Start()
    {

        image = GetComponent<Image>();
       fadeDuration = 3f;
       
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color color = image.color;
        color.a = 0; 
        image.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
           
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            image.color = color;

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

       
        color.a = 1;
        image.color = color;
    }
}
