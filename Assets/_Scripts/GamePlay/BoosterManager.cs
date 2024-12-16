using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoosterManager : SingletonBase<BoosterManager>
{
    //1 suffle
    //2 sort
    //3 vip
    //4 receive
    //5 receivePlayDailyGame
    [SerializeField] private GameObject[] boosterDisplays;
    private void Start()
    {
        if(GameModeController.gameMode == GameMode.Nomal)
        {
            foreach(GameObject boosterDisplay in boosterDisplays)
            {
                boosterDisplay.SetActive(false);
            }
        }
    }
    public void UseBooster(int boosterId)
    {
        switch (boosterId)
        {
            case 1:
                {
                    ColorManager.Instance.RandomCarColorWithBooster();
                    GameCache.GC.shuffleBoosterAmount--;
                    break;
                }
            case 2:
                {
                    SManManager.Instance.nomalModeWaitingLine.ChangeColorSManWithBooster();
                    GameCache.GC.sortBoosterAmount--;
                    break;
                }
            case 3:
                {
                    TouchManager.Instance.SetVipBoosterTouch(true);
                    GameCache.GC.vipSlotBoosterAmount--;
                    GamePlayUIManager.Instance.ShowVipTutorialText(true);
                    break;
                }
            case 4:
                {
                    SManManager.Instance.nomalModeWaitingLine.ChangeColorSManWithBooster();
                    CarManager.Instance.UnlockHolderReceiveBooster();
                    break;
                }
            case 5:
                {
                    SceneManager.LoadScene("GamePlay");
                    GameModeController.gameMode = GameMode.Daily;
                    break;
                }
        }
        GameController.Instance.IsUseBooster = true;
    }
}
