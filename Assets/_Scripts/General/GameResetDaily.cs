using System;
using UnityEngine;

public class GameResetDaily : MonoBehaviour
{
    void Start()
    {
        DateTime currentTime = DateTime.Now;

        DateTime lastResetTime = DateTime.Parse(GameCache.GC.lastResetTime);

        if (currentTime.Date > lastResetTime.Date)
        {
            ResetGame();

            GameCache.GC.lastResetTime = currentTime.ToString("o");
        }
    }

    void ResetGame()
    {
        GameCache.GC.canBreakEggDaily = true;
        GameCache.GC.endlessTreasureLevel = 0;
        GameCache.GC.finishBuySpacePackDaily = false;
        GameCache.GC.finishBuySpacePackDaily = false;
        GameCache.GC.countWinStreak = 0;
        GameCache.GC.finishCollectWinStreakChest = new bool[9];
        GameCache.ResetCollectCarData();
    }
}
