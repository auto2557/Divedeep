using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootMissile : MonoBehaviour
{
      public GameObject[] prefabs; 
    public Transform target; 
    public float yOffset = 15f; 
    public float minXSpacing = 5f; 
    public float maxXSpacing = 10f; 
    public int spawnCount = 10; 

    private float lastXPosition; 

    void Start()
    {
        SoundManager.instance.PlaySFX("other", 4, 1);
        lastXPosition = target.position.x; 

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomPrefab();
        }
    }

    void SpawnRandomPrefab()
    {
        
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        
        float xSpacing = Random.Range(minXSpacing, maxXSpacing);
        lastXPosition += xSpacing;

        
        Vector3 spawnPosition = new Vector3(lastXPosition, target.position.y + yOffset, target.position.z);

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
