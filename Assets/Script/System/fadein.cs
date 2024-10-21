using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Drag your Canvas Group here in the inspector
    public float fadeDuration = 2f; // Time for the fade

    private void Start()
    {
        // เริ่มทำการ Fade In เมื่อ Scene เริ่มต้น
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = 0; // เริ่มที่จอมืดสนิท

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // ปรับค่า alpha จาก 0 ไป 1 ตามเวลา
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null; // รอจนกว่าเฟรมถัดไป
        }

        canvasGroup.alpha = 1; // จบการ Fade In ที่ความโปร่งใส 100%
    }
}
