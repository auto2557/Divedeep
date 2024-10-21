using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void StartFade(float fadeTime, bool fadeIn)
    {
        StartCoroutine(Fade(fadeTime, fadeIn));
    }

    IEnumerator Fade(float fadeTime, bool fadeIn)
    {
        float startAlpha = canvasGroup.alpha;
        float endAlpha = fadeIn ? 1 : 0;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeTime);
            yield return null;
        }
    }
}
