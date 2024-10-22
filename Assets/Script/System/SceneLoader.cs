using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            // เราสามารถแสดง progress bar หรือทำอะไรระหว่างโหลดได้ที่นี่
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}
