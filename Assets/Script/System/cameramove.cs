using UnityEngine;

public class cameramove : MonoBehaviour
{
    private Vector3 offset = new Vector3(1.5f, 1f, -10f);
    private float smoothTime = 0.001f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}