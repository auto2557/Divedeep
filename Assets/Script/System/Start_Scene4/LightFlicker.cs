using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightFlicker : MonoBehaviour
{
    public Light2D light2D;
    public float minIntensity = 0.5f; 
    public float maxIntensity = 1.5f; 
    public float flickerSpeed = 1.0f; 

    private void Update()
    {
        float intensity = Mathf.PingPong(Time.time * flickerSpeed, maxIntensity - minIntensity) + minIntensity;
        light2D.intensity = intensity;
    }
}
