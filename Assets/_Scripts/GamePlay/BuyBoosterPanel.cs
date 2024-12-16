using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyBoosterPanel : MonoBehaviour
{
    [SerializeField] private int boosterId;
    public void BuyWithCoin()
    {
        if (GameCache.GC.coin < 900) return;
        BoosterManager.Instance.UseBooster(boosterId);
        gameObject.SetActive(false);
        GameCache.GC.coin -= 900;
        UICoinText.Instance.UpdateCoinText();
    }
    public void BuyWitAds()
    {
        AdsManager.Instance.ShowRewardAdsUseBooster(boosterId);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "GamePlay") GameController.Instance.PauseGame();
    }
    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            GameController.Instance.PlayGame();
        }
    }
    //public void ExitGame()
    //{
    //    if (boosterId == 4 || boosterId == 5)
    //    {
    //        if (GameModeController.gameMode == GameMode.Nomal) GameCache.GC.countWinStreak = 0;
    //        SceneManager.LoadScene("MainMenu");
    //    }
    //}
}
