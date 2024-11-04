using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform player;
    private Animator anim;

    void Start()
{
    anim = GetComponent<Animator>();

    // Attempt to find the player GameObject and assign it to the player variable
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    if (playerObject != null)
    {
        player = playerObject.transform;
        LoadPlayerPosition();  // Call this only if player is successfully assigned
    }
    else
    {
        Debug.LogWarning("Player GameObject with tag 'Player' not found. Make sure it exists in the scene.");
    }

    #if UNITY_EDITOR
    EditorApplication.wantsToQuit += OnEditorQuit;
    #endif
}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("dashMode"))
        {
            anim.SetBool("isSave", true);
            SavePlayerPosition();
            Debug.Log("Position saved");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("dashMode"))
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

    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.position.z);
        PlayerPrefs.Save();
    }

    private bool OnEditorQuit()
    {
        ResetPlayerPosition();
        return true;
    }

    void OnApplicationQuit()
    {
        ResetPlayerPosition();
    }

    private void ResetPlayerPosition()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.Save();
        Debug.Log("Position reset on exiting game or editor");
    }

    void OnDestroy()
    {
        #if UNITY_EDITOR
        EditorApplication.wantsToQuit -= OnEditorQuit;
        #endif
    }
}
