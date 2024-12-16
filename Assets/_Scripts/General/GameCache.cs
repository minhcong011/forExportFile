
using UnityEditor;
using UnityEngine;

public class GameCache
{
    public static GameCache GC = new GameCache();

    public int currentLevel = 1;
    public int shuffleBoosterAmount;
    public int sortBoosterAmount;
    public int vipSlotBoosterAmount;
    public int coin;
    public int avatarID;
    public int userCountryID;

    public int amountCarCollect;
    public int amountCarToTakeGift = 5;
    public int amountCarCollectIncrease;
    public int carCollectID;
    public int giftLevel;

    public int endlessTreasureLevel = 0;

    public int piratePackFinishBuy;
    public int mexicanPackFinishBuy;

    public int countWinStreak = 20;

    public float x2CollectCarBoosterTime;

    public bool blockAds;
    public bool soundOff;
    public bool vibrateOff;
    public bool finishChooseCountry;
    public bool canBreakEggDaily;
    public bool finishPlayDailyGame;
    public bool finishGetStarDailyGame;

    public bool finishBuyTropicalPackDaily;
    public bool finishBuySpacePackDaily;
    public bool finishLoadData;

    public bool[] finishCollectWinStreakChest = new bool[9];
    public bool[] canCollectWinStreakChest = new bool[9];
    public string userName;
    public string lastResetTime = "2000-10-14T00:00:00.0000000Z";
    public static void ResetCollectCarData()
    {
        GC.amountCarCollect = 0;
        GC.amountCarToTakeGift = 5;
        GC.amountCarCollectIncrease = 0;
        GC.carCollectID = Random.Range(0, 3);
        GC.giftLevel = 0;
    }
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
        GC.currentLevel = 2;
        GC.shuffleBoosterAmount = 100;
        GC.vipSlotBoosterAmount = 100;
        GC.sortBoosterAmount = 100;
        GC.amountCarCollectIncrease = 40;
        GC.amountCarToTakeGift = 5;
        GC.carCollectID = 0;
        GC.userCountryID = 60;
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
