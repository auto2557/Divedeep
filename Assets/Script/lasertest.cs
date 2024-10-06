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

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // ยิงเลเซอร์เฉพาะเมื่อยังไม่ได้ยิง
        if (!isLaserShooting)
        {
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
    }

    private void ShootLaser()
    {
        if (target == null) return; // ตรวจสอบว่ามี target อยู่จริงหรือไม่

        // คำนวณทิศทางจากเลเซอร์ไปยังเป้าหมายครั้งเดียวตอนเริ่มการยิง
        directionToTarget = (target.position - transform.position).normalized;

        // คำนวณระยะทางจากจุดเริ่มต้นไปยังเป้าหมาย
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // เริ่มต้นความยาวเลเซอร์ที่ 0 ก่อน
        currentLaserLength = 0f;

        // ตั้งสถานะว่าเลเซอร์กำลังยิง
        isLaserShooting = true;

        // ยิงเลเซอร์ไปยังเป้าหมาย
        StartCoroutine(ExpandLaser(distanceToTarget));
    }

    private IEnumerator ExpandLaser(float distanceToTarget)
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
                isLaserShooting = false;
                yield break;
            }

            yield return null; // รอ 1 เฟรม ก่อนขยายเลเซอร์ต่อไป
        }

        // รอ 2 วินาทีก่อนลบเลเซอร์
        yield return new WaitForSeconds(2f);
        lineRenderer.enabled = false;
        isLaserShooting = false;
    }
}
