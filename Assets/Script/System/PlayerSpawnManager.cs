using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // สำหรับการจัดการไฟล์
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class PlayerPositionData
{
    public float x;
    public float y;
    public float z;
}

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    private string saveFilePath;

    void Start()
    {
        anim = GetComponent<Animator>();

       
        saveFilePath = Path.Combine(Application.persistentDataPath, "PlayerPosition.json");

      
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            LoadPlayerPosition();  
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
            SoundManager.instance.PlaySFX("other", 3, 1);
            anim.SetBool("isSave", true);
            SavePlayerPosition();
            Debug.Log("Position saved to JSON file");
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
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerPositionData data = JsonUtility.FromJson<PlayerPositionData>(json);

            if (data != null)
            {
                player.position = new Vector3(data.x, data.y, data.z);
                Debug.Log("Position loaded from JSON file");
            }
        }
        else
        {
            Debug.Log("Save file not found, starting at default position");
        }
    }

    public void SavePlayerPosition()
    {
        PlayerPositionData data = new PlayerPositionData
        {
            x = player.position.x,
            y = player.position.y,
            z = player.position.z
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Position saved to JSON file at: " + saveFilePath);
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
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Position reset and JSON file deleted on exiting game or editor");
        }
    }

    void OnDestroy()
    {
        #if UNITY_EDITOR
        EditorApplication.wantsToQuit -= OnEditorQuit;
        #endif
    }
}
