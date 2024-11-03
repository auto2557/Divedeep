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
    
    [SerializeField] private List<AudioClip> playerSFXClips;
    [SerializeField] private List<AudioClip> AquaStingerSFXClips;
    [SerializeField] private List<AudioClip> AquaAnemoneSFXClips;
    [SerializeField] private List<AudioClip> RustyColossusSFXClips;
    [SerializeField] private List<AudioClip> HydraSFXClips;
    [SerializeField] private List<AudioClip> otherSFXClips;

    private Dictionary<string, List<AudioClip>> sfxClips;  
    
    private float masterVolume = 0.5f;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Button muteMasterButton;
    public Button muteBGMButton;
    public Button muteSFXButton;
    public Button resetVolumeButton;

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
        InitializeSFXCategories();
        PlayBGM(0);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        muteMasterButton.onClick.AddListener(MuteMasterVolume);
        muteBGMButton.onClick.AddListener(MuteBGMVolume);
        muteSFXButton.onClick.AddListener(MuteSFXVolume);
        resetVolumeButton.onClick.AddListener(ResetAllVolumes);
    }

    private void InitializeSFXCategories()
    {
        sfxClips = new Dictionary<string, List<AudioClip>>
        {
            { "player", playerSFXClips },
            { "enemy1", AquaStingerSFXClips },
            { "enemy2", AquaAnemoneSFXClips },
            { "enemy3", RustyColossusSFXClips },
            { "boss", HydraSFXClips },
            { "other", otherSFXClips }
        };
    }

    public void PlayBGM(int index)
    {
        if (index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index];
            bgmSource.Play();
        }
    }

    public void PlaySFX(string category, int index)
    {
        if (sfxClips.ContainsKey(category) && index < sfxClips[category].Count)
        {
            sfxSource.PlayOneShot(sfxClips[category][index]);
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

            UpdateAllVolumes();
        }
    }

    // Button functions
    public void MuteMasterVolume()
    {
        SetMasterVolume(0f);
        masterSlider.value = 0f;
    }

    public void MuteBGMVolume()
    {
        SetBGMVolume(0f);
        bgmSlider.value = 0f;
    }

    public void MuteSFXVolume()
    {
        SetSFXVolume(0f);
        sfxSlider.value = 0f;
    }

    public void ResetAllVolumes()
    {
        SetMasterVolume(0.5f);
        SetBGMVolume(0.5f);
        SetSFXVolume(0.5f);

        masterSlider.value = 0.5f;
        bgmSlider.value = 0.5f;
        sfxSlider.value = 0.5f;
    }
}
