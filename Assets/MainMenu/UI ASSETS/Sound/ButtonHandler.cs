using UnityEngine;
using UnityEngine.UI; // สำหรับการใช้งาน UI

public class ButtonHandler: MonoBehaviour
{
    public Image targetImage; // อ้างอิง Image ที่ต้องการเปลี่ยน
    public Sprite image1; // ภาพแรก
    public Sprite image2; // ภาพที่สอง

    private bool isUsingImage1 = true; // ติดตามสถานะภาพปัจจุบัน

    // ฟังก์ชันที่จะเรียกใช้เมื่อกดปุ่ม
    public void SwitchImage()
    {
        if (isUsingImage1)
        {
            targetImage.sprite = image2; // เปลี่ยนไปใช้ image2
        }
        else
        {
            targetImage.sprite = image1; // เปลี่ยนกลับไปใช้ image1
        }
        isUsingImage1 = !isUsingImage1; // เปลี่ยนสถานะ
    }
}
