using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PuzzleData
{
    public bool GenA;
    public bool GenB;
    public bool[] elevatorsActive;
}

public class PuzzleSystem : MonoBehaviour
{
    public openA A;
    public openB B;
    public GameObject quest;
    public Elevator[] ele;
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/puzzleData.json";
        LoadData();
    }

    void Update()
    {
        if (A.GenA && B.GenB)
        {
            ele[0].enabled = true;
            ele[1].enabled = true;
            Destroy(quest);
            SaveData();
        }
    }

    void SaveData()
    {
        PuzzleData data = new PuzzleData
        {
            GenA = A.GenA,
            GenB = B.GenB,
            elevatorsActive = new bool[ele.Length]
        };

        for (int i = 0; i < ele.Length; i++)
        {
            data.elevatorsActive[i] = ele[i].enabled;
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PuzzleData data = JsonUtility.FromJson<PuzzleData>(json);

            A.GenA = data.GenA;
            B.GenB = data.GenB;

            for (int i = 0; i < ele.Length; i++)
            {
                ele[i].enabled = data.elevatorsActive[i];
            }
        }
    }
}
