using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoch : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        ChangeScene();
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
