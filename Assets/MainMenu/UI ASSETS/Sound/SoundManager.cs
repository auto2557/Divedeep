using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource;  // สำหรับเล่นเพลงพื้นหลัง
    public AudioSource sfxSource;  // สำหรับเล่นเสียงเอฟเฟกต์

    [SerializeField]
    private AudioClip[] bgmClips;  // Array สำหรับเก็บเพลงพื้นหลัง
    [SerializeField]
    private AudioClip[] sfxClips;  // Array สำหรับเก็บเสียงเอฟเฟกต์

    private float masterVolume = 0.5f;  // Master Volume เริ่มต้นที่ 100%
    private float bgmVolume = 0.5f;     // BGM Volume เริ่มต้นที่ 100%
    private float sfxVolume = 0.5f;     // SFX Volume เริ่มต้นที่ 100%

    public Slider masterSlider;   // Slider สำหรับ Master Volume
    public Slider bgmSlider;      // Slider สำหรับ BGM Volume
    public Slider sfxSlider;      // Slider สำหรับ SFX Volume

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // รักษา SoundManager เมื่อเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM(0);  // เล่นเพลงพื้นหลัง index 0 เมื่อเริ่มฉาก

        // กำหนดค่าจาก Slider
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // ตั้งค่าเริ่มต้นให้กับ Sliders
        masterSlider.value = masterVolume;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    // ฟังก์ชันสำหรับเล่นเพลงพื้นหลัง
    public void PlayBGM(int index)
    {
        if (index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index];
            bgmSource.Play();
        }
    }

    // ฟังก์ชันสำหรับเล่นเสียงเอฟเฟกต์
    public void PlaySFX(int index)
    {
        if (index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }

    // ปรับระดับเสียงของเพลงพื้นหลัง
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = bgmVolume * masterVolume;  // คูณด้วย Master Volume
    }

    // ปรับระดับเสียงของเสียงเอฟเฟกต์
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume * masterVolume;  // คูณด้วย Master Volume
    }

    // ฟังก์ชันสำหรับปรับ Master Volume
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);  // จำกัดค่าให้อยู่ระหว่าง 0 และ 1
        UpdateAllVolumes();
    }

    // ฟังก์ชันสำหรับอัพเดตระดับเสียงทั้งหมด
    private void UpdateAllVolumes()
    {
        bgmSource.volume = bgmVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
    }
}
