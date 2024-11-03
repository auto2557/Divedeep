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
        yDistance = 6f;
        Camera.main.orthographicSize = 7;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        laserPrefab = GameObject.FindGameObjectWithTag("hydrabeam");
        ScheduleNextSpawn();
    }


    void ScheduleNextSpawn()
    {
        
        float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

        // เลือก Pattern
        int pattern = Random.Range(0, 3); 

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

    
    IEnumerator SpawnStraightLinePattern()
    {
        for (int i = 0; i < straightLineCount; i++)
        {
            
            float xPosition = player.position.x + Random.Range(-maxXDistance, maxXDistance);
            Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);

            
            SpawnLaser(spawnPosition);

           
            yield return new WaitForSeconds(straightLineInterval);
        }

       
        ScheduleNextSpawn();
    }

    
    IEnumerator SpawnSpreadPattern()
    {
        for (int i = 0; i < spreadCount; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                
                float xPosition = player.position.x + j * (minXDistance + Random.Range(0, maxXDistance - minXDistance));
                Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);

             
                SpawnLaser(spawnPosition);
            }

          
            yield return new WaitForSeconds(spreadInterval);
        }

       
        ScheduleNextSpawn();
    }

    
    IEnumerator SpawnRapidFirePattern()
    {
        for (int i = 0; i < rapidFireCount; i++)
        {
      
            float xPosition = player.position.x + Random.Range(-maxXDistance, maxXDistance);
            Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);

         
            SpawnLaser(spawnPosition);

            
            yield return new WaitForSeconds(rapidFireInterval);
        }

    
        ScheduleNextSpawn();
    }

    // ฟังก์ชันสำหรับการยิงเลเซอร์
    void SpawnLaser(Vector2 position)
    {
       
        GameObject laser = Instantiate(laserPrefab, position, Quaternion.identity);

        Destroy(laser, 3f);
    }
}
