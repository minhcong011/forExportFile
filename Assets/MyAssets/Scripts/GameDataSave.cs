
using UnityEngine;

public class GameDataSave : SingletonBaseDontDestroyOnLoad<GameDataSave>
{
    private bool finishLoad;
    public bool FinishLoad { get { return finishLoad; } }
    private void Start()
    {
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
