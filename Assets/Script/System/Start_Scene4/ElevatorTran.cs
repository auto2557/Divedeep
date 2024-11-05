using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class ElevatorState
{
    public bool hasReachedDestination;
}

public class ElevatorTran : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform startPoint;
    public Transform endPoint;
    public player player; 
    public FadeImageAlpha[] fade;
    public GameObject[] black;
    public GameObject UIplayer;
    
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/elevatorState.json";
        
        rb = GetComponent<Rigidbody2D>();
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (File.Exists(saveFilePath))
        {
            LoadState();
        }
        else
        {
            ResetElevator();
        }
    }

    void Update()
    {
        if (transform.position.y >= endPoint.position.y)
        {
            rb.velocity = Vector2.zero;
            player.enabled = true;
            fade[0].enabled = true;
            fade[1].enabled = true;
            UIplayer.SetActive(true);
            SaveState();
        }
    }

    private void SaveState()
    {
        ElevatorState state = new ElevatorState { hasReachedDestination = true };
        string json = JsonUtility.ToJson(state);
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadState()
    {
        string json = File.ReadAllText(saveFilePath);
        ElevatorState state = JsonUtility.FromJson<ElevatorState>(json);

        if (state.hasReachedDestination)
        {
            transform.position = endPoint.position;
            rb.velocity = Vector2.zero;
            player.enabled = true;
            fade[0].enabled = true;
            fade[1].enabled = true;
            UIplayer.SetActive(true);
        }
        else
        {
            ResetElevator();
        }
    }

    private void ResetElevator()
    {
        transform.position = startPoint.position;
        rb.velocity = new Vector2(0, 2f);
        player.enabled = false;
        fade[0].enabled = false;
        fade[1].enabled = false;
        UIplayer.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        DeleteSaveData();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DeleteSaveData();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }
}
