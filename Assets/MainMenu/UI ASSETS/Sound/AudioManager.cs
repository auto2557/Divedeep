using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;  // สำหรับเล่นเพลงพื้นหลัง
    public AudioSource sfxSource;  // สำหรับเล่นเสียงเอฟเฟกต์

    private string filePath;
    private AudioSettings audioSettings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // รักษา AudioManager เมื่อเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ตั้งค่า filePath สำหรับบันทึก JSON
        filePath = Path.Combine(Application.persistentDataPath, "audioSettings.json");

        LoadAudioSettings();  // โหลดการตั้งค่าเสียงเมื่อเริ่ม
    }

    private void Start()
    {
        // เล่นเพลงพื้นหลัง
        PlayBGM(0);
    }

    // ฟังก์ชันสำหรับบันทึกการตั้งค่าเสียงลงในไฟล์ JSON
    public void SaveAudioSettings()
    {
        string json = JsonUtility.ToJson(audioSettings, true);
        File.WriteAllText(filePath, json);
    }

    // ฟังก์ชันสำหรับโหลดการตั้งค่าเสียงจากไฟล์ JSON
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
            // ถ้าไฟล์ไม่อยู่ ให้ใช้ค่าดีฟอลต์
            audioSettings = new AudioSettings
            {
                masterVolume = 0.5f,
                bgmVolume = 0.5f,
                sfxVolume = 0.5f
            };

            SaveAudioSettings();  // บันทึกการตั้งค่าเสียงครั้งแรก
        }
    }

    // ฟังก์ชันสำหรับปรับระดับเสียงและใช้ค่าจาก JSON
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
        SaveAudioSettings();  // บันทึกการตั้งค่าเสียงทุกครั้งที่เปลี่ยน
    }

    // ฟังก์ชันสำหรับตั้งค่า BGM Volume
    public void SetBGMVolume(float volume)
    {
        audioSettings.bgmVolume = Mathf.Clamp(volume, 0f, 1f);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    // ฟังก์ชันสำหรับตั้งค่า SFX Volume
    public void SetSFXVolume(float volume)
    {
        audioSettings.sfxVolume = Mathf.Clamp(volume, 0f, 1f);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    // ฟังก์ชันสำหรับเล่นเพลงพื้นหลัง
    public void PlayBGM(int index)
    {
        bgmSource.Play();
    }
}
