using UnityEngine;
using UnityEngine.UI; 

public class ButtonHandler: MonoBehaviour
{
    public Image targetImage; 
    public Sprite image1; 
    public Sprite image2; 

    private bool isUsingImage1 = true; 

   
    public void SwitchImage()
    {
        if (isUsingImage1)
        {
            targetImage.sprite = image2; 
        }
        else
        {
            targetImage.sprite = image1; 
        }
        isUsingImage1 = !isUsingImage1; 
    }
}
