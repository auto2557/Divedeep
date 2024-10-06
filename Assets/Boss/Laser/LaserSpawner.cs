using UnityEngine;
using System.Collections.Generic;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab of the laser
    public Transform player; // Player's position

    // Distance range for randomizing Y and X axes
    public float minYDistance = 8f;
    public float maxYDistance = 10f;
    public float minXDistance = 2f;
    public float maxXDistance = 5f;

    // Minimum distance required between lasers to avoid overlap
    public float minSpawnDistance = 2f;

    // Time after which the laser will disappear
    public float laserLifetime = 3f;

    // Random spawn time interval
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    void Start()
    {
        // Start the laser spawning process
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        // Get a random spawn interval between min and max
        float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

        // Schedule the next laser spawn
        Invoke("SpawnPattern", spawnInterval);
    }

    void SpawnPattern()
    {
        // Randomly choose a shooting pattern
        int pattern = Random.Range(0, 3); // Three patterns: 0, 1, 2

        switch (pattern)
        {
            case 0:
                SpawnStraightLinePattern();
                break;
            case 1:
                SpawnSpreadPattern();
                break;
            case 2:
                SpawnCircularPattern();
                break;
        }

        // Schedule the next laser spawn after this one
        ScheduleNextSpawn();
    }

    // Pattern 1: Straight Line Above Player
    void SpawnStraightLinePattern()
    {
        int laserCount = Random.Range(3, 7);
        float yDistance = Random.Range(minYDistance, maxYDistance);

        for (int i = 0; i < laserCount; i++)
        {
            float xPosition = player.position.x + i * minSpawnDistance * 2;
            Vector2 spawnPosition = new Vector2(xPosition, player.position.y + yDistance);
            SpawnLaser(spawnPosition);
        }
    }

    // Pattern 2: Spread Pattern (like a cone)
    void SpawnSpreadPattern()
    {
        int laserCount = Random.Range(3, 7);
        float yDistance = Random.Range(minYDistance, maxYDistance);

        for (int i = 0; i < laserCount; i++)
        {
            float angleOffset = (i - (laserCount / 2)) * 15f; // Spread angle
            float xPosition = player.position.x + Mathf.Sin(angleOffset * Mathf.Deg2Rad) * maxXDistance;
            float yPosition = player.position.y + yDistance;

            Vector2 spawnPosition = new Vector2(xPosition, yPosition);
            SpawnLaser(spawnPosition);
        }
    }

    // Pattern 3: Circular Pattern Around Player
    void SpawnCircularPattern()
    {
        int laserCount = Random.Range(3, 7);
        float radius = Random.Range(minXDistance, maxXDistance);

        for (int i = 0; i < laserCount; i++)
        {
            float angle = i * (360f / laserCount);
            float xPosition = player.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float yPosition = player.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector2 spawnPosition = new Vector2(xPosition, yPosition);
            SpawnLaser(spawnPosition);
        }
    }

    // Method for spawning lasers at specific positions
    void SpawnLaser(Vector2 position)
    {
        // Instantiate the laser at the given position
        GameObject laser = Instantiate(laserPrefab, position, Quaternion.identity);

        // Destroy the laser after a delay
        Destroy(laser, laserLifetime);
    }
}
