using UnityEngine;

public class StopAudioOnStart : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
