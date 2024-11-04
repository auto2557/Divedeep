using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questPop : MonoBehaviour
{
    public GameObject quest;
  

  void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.CompareTag("Player"))
    {
        quest.SetActive(true);
    }
  }
}
