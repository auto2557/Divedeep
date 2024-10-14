using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // เป้าหมาย เช่น Player
    public Transform target;

    // ความยาวสูงสุดของเลเซอร์
    public float maxLaserLength = 10f;

    // ความเร็วในการขยายเลเซอร์
    public float laserGrowthSpeed = 5f;

    // ตัวแปรในการเก็บความยาวเลเซอร์ปัจจุบัน
    private float currentLaserLength = 0f;

    // เก็บสถานะว่าเลเซอร์กำลังยิงหรือไม่
    private bool isLaserShooting = false;

    // เก็บทิศทางที่เลเซอร์จะยิง
    private Vector2 directionToTarget;

    // ระยะเพิ่มหลังจากชนเป้าหมาย
    public float extraDistanceAfterHit = 20f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (!isLaserShooting)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;

                ShootLaser();
            }
        }
    }

    private void ShootLaser()
    {
        if (target == null) return; 

        
        directionToTarget = (target.position - transform.position).normalized;

     
        float distanceToTarget = Vector2.Distance(transform.position, target.position) + extraDistanceAfterHit;

      
        currentLaserLength = 0f;

       
        isLaserShooting = true;

        
        StartCoroutine(ExpandLaser(distanceToTarget));
    }

    private IEnumerator ExpandLaser(float distanceToTarget)
    {
        while (currentLaserLength < distanceToTarget)
        {
     
            currentLaserLength += laserGrowthSpeed * Time.deltaTime;
            currentLaserLength = Mathf.Min(currentLaserLength, distanceToTarget); 

          
            lineRenderer.SetPosition(0, transform.position);

         
            Vector2 laserEndPoint = (Vector2)transform.position + directionToTarget * currentLaserLength;
            lineRenderer.SetPosition(1, laserEndPoint);

           
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionToTarget, currentLaserLength);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Player"))
                {
                  
                    lineRenderer.SetPosition(1, hit.point);
                    currentLaserLength = Vector2.Distance(transform.position, hit.point);
                    break;
                }
                else if (hit.collider.CompareTag("Block"))
                {
                    
                    continue;
                }
            }

            yield return null; 
        }

        yield return new WaitForSeconds(2f);
        lineRenderer.enabled = false;
        isLaserShooting = false;
    }
}
