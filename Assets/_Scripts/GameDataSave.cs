
using UnityEngine;

public class GameDataSave : SingletonBaseDontDestroyOnLoad<GameDataSave>
{
    private bool finishLoad;
    public bool FinishLoad { get { return finishLoad; } }
    protected override void Awake()
    {
        base.Awake();
        if (!finishLoad)
        {
            GameCache.LoadDataFromJson();
            finishLoad = true;
        }
    }
    private void OnApplicationQuit()
    {
        GameCache.SaveDataToJson();
    }
    private void OnApplicationPause(bool pause)
    {
        if(!finishLoad) return;
        GameCache.SaveDataToJson();
    }
}
