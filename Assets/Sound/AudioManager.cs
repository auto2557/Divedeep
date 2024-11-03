using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;  
    public AudioSource sfxSource;  

    private string filePath;
    private AudioSettings audioSettings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

       
        filePath = Path.Combine(Application.persistentDataPath, "audioSettings.json");

        LoadAudioSettings();  
    }

    private void Start()
    {
        
        PlayBGM(0);
    }

   
    public void SaveAudioSettings()
    {
        string json = JsonUtility.ToJson(audioSettings, true);
        File.WriteAllText(filePath, json);
    }

   
    public void LoadAudioSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            audioSettings = JsonUtility.FromJson<AudioSettings>(json);

            ApplyAudioSettings();
        }
        else
        {
    
            audioSettings = new AudioSettings
            {
                masterVolume = 0.5f,
                bgmVolume = 0.5f,
                sfxVolume = 0.5f
            };

            SaveAudioSettings();  
        }
    }


    private void ApplyAudioSettings()
    {
        bgmSource.volume = audioSettings.bgmVolume * audioSettings.masterVolume;
        sfxSource.volume = audioSettings.sfxVolume * audioSettings.masterVolume;
    }

    // ฟังก์ชันสำหรับตั้งค่า Master Volume
    public void SetMasterVolume(float volume)
    {
        audioSettings.masterVolume = Mathf.Clamp(volume, 0f, 1f);
        ApplyAudioSettings();
        SaveAudioSettings();  
    }


    public void SetBGMVolume(float volume)
    {
        audioSettings.bgmVolume = Mathf.Clamp(volume, 0f, 1f);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    public void SetSFXVolume(float volume)
    {
        audioSettings.sfxVolume = Mathf.Clamp(volume, 0f, 1f);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

   
    public void PlayBGM(int index)
    {
        bgmSource.Play();
    }
}
