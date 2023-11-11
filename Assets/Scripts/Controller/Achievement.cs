using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    public ListArchive archivements = new ListArchive();
    public List<GameObject> passedMedal = new List<GameObject>();


    private string dir = "";
    private const string LOAD_KEY = "FirstTimeLoad";
    private bool isLoaded = false;
    
    private void Start()
    {
        StartLoading();
    }
    public void StartLoading()
    {
        isLoaded = false;
        archivements = new ListArchive();
        Init();
    }

    private void Init()
    {
        if (!PlayerPrefs.HasKey(LOAD_KEY))
        {
            PlayerPrefs.SetInt(LOAD_KEY, 0);
        }
        dir = Application.persistentDataPath + "/gamefile.json";
        if (PlayerPrefs.GetInt(LOAD_KEY) == 0)
        {
            SaveData();
            PlayerPrefs.SetInt(LOAD_KEY, 1);
        }
        LoadData();

        if (isLoaded)
        {
            for (int i = 0; i < archivements.archivement.Count; i++)
            {
                passedMedal[i].SetActive(archivements.archivement[i].isPassed);
            }

         
        }
    }

    public void SaveData()
    {
        string json = "";
        json = JsonUtility.ToJson(archivements);
        File.WriteAllText(dir, json);

    }

    public void LoadData()
    {
      
        if (File.Exists(dir))
        {
            string fileData = File.ReadAllText(dir);
            archivements = JsonUtility.FromJson<ListArchive>(fileData);
            isLoaded = true;
        }
        else
        {
            isLoaded = false;
           
        }
    }

    public void SetArchivement(int bossID ,bool isPassed)
    {
        archivements.Set(bossID, isPassed);
        SaveData();
    }
}
