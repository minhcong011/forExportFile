
using UnityEditor;
using UnityEngine;

public class GameCache
{
    public static GameCache GC = new GameCache();

    public int currentXP;
    public int maxXP = 10;
    public float currentHp;
    public float maxHp = 20;
    public int currentLevel = 1;

    public bool blockAds;
    public bool soundOff;
    public bool vibrateOff;

    public WeaponType currentWeaponType = WeaponType.Hand;
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
        GC.currentLevel = 50;
        SaveDataToJson();
    }
#endif
    public static void SaveDataToJson()
    {
        string json = JsonUtility.ToJson(GC);
        PlayerPrefs.SetString("data", json);
        PlayerPrefs.Save();
    }
    public static void LoadDataFromJson()
    {
        string json = PlayerPrefs.GetString("data", "");
        if(json != "") GC = JsonUtility.FromJson<GameCache>(json);
        GC.currentHp = GC.maxHp;
    }
}
