using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // เป้าหมาย เช่น Player
    private Transform target;

    // ความยาวสูงสุดของเลเซอร์
    public float maxLaserLength = 10f;

    // ความเร็วในการขยายเลเซอร์
    public float laserGrowthSpeed = 5f;

    // ตัวแปรในการเก็บความยาวเลเซอร์ปัจจุบัน
    private float currentLaserLength = 0f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // หาค่า target จาก GameObject ที่มีแท็ก "Player"
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;

            // ยิงเลเซอร์ทันทีเมื่อเริ่มต้น
            ShootLaser();
        }
        else
        {
            Debug.LogError("ไม่พบ GameObject ที่มีแท็ก 'Player'");
        }
    }

    private void ShootLaser()
    {
        if (target == null) return; // ตรวจสอบว่ามี target อยู่จริงหรือไม่

        // คำนวณทิศทางจากเลเซอร์ไปยังเป้าหมาย
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // คำนวณระยะทางจากจุดเริ่มต้นไปยังเป้าหมาย
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

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
            lineRenderer.SetPosition(0, transform.position);

            // คำนวณตำแหน่งสิ้นสุดของเลเซอร์ตามความยาวปัจจุบัน
            Vector2 laserEndPoint = (Vector2)transform.position + directionToTarget * currentLaserLength;
            lineRenderer.SetPosition(1, laserEndPoint);

            // ยิง Raycast ในทิศทางของเป้าหมายตามความยาวปัจจุบัน
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, currentLaserLength);

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

                // ลบเลเซอร์เมื่อชนวัตถุหรือยิงเสร็จ
                lineRenderer.enabled = false;
                yield break;
            }

            yield return null; // รอ 1 เฟรม ก่อนขยายเลเซอร์ต่อไป
        }

        // ลบเลเซอร์เมื่อยิงเสร็จ
        lineRenderer.enabled = false;
    }
}
