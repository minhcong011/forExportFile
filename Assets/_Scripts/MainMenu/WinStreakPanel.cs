using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinStreakPanel : SingletonBase<WinStreakPanel>
{
    [SerializeField] private WinStreakChest[] winStreakChests;
    [SerializeField] private GameObject[] levelsProcess;
    [SerializeField] private WinStreakClampPanel winStreakClampPanel;
    private void Start()
    {
        SetChestWhenStart();
        SetLevelProcess();
    }
    private void SetLevelProcess()
    {
        for(int i = 0; i < levelsProcess.Length; i++)
        {
            levelsProcess[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            if (i < GameCache.GC.countWinStreak) levelsProcess[i].transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    private void SetChestWhenStart()
    {
        for(int i = 0; i < winStreakChests.Length; i++)
        {
            if (winStreakChests[i].AmountWinStreakToCollect  <= GameCache.GC.countWinStreak)
            {
                if (GameCache.GC.finishCollectWinStreakChest[i] == false)
                {
                    winStreakChests[i].IsCanClamp = true;
                }
                else
                {
                    winStreakChests[i].SetFinishClamp();
                }
            }
        }
    }
    public void ClampChest(GiftInItemSheet[] giftInItemSheets)
    {
        winStreakClampPanel.gameObject.SetActive(true);
        winStreakClampPanel.Set(giftInItemSheets);
    }
}
