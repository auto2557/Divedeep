using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource;  
    public AudioSource sfxSource;  

    [SerializeField]
    private AudioClip[] bgmClips;  
    
    public AudioClip[] sfxClips;  
    private float masterVolume = 0.5f;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

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
        }
    }

    private void Start()
    {
        LoadVolumeSettings();
        PlayBGM(0);

    
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

       
        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    public void PlayBGM(int index)
    {
        if (index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index];
            bgmSource.Play();
        }
    }

    public void PlaySFX(int index)
    {
        if (index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = bgmVolume * masterVolume;
        SaveVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume * masterVolume;
        SaveVolumeSettings();
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);
        UpdateAllVolumes();
        SaveVolumeSettings();
    }

    private void UpdateAllVolumes()
    {
        bgmSource.volume = bgmVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
    }


    [System.Serializable]
    private class VolumeSettings
    {
        public float masterVolume;
        public float bgmVolume;
        public float sfxVolume;
    }

    private void SaveVolumeSettings()
    {
        VolumeSettings settings = new VolumeSettings
        {
            masterVolume = masterVolume,
            bgmVolume = bgmVolume,
            sfxVolume = sfxVolume
        };
        
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(Application.persistentDataPath + "/volumeSettings.json", json);
    }

    private void LoadVolumeSettings()
    {
        string path = Application.persistentDataPath + "/volumeSettings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            VolumeSettings settings = JsonUtility.FromJson<VolumeSettings>(json);
            masterVolume = settings.masterVolume;
            bgmVolume = settings.bgmVolume;
            sfxVolume = settings.sfxVolume;
        }
    }
}
