
using System;
using UnityEditor;
using UnityEngine;

public class GameCache
{
    public static GameCache GC = new GameCache();

    public bool blockAds;
    public bool soundOff;
    public bool vibrateOff;
#if UNITY_EDITOR
    [MenuItem("Data/DeleteAll")]
    private static void DeleteAll()
    {
        Debug.Log("Delete All Data.........");
        PlayerPrefs.DeleteAll();
        GC = new GameCache();
        SaveDataToJson();
    }
    [MenuItem("Data/Test")]
    private static void Test()
    {
        Debug.Log("test");
        SaveDataToJson();
    }
#endif
    public bool GetGunUnlock(string gunName)
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt(gunName, 0));
    }
    public void SetGunUnlock(string gunName, bool value)
    {
        PlayerPrefs.SetInt(gunName, Convert.ToInt32(gunName));
    }
    public static void SaveDataToJson()
    {
        string json = JsonUtility.ToJson(GC);
        PlayerPrefs.SetString("data", json);
        PlayerPrefs.Save();
        Debug.Log("Save" + json);
    }
    public static void LoadDataFromJson()
    {
        string json = PlayerPrefs.GetString("data", "");
        Debug.Log("Load" + json);
        if(json != "") GC = JsonUtility.FromJson<GameCache>(json);
    }
}
