using UnityEngine;

public class HydraBeamAttack : MonoBehaviour
{
    public LineRenderer beamLine;        // สำหรับวาดลำแสง
    public Transform firePoint;          // ตำแหน่งที่ยิงลำแสงออกมา
    public float beamDuration = 3f;      // ระยะเวลาที่ลำแสงทำงาน
    public float beamDamage = 10f;       // ความเสียหายต่อตัวละคร
    private float timer;

    void Start()
    {
        beamLine.enabled = false;  // ปิดลำแสงตอนเริ่มต้น
    }

    public void StartBeamAttack()
    {
        beamLine.enabled = true;
        timer = beamDuration;
        beamLine.SetPosition(0, firePoint.position);  // ตำแหน่งเริ่มต้นของลำแสง
        beamLine.SetPosition(1, firePoint.position + transform.right * 10f);  // ปลายลำแสง
    }

    void Update()
    {
        if (beamLine.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopBeamAttack();
            }
        }
    }

    public void StopBeamAttack()
    {
        beamLine.enabled = false;
    }

    /*void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ทำความเสียหายต่อผู้เล่นเมื่ออยู่ในลำแสง
            collision.GetComponent<PlayerHealth>().TakeDamage(beamDamage * Time.deltaTime);
        }
    }*/
}
