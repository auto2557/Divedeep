using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float laserLength = 10f;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public LayerMask hitLayers;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        ShootLaserAtPlayer();
    }
void ShootLaserAtPlayer()
{
    if (player != null)
    {
        Vector2 directionToPlayer = (player.transform.position - firePoint.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, directionToPlayer, laserLength, hitLayers);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);

            // ตรวจสอบว่าเลเซอร์ชน Player หรือไม่
            /*if (hit.collider.CompareTag("Player"))
            {
                // ให้ Player รับดาเมจ
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10); // ทำดาเมจ 10
                }
            }*/
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, (Vector2)firePoint.position + directionToPlayer * laserLength);
        }
    }
}

}
