using UnityEngine;

public class SoundController : MonoBehaviour
{
    void Update()
    {
        // ตรวจสอบการกดปุ่มเพื่อเล่นเพลงพื้นหลัง
        if (Input.GetKeyDown(KeyCode.J))
        {
            SoundManager.instance.PlayBGM(0);  // เล่นเพลงพื้นหลังจาก index 0
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.instance.PlayBGM(1);  // เล่นเพลงพื้นหลังจาก index 1
        }

        // ตรวจสอบการกดปุ่มเพื่อเล่นเสียงเอฟเฟกต์
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundManager.instance.PlaySFX(0);  // เล่นเสียงเอฟเฟกต์จาก index 0
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SoundManager.instance.PlaySFX(1);  // เล่นเสียงเอฟเฟกต์จาก index 1
        }

        // ตรวจสอบการเปลี่ยนแปลงระดับเสียงเพลงพื้นหลัง
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.SetBGMVolume(1f);  // ปรับระดับเสียง BGM ขึ้น
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.instance.SetBGMVolume(0.5f);  // ปรับระดับเสียง BGM ลง
        }

        // ตรวจสอบการเปลี่ยนแปลง Master Volume
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundManager.instance.SetMasterVolume(0.5f);  // ลดระดับเสียงทั้งหมด 50%
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundManager.instance.SetMasterVolume(1f);  // เพิ่มระดับเสียงทั้งหมด 100%
        }
    }
}
