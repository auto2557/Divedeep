using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro textMesh; 

    private float disappearTimer;
    private Color textColor;

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    private void Update()
{

    float moveXSpeed = 1f; 
    float moveYSpeed = 2f; 

    transform.position += new Vector3(moveXSpeed, moveYSpeed) * Time.deltaTime;

    disappearTimer -= Time.deltaTime;
    if (disappearTimer <= 0)
    {
        float disappearSpeed = 1f;
        textColor.a -= disappearSpeed * Time.deltaTime;
        textMesh.color = textColor;
        if (textColor.a <= 0)
        {
            Destroy(gameObject); 
        }
    }
}

}
