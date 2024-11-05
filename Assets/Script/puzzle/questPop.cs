using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestPop : MonoBehaviour
{
    public GameObject quest;

    private string savePath;

    [System.Serializable]
    public class QuestData
    {
        public bool isQuestActive;
    }

    private QuestData questData = new QuestData();

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "questData.json");
        LoadQuestData();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            quest.SetActive(true);
            questData.isQuestActive = true;
            SaveQuestData(); 
        }
    }

    void SaveQuestData()
    {
        string json = JsonUtility.ToJson(questData);
        File.WriteAllText(savePath, json);
    }

    void LoadQuestData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            questData = JsonUtility.FromJson<QuestData>(json);
            quest.SetActive(questData.isQuestActive);
        }
        else
        {
            questData.isQuestActive = false;
            quest.SetActive(false);
        }
    }

    private void OnApplicationQuit()
    {
        questData.isQuestActive = false;
        SaveQuestData(); 
    }
}
