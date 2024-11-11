using UnityEngine;
using System.Collections;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab; 
    public Transform player; 

  
    public float minXDistance = 2f;
    public float maxXDistance = 5f;

    
    public float yDistance = 10f;

   
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    
    public int straightLineCount = 10;
    public int spreadCount = 5;
    public int rapidFireCount = 25;

   
    public float straightLineInterval = 2f;
    public float spreadInterval = 2f;
    public float rapidFireInterval = 1f;

    void Start()
    {
        yDistance = 9f;
        Camera.main.orthographicSize = 9;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        laserPrefab = GameObject.FindGameObjectWithTag("hydrabeam");
        ScheduleNextSpawn();
    }


    void ScheduleNextSpawn()
    {
        
        float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

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

   
    void SpawnLaser(Vector2 position)
    {
       
        GameObject laser = Instantiate(laserPrefab, position, Quaternion.identity);

        Destroy(laser, 3f);
    }
}
