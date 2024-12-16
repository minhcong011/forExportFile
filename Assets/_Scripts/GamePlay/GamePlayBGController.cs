using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayBGController : MonoBehaviour
{
    [SerializeField] private GameObject[] bgs;
    [SerializeField] private GameObject dailyModeBg;
    private void Start()
    {
        if(GameModeController.gameMode == GameMode.Daily)
        {
            dailyModeBg.SetActive(true);
            return;
        }
        if (CheckLevelID(1, 10) || CheckLevelID(41, 50)) bgs[0].SetActive(true); 
        if (CheckLevelID(11, 20) || CheckLevelID(51, 60)) bgs[1].SetActive(true); 
        if (CheckLevelID(21, 30) || CheckLevelID(61, 70)) bgs[2].SetActive(true); 
        if (CheckLevelID(31, 40) || CheckLevelID(71, 80)) bgs[3].SetActive(true);
    }
    private bool CheckLevelID(int min, int max)
    {
        if (GameCache.GC.currentLevel >= min && GameCache.GC.currentLevel <= max) return true;
        return false;
    }
}
