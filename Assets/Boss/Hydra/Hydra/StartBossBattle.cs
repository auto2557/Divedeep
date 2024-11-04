using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public GameObject bossArea;
  
  void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.CompareTag("Player") || collision.CompareTag("dashMode"))
    {
        bossArea.SetActive(true);
        Destroy(gameObject,5f);
    }
  }
}
