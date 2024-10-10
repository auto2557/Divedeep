using UnityEngine;
using System.Collections;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab ของเลเซอร์
    public Transform player; // ตำแหน่งของผู้เล่น

    // ระยะห่างสำหรับสุ่มแกน X
    public float minXDistance = 2f;
    public float maxXDistance = 5f;

    // ระยะห่างในแกน Y ที่เลเซอร์จะเริ่มจากด้านบน
    public float yDistance = 10f;

    // เวลาในการสุ่ม interval ของ pattern
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    // จำนวนการยิงสำหรับแต่ละ Pattern
    public int straightLineCount = 10;
    public int spreadCount = 5;
    public int rapidFireCount = 25;

    // เวลาระหว่างการยิงในแต่ละ Pattern
    public float straightLineInterval = 2f;
    public float spreadInterval = 2f;
    public float rapidFireInterval = 1f;

    void Start()
    {
        // เริ่มต้นกระบวนการ spawn
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        // สุ่ม interval สำหรับการ spawn
        float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

        // เลือก Pattern
        int pattern = Random.Range(0, 3); // สุ่ม 3 Pattern

        // สร้างเลเซอร์ตาม Pattern ที่สุ่มได้
        switch (pattern)
        {
            case 0:
                StartCoroutine(SpawnStraightLinePattern());
                break;
            case 1:
                StartCoroutine(SpawnSpreadPattern());
                break;
            case 2:
                StartCoroutine(SpawnRapidFirePattern());
                break;
        }
    }

    // Pattern 1: ยิงเลเซอร์ทีละ 1 จากด้านบน ทุก 2 วินาที 10 ครั้ง
    IEnumerator SpawnStraightLinePattern()
    {
        for (int i = 0; i < straightLineCount; i++)
        {
            // สุ่มตำแหน่ง X ด้านบนของผู้เล่น
            float xPosition = player.position.x + Random.Range(-maxXDistance, maxXDistance);
            Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);

            // ยิงเลเซอร์จากด้านบน
            SpawnLaser(spawnPosition);

            // รอ 2 วินาทีก่อนยิงเลเซอร์ถัดไป
            yield return new WaitForSeconds(straightLineInterval);
        }

        // เรียกใช้งาน Pattern ถัดไป
        ScheduleNextSpawn();
    }

    // Pattern 2: ยิงเลเซอร์ 3 อันพร้อมกันทุก 2 วินาที 5 ครั้ง
    IEnumerator SpawnSpreadPattern()
    {
        for (int i = 0; i < spreadCount; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // กระจายเลเซอร์ 3 อันในแกน X ด้านบนผู้เล่น
                float xPosition = player.position.x + j * (minXDistance + Random.Range(0, maxXDistance - minXDistance));
                Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);

                // ยิงเลเซอร์จากด้านบน
                SpawnLaser(spawnPosition);
            }

            // รอ 2 วินาทีก่อนยิงชุดต่อไป
            yield return new WaitForSeconds(spreadInterval);
        }

        // เรียกใช้งาน Pattern ถัดไป
        ScheduleNextSpawn();
    }

    // Pattern 3: ยิงเลเซอร์ทีละ 1 ทุก 1 วินาที รวม 25 อัน
    IEnumerator SpawnRapidFirePattern()
    {
        for (int i = 0; i < rapidFireCount; i++)
        {
            // สุ่มตำแหน่ง X ด้านบนของผู้เล่น
            float xPosition = player.position.x + Random.Range(-maxXDistance, maxXDistance);
            Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);

            // ยิงเลเซอร์จากด้านบน
            SpawnLaser(spawnPosition);

            // รอ 1 วินาทีก่อนยิงเลเซอร์ถัดไป
            yield return new WaitForSeconds(rapidFireInterval);
        }

        // เรียกใช้งาน Pattern ถัดไป
        ScheduleNextSpawn();
    }

    // ฟังก์ชันสำหรับการยิงเลเซอร์
    void SpawnLaser(Vector2 position)
    {
        // Instantiate เลเซอร์ในตำแหน่งที่กำหนด
        GameObject laser = Instantiate(laserPrefab, position, Quaternion.identity);

        // ทำลายเลเซอร์หลังจากเวลาที่กำหนด (เช่น 3 วินาที)
        Destroy(laser, 3f);
    }
}
