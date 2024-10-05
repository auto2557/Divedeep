using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab of the laser
    public Transform player; // Player's position

    // Distance range for randomizing Y and X axes
    public float minYDistance = 8f;
    public float maxYDistance = 10f;
    public float minXDistance = 2f;
    public float maxXDistance = 5f;

    // Time after which the laser will disappear
    public float laserLifetime = 4f;

    void Start()
    {
        // Spawn the laser every 2 seconds
        InvokeRepeating("SpawnLaser", 2f, 2f);
    }

    void SpawnLaser()
    {
        // Randomize Y distance (above the Player)
        float randomY = Random.Range(minYDistance, maxYDistance);
        
        // Randomize X position (must be to the side of the Player)
        float randomX = Random.Range(minXDistance, maxXDistance);
        
        // Randomize whether to spawn on the left or right of the Player
        float spawnXPosition = player.position.x + (Random.value < 0.5f ? -randomX : randomX);

        // Set the spawn position of the laser
        Vector2 spawnPosition = new Vector2(spawnXPosition, player.position.y + randomY);
        
        // Instantiate the laser at the random position
        GameObject laser = Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
        
        // Destroy the laser after a delay of 4 seconds
        Destroy(laser, laserLifetime);
    }
}
