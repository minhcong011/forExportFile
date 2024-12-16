using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject openBreakEggBt;
    [SerializeField] private TextMeshProUGUI playNomalModeCurrentLevelText;
    [SerializeField] private TextMeshProUGUI tropicalPackTimeRemainResetText;
    [SerializeField] private TextMeshProUGUI spacePackTimeRemainResetText;
    [SerializeField] private TextMeshProUGUI endlessTreasureTimeRemainResetText;

    [SerializeField] private GameObject openPiratePackBt;
    [SerializeField] private GameObject openMexicanPackBt;
    [SerializeField] private GameObject openTropicalPackBt;
    [SerializeField] private GameObject openSpacePackBt;

    [SerializeField] private GameObject buyReceiveDailyLevelBooster;
    private void Start()
    {
        Application.targetFrameRate = 60;
        SetMainMenuUI();
        SetShowSupperPackBt();
    }
    private void SetShowSupperPackBt()
    {
        //openPiratePackBt.SetActive(GameCache.GC.piratePackFinishBuy < 2);
        //openMexicanPackBt.SetActive(GameCache.GC.mexicanPackFinishBuy < 2);
        //openTropicalPackBt.SetActive(!GameCache.GC.finishBuyTropicalPackDaily);
        //openSpacePackBt.SetActive(!GameCache.GC.finishBuySpacePackDaily);

        if (openTropicalPackBt.activeSelf) tropicalPackTimeRemainResetText.text = GetTimeUntilNextReset();
        if (openSpacePackBt.activeSelf) spacePackTimeRemainResetText.text = GetTimeUntilNextReset();
    }
    private void SetMainMenuUI()
    {
        openBreakEggBt.SetActive(GameCache.GC.canBreakEggDaily);
        playNomalModeCurrentLevelText.text = $"Level {GameCache.GC.currentLevel}";
        endlessTreasureTimeRemainResetText.text = GetTimeUntilNextReset();
    }
    string GetTimeUntilNextReset()
    {
        DateTime currentTime = DateTime.Now;
        DateTime nextResetTime = currentTime.Date.AddDays(1);
        TimeSpan timeUntilReset = nextResetTime - currentTime;

        return $"{timeUntilReset.Hours}h {timeUntilReset.Minutes}m";
    }
    public void PlayNomalGame()
    {
        GameModeController.gameMode = GameMode.Nomal;
        SceneManager.LoadScene("GamePlay");
    }
    public void PlayDailyLevel()
    {
        if (!GameCache.GC.finishPlayDailyGame)
        {
            GameModeController.gameMode = GameMode.Daily;
            SceneManager.LoadScene("GamePlay");
        }
        else buyReceiveDailyLevelBooster.SetActive(true);
    }
}
