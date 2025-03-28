
using UnityEditor;
using UnityEngine;

public class GameCache
{
    public static GameCache GC = new GameCache();

    public int amountAddTimeBooster;
    public int amountBreakBooster;
    public int amountFindBooster;

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
        //PlayerPrefs.SetInt("Level", 97);
        PlayerPrefs.SetInt("CoinsAmount", 10000);
        SaveDataToJson();
    }
#endif
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
