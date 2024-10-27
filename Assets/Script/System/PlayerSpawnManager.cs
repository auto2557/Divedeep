using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{

     public Transform player;
     private Animator anim;

    void Start()
{
    anim = GetComponent<Animator>();
    LoadPlayerPosition();
}



public void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.CompareTag("Player"))
    {
        anim.SetBool("isSave", true);
         PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.position.z);
        PlayerPrefs.Save();

        Debug.Log("save");
    }
}
public void OnTriggerExit2D(Collider2D collision)
{
    if(collision.CompareTag("Player"))
    {
        anim.SetBool("isSave", false);
    }
}


    public void LoadPlayerPosition()
{
    if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
    {
        float x = PlayerPrefs.GetFloat("PlayerPosX");
        float y = PlayerPrefs.GetFloat("PlayerPosY");
        float z = PlayerPrefs.GetFloat("PlayerPosZ");
        
        player.position = new Vector3(x, y, z);
      
    }
    
}


}
