using UnityEngine;

public class AlphaPulsate : MonoBehaviour
{
    [SerializeField] private float minAlpha = 0.2f; 
    [SerializeField] private float maxAlpha = 1.0f; 
    [SerializeField] private float speed = 1.0f;    
    private SpriteRenderer spriteren;

    void Start()
    {
        spriteren = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * speed, 1));

        if (spriteren != null)
        {
            Color color = spriteren.color;
            color.a = alpha;
            spriteren.color = color;
        }
    }
}
