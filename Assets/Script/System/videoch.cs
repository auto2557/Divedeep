using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoch : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneName;

    void Start()
    {
        // ตรวจสอบเมื่อวิดีโอเล่นจบ
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        // ตรวจสอบเมื่อกด spacebar
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
