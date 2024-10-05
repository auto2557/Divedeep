using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // จุดเริ่มต้นของเลเซอร์ (เช่น ปากกระบอกปืน)
    public Transform laserStartPoint;

    // เป้าหมาย เช่น Player
    public Transform target;

    // ความยาวสูงสุดของเลเซอร์
    public float maxLaserLength = 10f;

    // ความเร็วในการขยายเลเซอร์
    public float laserGrowthSpeed = 5f;

    // ตัวแปรในการเก็บความยาวเลเซอร์ปัจจุบัน
    private float currentLaserLength = 0f;

    // เวลารอระหว่างการยิงแต่ละครั้ง
    public float timeBetweenShots = 5f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // เลเซอร์มีจุดเริ่มต้นและจุดสิ้นสุด

        // เริ่มยิงเลเซอร์ทุก ๆ 5 วินาที
        StartCoroutine(ShootLaserEveryInterval());
    }

    private void Update()
    {
        // ในการยิงเลเซอร์ เราจะจัดการใน Coroutine
    }

    private IEnumerator ShootLaserEveryInterval()
    {
        while (true)
        {
            ShootLaser(); // ยิงเลเซอร์
            yield return new WaitForSeconds(timeBetweenShots); // รอ 5 วินาที หรือค่า timeBetweenShots
        }
    }

    private void ShootLaser()
    {
        // คำนวณทิศทางจากเลเซอร์ไปยังเป้าหมาย
        Vector2 directionToTarget = (target.position - laserStartPoint.position).normalized;

        // คำนวณระยะทางจากจุดเริ่มต้นไปยังเป้าหมาย
        float distanceToTarget = Vector2.Distance(laserStartPoint.position, target.position);

        // เริ่มต้นความยาวเลเซอร์ที่ 0 ก่อน
        currentLaserLength = 0f;

        // ยิงเลเซอร์ไปยังเป้าหมาย
        StartCoroutine(ExpandLaser(directionToTarget, distanceToTarget));
    }

    private IEnumerator ExpandLaser(Vector2 directionToTarget, float distanceToTarget)
    {
        while (currentLaserLength < distanceToTarget)
        {
            // เพิ่มความยาวของเลเซอร์ทีละน้อย
            currentLaserLength += laserGrowthSpeed * Time.deltaTime;
            currentLaserLength = Mathf.Min(currentLaserLength, distanceToTarget); // จำกัดความยาวไม่ให้เกินระยะถึงเป้าหมาย

            // ตั้งตำแหน่งจุดเริ่มต้นของเลเซอร์
            lineRenderer.SetPosition(0, laserStartPoint.position);

            // คำนวณตำแหน่งสิ้นสุดของเลเซอร์ตามความยาวปัจจุบัน
            Vector2 laserEndPoint = (Vector2)laserStartPoint.position + directionToTarget * currentLaserLength;
            lineRenderer.SetPosition(1, laserEndPoint);

            // ยิง Raycast ในทิศทางของเป้าหมายตามความยาวปัจจุบัน
            RaycastHit2D hit = Physics2D.Raycast(laserStartPoint.position, directionToTarget, currentLaserLength);

            if (hit)
            {
                // หากชนวัตถุ ให้สิ้นสุดเลเซอร์ที่จุดที่ชน
                lineRenderer.SetPosition(1, hit.point);

                // ตรวจสอบว่าชนกับศัตรูหรือไม่
                if (hit.collider.CompareTag("Enemy"))
                {
                    // ทำลายศัตรู
                    Destroy(hit.collider.gameObject);
                }

                // ออกจากการยิงเลเซอร์หากชนวัตถุ
                yield break;
            }

            yield return null; // รอ 1 เฟรม ก่อนขยายเลเซอร์ต่อไป
        }
    }
}
